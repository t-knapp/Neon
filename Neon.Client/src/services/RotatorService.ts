import { observable, action } from 'mobx';
import HttpImageAssetProvider from '../providers/HttpImageAssetProvider';
import ImageAsset from '../models/ImageAsset';
import AddressBuilder from './AddressBuilder';

export default class RotatorService {
    @observable public currentImageAssetUrl: string;

    private _index: number;
    private _provider: HttpImageAssetProvider;
    private _addressBuilder: AddressBuilder;

    constructor(provider: HttpImageAssetProvider) {
        this._provider = provider;
        this._index = 0;
        this._addressBuilder = new AddressBuilder(provider.baseUrl);
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
            this.currentImageAssetUrl = this._addressBuilder.imageAssetContent(item.id).getUrl();
            window.setTimeout(() => this._rotate(), item.displayTime * 1000);
        } catch (ex) {
            this.currentImageAssetUrl = '';
        }
    }
}