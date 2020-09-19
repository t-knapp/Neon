import { observable, action } from 'mobx';
import moment from 'moment';
import { Mutex } from 'async-mutex';
import HttpImageAssetProvider from '../providers/HttpImageAssetProvider';
import ImageAsset from '../models/ImageAsset';

export default class RotatorService {
    @observable public currentImage: string;

    private readonly _mutex: Mutex;
    private _index: number;
    private _running: boolean;
    private _provider: HttpImageAssetProvider;

    constructor(provider: HttpImageAssetProvider) {
        this._mutex = new Mutex();
        this._index = 0;
        this._running = false;
        this._provider = provider;
    }

    public start(): void {
        this._running = true;
        this._rotate(); // Fire & Forget
    }

    public stop(): void {
        this._running = false;
    }

    private async _blobToBase64(blob: Blob): Promise<string> {
        return new Promise((resolve, reject) => {
            const reader: FileReader = new FileReader();
            reader.onloadend = () => {
                resolve(reader.result as string);
            };
            reader.onerror = () => {
                reject(reader.error);
            };
            // result will be string @see: https://developer.mozilla.org/en-US/docs/Web/API/FileReader/result
            reader.readAsDataURL(blob);
        });
    }

    @action
    private async _rotate(): Promise<void> {
        while (this._running) {
            try {
                const assetList: ImageAsset[] = (await this._provider.allAsync())
                    .filter(this._filter)
                    .sort(this._sort);
                if (assetList.length === 0) {
                    this.currentImage = null;
                    await new Promise((resolve) => window.setTimeout(resolve, 5000));
                    continue;
                }
                this._index = (this._index + 1) >= assetList.length ? 0 : this._index + 1;
                const asset: ImageAsset = assetList[this._index];
                this.currentImage = await this._blobToBase64(await this._provider.oneContentAsync(asset.id));
                await new Promise((resolve) => window.setTimeout(resolve, asset.displayTime * 1000));
            } catch (ex) {
                console.warn('Error while rotating:', ex);
                this.currentImage = null;
                await new Promise((resolve) => window.setTimeout(resolve, 5000));
            }
        }
    }

    private _filter(asset: ImageAsset): boolean {
        const now: moment.Moment = moment();
        const r: boolean = asset.isActive
            && (asset.notBefore ? (moment.utc(asset.notBefore).isSameOrBefore(now, 'day')) : true)
            && (asset.notAfter ? (moment.utc(asset.notAfter).isSameOrAfter(now, 'day')) : true);
        return r;
    }

    private _sort(lhs: ImageAsset, rhs: ImageAsset): number {
        if (lhs.order < rhs.order)
            return -1;
        if (lhs.order > rhs.order)
            return 1;
        return 0;
    }
}