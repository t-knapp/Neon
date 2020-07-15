export default class AddressBuilder {
    private readonly _baseUrl: string;
    private _path: string;

    public constructor(baseUrl: string) {
        this._baseUrl = baseUrl.endsWith('/') ? baseUrl : baseUrl + '/';
    }

    public imageAssetAll(): AddressBuilder {
        this._path = 'imageassets';
        return this;
    }

    public imageAssetContent(id: string): AddressBuilder {
        this._path = 'imageassets/' + id + '/content';
        return this;
    }

    public getUrl(): string {
        return this._baseUrl + this._path;
    }
}