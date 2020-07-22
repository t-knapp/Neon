export default interface IAddImageAssetResource {
    name: string;
    image: File;
    contextName: string;
    displayTime: number;
    notBefore: string;
    notAfter: string;
}