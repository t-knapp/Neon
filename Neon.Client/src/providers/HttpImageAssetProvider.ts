import ImageAsset from '../models/ImageAsset';
import IImageAssetProvider from './IImageAssetProvider';
import AddressBuilder from '../services/AddressBuilder';

export default class HttpImageAssetProvider implements IImageAssetProvider {
    private _baseUrl: string;

    constructor(baseUrl: string) {
        this._baseUrl = baseUrl;
    }

    public get baseUrl(): string {
        return this._baseUrl;
    }

    public async allAsync(): Promise<ImageAsset[]> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).imageAssetAll().getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.json();
    }

    public oneAsync(id: string): Promise<ImageAsset> {
        throw new Error('Method not implemented.');
    }

    public async oneContentAsync(id: string): Promise<Blob> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).imageAssetContent(id).getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.blob();
    }
}