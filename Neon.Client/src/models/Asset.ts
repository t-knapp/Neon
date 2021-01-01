export default class Asset {
    public id: string;
    public name: string;
    public type: EAssetType;
    public isActive: boolean;
    public order: number;
    public displayTime: number;
    public notBefore: string;
    public notAfter: string;
}

export enum EAssetType {
    IMAGE,
    HTML
}