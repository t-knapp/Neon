import { observable, action } from 'mobx';
import IImageAssetProvider from '../providers/IImageAssetProvider';
import ImageAsset from '../models/ImageAsset';

export default class RotatorService {
    @observable public currentImageAssetUrl: string;

    private _index: number;
    private _provider: IImageAssetProvider;

    constructor(provider: IImageAssetProvider) {
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
            const list: ImageAsset[] = await this._provider.allAsync();
            this._index = (this._index + 1) >= list.length ? 0 : this._index + 1;
            const item: ImageAsset = list[this._index];
            console.log('item', item);
            this.currentImageAssetUrl = 'https://localhost:5001/imageassets/' + item.id + '/content';
            window.setTimeout(() => this._rotate(), item.displayTime * 1000);
        } catch (ex) {
            this.currentImageAssetUrl = '';
        }
    }
}