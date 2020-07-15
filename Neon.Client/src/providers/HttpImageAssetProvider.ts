import ImageAsset from '../models/ImageAsset';
import IImageAssetProvider from './IImageAssetProvider';
import AddressBuilder from '../services/AddressBuilder';

export default class HttpImageAssetProvider implements IImageAssetProvider {
    private _baseUrl: string;
    private _addressBuilder: AddressBuilder;

    constructor(baseUrl: string) {
        this._baseUrl = baseUrl;
        this._addressBuilder = new AddressBuilder(baseUrl);
        this._addressBuilder.imageAssetAll();
    }

    public get baseUrl(): string {
        return this._baseUrl;
    }

    public async allAsync(): Promise<ImageAsset[]> {
        const request: Request = new Request(this._addressBuilder.getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.json();
    }

    public oneAsync(id: string): Promise<ImageAsset> {
        throw new Error('Method not implemented.');
    }
}