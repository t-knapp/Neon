import { observable, action } from 'mobx';
import moment from 'moment';
import HttpImageAssetProvider from '../providers/HttpImageAssetProvider';
import ImageAsset from '../models/ImageAsset';
import { toString } from '../helpers/BlobHelper';

export default class RotatorService {
    @observable public currentImage: string;

    private _index: number;
    private _running: boolean;
    private _provider: HttpImageAssetProvider;
    private _cancellation: () => void;

    constructor(provider: HttpImageAssetProvider) {
        this._index = 0;
        this._running = false;
        this._provider = provider;
    }

    public start(): void {
        this._running = true;
        this._rotate(new Promise((resolve) => { this._cancellation = resolve; }));
    }

    public stop(): void {
        this._running = false;
        this._cancellation();
    }

    @action
    private async _rotate(cancellationPromise: Promise<void>): Promise<void> {
        while (this._running) {
            try {
                const assetList: ImageAsset[] = (await this._provider.allAsync())
                    .filter(this._filter)
                    .sort(this._sort);
                if (assetList.length === 0) {
                    this.currentImage = null;
                    await Promise.race([new Promise((resolve) => window.setTimeout(resolve, 5000)), cancellationPromise]);
                    continue;
                }
                this._index = (this._index + 1) >= assetList.length ? 0 : this._index + 1;
                const asset: ImageAsset = assetList[this._index];
                this.currentImage = await toString(await this._provider.oneContentAsync(asset.id));
                await Promise.race([new Promise((resolve) => window.setTimeout(resolve, asset.displayTime * 1000)), cancellationPromise]);
            } catch (ex) {
                console.warn('Error while rotating:', ex);
                this.currentImage = null;
                await Promise.race([new Promise((resolve) => window.setTimeout(resolve, 5000)), cancellationPromise]);
            }
        }
    }

    private _filter(asset: ImageAsset): boolean {
        const now: moment.Moment = moment();
        return asset.isActive
            && (asset.notBefore ? (moment.utc(asset.notBefore).isSameOrBefore(now, 'day')) : true)
            && (asset.notAfter ? (moment.utc(asset.notAfter).isSameOrAfter(now, 'day')) : true);
    }

    private _sort(lhs: ImageAsset, rhs: ImageAsset): number {
        if (lhs.order < rhs.order)
            return -1;
        if (lhs.order > rhs.order)
            return 1;
        return 0;
    }
}