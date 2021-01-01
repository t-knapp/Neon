import { Operation } from 'fast-json-patch';
import Asset from '../models/Asset';
import AddressBuilder from '../services/AddressBuilder';
import IAssetProvider from './IAssetProvider';

export default class HttpAssetProvider implements IAssetProvider {

    private readonly _baseUrl: string;

    constructor(baseUrl: string) {
        this._baseUrl = baseUrl;
    }

    public async allAsync(): Promise<Asset[]> {
        const request: Request = new Request(new AddressBuilder(this._baseUrl).assetAll().getUrl());
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.json();
    }

    public oneAsync(id: string): Promise<Asset> {
        throw new Error('Method not implemented.');
    }

    public async updateOneAsync(id: string, operations: Operation[]): Promise<Asset> {
        const result: Response = await fetch(
            new AddressBuilder(this._baseUrl).asset(id).getUrl(),
            {
                method: 'PATCH',
                body: JSON.stringify(operations),
                headers: {
                    'Content-Type': 'application/json-patch+json'
                }
            }
        );
        if (result.status === 200)
            return await result.json();
    }

    public async deleteOneAsync(id: string): Promise<Asset> {
        const result: Response = await fetch(
            new AddressBuilder(this._baseUrl).asset(id).getUrl(), {method: 'DELETE'}
        );
        if (result.status === 200)
            return await result.json();
    }
}