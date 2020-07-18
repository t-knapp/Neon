import { observable, action } from 'mobx';
import HttpImageAssetProvider from '../providers/HttpImageAssetProvider';
import ImageAsset from '../models/ImageAsset';

export default class RotatorService {
    @observable public currentImage: Blob;

    private _index: number;
    private _provider: HttpImageAssetProvider;

    constructor(provider: HttpImageAssetProvider) {
        this._provider = provider;
        this._index = 0;
    }

    public start(): void {
        this._rotate();
    }

    public stop(): void {
        // TODO: ...
    }

    @action
    private async _rotate(): Promise<void> {
        try {
            const assetList: ImageAsset[] = await this._provider.allAsync();
            this._index = (this._index + 1) >= assetList.length ? 0 : this._index + 1;
            const asset: ImageAsset = assetList[this._index];
            this.currentImage = await this._provider.oneContentAsync(asset.id);

            window.setTimeout(() => this._rotate(), asset.displayTime * 1000);
        } catch (ex) {
            console.warn('rotate', ex);
        }
    }
}