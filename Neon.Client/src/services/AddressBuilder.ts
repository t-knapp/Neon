export default class AddressBuilder {
    private readonly _baseUrl: string;
    private _path: string;

    public constructor(baseUrl: string) {
        this._baseUrl = baseUrl.endsWith('/') ? baseUrl : baseUrl + '/';
    }

    public assetAll(): AddressBuilder {
        this._path = 'assets';
        return this;
    }

    public asset(id: string): AddressBuilder {
        this._path = 'assets/' + id;
        return this;
    }

    public imageAssetAll(): AddressBuilder {
        this._path = 'imageassets';
        return this;
    }

    public imageAsset(id: string): AddressBuilder {
        this._path = 'imageassets/' + id;
        return this;
    }

    public imageAssetContent(id: string): AddressBuilder {
        this._path = 'imageassets/' + id + '/content';
        return this;
    }

    public htmlAssetAll(): AddressBuilder {
        this._path = 'htmlassets';
        return this;
    }

    public htmlAsset(id: string): AddressBuilder {
        this._path = 'htmlassets/' + id;
        return this;
    }

    public htmlAssetContent(id: string): AddressBuilder {
        this._path = 'htmlassets/' + id + '/content';
        return this;
    }

    public getUrl(): string {
        return this._baseUrl + this._path;
    }
}