export default interface IUpdateHtmlAssetResource {
    id: string;
    name: string;
    content: string;
    isActive: boolean;
    order: number;
    displayTime: number;
    notBefore: string;
    notAfter: string;
}