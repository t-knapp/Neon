export default interface IAddHtmlAssetResource {
    name: string;
    content: string;
    isActive: boolean;
    order: number;
    displayTime: number;
    notBefore: string;
    notAfter: string;
}