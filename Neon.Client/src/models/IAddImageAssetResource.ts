export default interface IAddImageAssetResource {
    name: string;
    image: File;
    isActive: boolean;
    order: number;
    displayTime: number;
    notBefore: string;
    notAfter: string;
}