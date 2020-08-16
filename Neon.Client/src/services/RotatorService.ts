import { observable, action } from 'mobx';
import moment from 'moment';
import { Mutex, MutexInterface } from 'async-mutex';
import HttpImageAssetProvider from '../providers/HttpImageAssetProvider';
import ImageAsset from '../models/ImageAsset';

export default class RotatorService {
    @observable public currentImage: Blob;

    private _index: number;
    private _mutex: Mutex;
    private _running: boolean;
    private _provider: HttpImageAssetProvider;

    constructor(provider: HttpImageAssetProvider) {
        this._provider = provider;
        this._index = 0;
        this._mutex = new Mutex();
        this._running = false;
    }

    public start(): void {
        this._running = true;
        this._rotate(); // Fire & Forget
    }

    public stop(): void {
        this._running = false;
    }

    @action
    private async _rotate(): Promise<void> {
        while(this._running) {
            try {
                const assetList: ImageAsset[] = (await this._provider.allAsync())
                    .filter(this.filter)
                    .sort(this.sort);
                this._index = (this._index + 1) >= assetList.length ? 0 : this._index + 1;
                const asset: ImageAsset = assetList[this._index];
                this.currentImage = await this._provider.oneContentAsync(asset.id);

                await new Promise((resolve) => window.setTimeout(resolve, asset.displayTime * 1000));
            } catch (ex) {
                console.warn('Error while rotating:', ex);
            }
        }
    }

    private filter(asset: ImageAsset): boolean {
        const now: moment.Moment = moment();
        return asset.isActive
            && asset.notBefore ? (moment.utc(asset.notBefore).isSameOrBefore(now, 'day')) : true
            && asset.notAfter ? (moment.utc(asset.notAfter).isSameOrAfter(now, 'day')) : true;
    }

    private sort(lhs: ImageAsset, rhs: ImageAsset): number {
        if (lhs.order < rhs.order)
            return -1;
        if (lhs.order > rhs.order)
            return 1;
        return 0;
    }
}