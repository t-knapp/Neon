import ImageAsset from '../models/ImageAsset';
import IImageAssetProvider from './IImageAssetProvider';
import AddressBuilder from '../services/AddressBuilder';
import IAddImageAssetResource from '../models/IAddImageAssetResource';
import IUpdateImageAssetResource from '../models/IUpdateImageAssetResource';

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

    public async oneAsync(id: string): Promise<ImageAsset> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).imageAsset(id).getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.json();
    }

    public async oneContentAsync(id: string): Promise<Blob> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).imageAssetContent(id).getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.blob();
    }

    public async deleteOneAsync(id: string): Promise<ImageAsset> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).imageAsset(id).getUrl());
        const result: Response = await fetch(request, {method: 'DELETE'});
        if (result.status === 200)
            return await result.json();
    }

    public async addOneAsync(asset: IAddImageAssetResource): Promise<ImageAsset> {
        const formData: FormData = new FormData();
        formData.append('Name', asset.name);
        formData.append('Order', asset.order.toString());
        formData.append('IsActive', asset.isActive ? 'true' : 'false');
        formData.append('DisplayTime', asset.displayTime.toString());
        formData.append('Image', asset.image);
        if (asset.notBefore)
            formData.append('NotBefore', asset.notBefore);
        if (asset.notAfter)
            formData.append('NotAfter', asset.notAfter);

        const result: Response = await fetch(
            new AddressBuilder(this._baseUrl).imageAssetAll().getUrl(),
            {
                method: 'POST',
                body: formData
            }
        );
        if (result.status === 200)
            return await result.json();
    }

    public async updateOneAsync(asset: IUpdateImageAssetResource): Promise<ImageAsset> {
        const result: Response = await fetch(
            new AddressBuilder(this._baseUrl).imageAssetAll().getUrl(),
            {
                method: 'PATCH',
                body: JSON.stringify(asset),
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );
        if (result.status === 200)
            return await result.json();
    }
}