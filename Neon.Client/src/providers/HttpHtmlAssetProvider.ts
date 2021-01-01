import { Operation } from 'fast-json-patch';
import HtmlAsset from '../models/HtmlAsset';
import IAddHtmlAssetResource from '../models/IAddHtmlAssetResource';
import AddressBuilder from '../services/AddressBuilder';
import IHtmlAssetProvider from './IHtmlAssetProvider';

export default class HttpHtmlAssetProvider implements IHtmlAssetProvider {

    private _baseUrl: string;

    constructor(baseUrl: string) {
        this._baseUrl = baseUrl;
    }

    public get baseUrl(): string {
        return this._baseUrl;
    }

    public async allAsync(): Promise<HtmlAsset[]> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).htmlAssetAll().getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.json();
    }

    public async oneAsync(id: string): Promise<HtmlAsset> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).htmlAsset(id).getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.json();
    }

    public async oneContentAsync(id: string): Promise<string> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).htmlAssetContent(id).getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.text();
    }

    public async addOneAsync(asset: IAddHtmlAssetResource): Promise<HtmlAsset> {
        const formData: FormData = new FormData();
        formData.append('Name', asset.name);
        formData.append('Order', asset.order.toString());
        formData.append('IsActive', asset.isActive ? 'true' : 'false');
        formData.append('DisplayTime', asset.displayTime.toString());
        formData.append('Content', asset.content);
        if (asset.notBefore)
            formData.append('NotBefore', asset.notBefore);
        if (asset.notAfter)
            formData.append('NotAfter', asset.notAfter);

        const result: Response = await fetch(
            new AddressBuilder(this._baseUrl).htmlAssetAll().getUrl(),
            {
                method: 'POST',
                body: formData
            }
        );
        if (result.status === 200)
            return await result.json();
    }

    public updateOneAsync(id: string, operations: Operation[]): Promise<HtmlAsset> {
        throw new Error('Method not implemented.');
    }

    public deleteOneAsync(id: string): Promise<HtmlAsset> {
        throw new Error('Method not implemented.');
    }
}