import ImageAsset from '../models/ImageAsset';
import IImageAssetProvider from './IImageAssetProvider';

export default class HttpImageAssetProvider implements IImageAssetProvider {

    private _baseUrl: string;

    // TODO: AddressProvider?

    constructor(baseUrl: string) {
        this._baseUrl = baseUrl;
    }

    public async allAsync(): Promise<ImageAsset[]> {
        const request: Request = new Request(this._baseUrl + 'imageassets');
        const result: Response = await fetch(request);
        if (result.status === 200)
            return await result.json();
    }

    public oneAsync(id: string): Promise<ImageAsset> {
        throw new Error('Method not implemented.');
    }
}