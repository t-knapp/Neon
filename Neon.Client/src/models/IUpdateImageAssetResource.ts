export default interface IUpdateImageAssetResource {
    id: string;
    name?: string;
    isActive?: boolean;
    order?: number;
    displayTime?: number;
    notBefore?: string;
    notAfter?: string;
}