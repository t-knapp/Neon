import { observable, action } from 'mobx';
import moment from 'moment';
import { toString } from '../helpers/BlobHelper';
import Asset, { EAssetType } from '../models/Asset';
import IAssetProvider from '../providers/IAssetProvider';
import IHtmlAssetProvider from '../providers/IHtmlAssetProvider';
import IImageAssetProvider from '../providers/IImageAssetProvider';

export default class RotatorService {
    @observable public available: boolean;
    @observable public assetType: EAssetType;
    @observable public currentContent: string;
    @observable public hasError: boolean;

    private _index: number;
    private _running: boolean;
    private _assetProvider: IAssetProvider;
    private _imageProvider: IImageAssetProvider;
    private _htmlProvider: IHtmlAssetProvider;
    private _cancellation: () => void;

    constructor(assetProvider: IAssetProvider, imageProvider: IImageAssetProvider, htmlProvider: IHtmlAssetProvider) {
        this._index = 0;
        this._running = false;
        this._assetProvider = assetProvider;
        this._imageProvider = imageProvider;
        this._htmlProvider = htmlProvider;
    }

    public start(): void {
        this._running = true;
        this._rotate(new Promise((resolve) => {
            this._cancellation = resolve;
        }));
    }

    public stop(): void {
        this._running = false;
        this._cancellation();
    }

    @action
    private async _rotate(cancellationPromise: Promise<void>): Promise<void> {
        while (this._running) {
            try {
                const assetList: Asset[] = (await this._assetProvider.allAsync())
                    .filter(this._filter)
                    .sort(this._sort);
                if (assetList.length === 0) {
                    this.currentContent = null;
                    await Promise.race([new Promise((resolve) => window.setTimeout(resolve, 5000)), cancellationPromise]);
                    continue;
                }
                this._index = (this._index + 1) >= assetList.length ? 0 : this._index + 1;
                const asset: Asset = assetList[this._index];
                this.available = false;
                await new Promise((resolve) => window.setTimeout(resolve, 333));
                this.currentContent = await this._content(asset);
                this.assetType = asset.type;
                this.available = true;
                await Promise.race([new Promise((resolve) => window.setTimeout(resolve, asset.displayTime * 1000)), cancellationPromise]);
                this.hasError = false;
            } catch (ex) {
                console.warn('Error while rotating:', ex);
                this.hasError = true;
                this.currentContent = null;
                await Promise.race([new Promise((resolve) => window.setTimeout(resolve, 5000)), cancellationPromise]);
            }
        }
    }

    private async _content(asset: Asset): Promise<string> {
        if (asset.type === EAssetType.IMAGE)
            return toString(await this._imageProvider.oneContentAsync(asset.id));

        if (asset.type === EAssetType.HTML)
            return this._htmlProvider.oneContentAsync(asset.id);
    }

    private _filter(asset: Asset): boolean {
        const now: moment.Moment = moment();
        return asset.isActive
            && (asset.notBefore ? (moment.utc(asset.notBefore).isSameOrBefore(now, 'day')) : true)
            && (asset.notAfter ? (moment.utc(asset.notAfter).isSameOrAfter(now, 'day')) : true);
    }

    private _sort(lhs: Asset, rhs: Asset): number {
        if (lhs.order < rhs.order)
            return -1;
        if (lhs.order > rhs.order)
            return 1;
        return 0;
    }
}