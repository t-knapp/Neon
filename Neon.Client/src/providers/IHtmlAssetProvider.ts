import { Operation } from 'fast-json-patch';
import HtmlAsset from '../models/HtmlAsset';
import IAddHtmlAssetResource from '../models/IAddHtmlAssetResource';

export default interface IHtmlAssetProvider {
    allAsync(): Promise<HtmlAsset[]>;
    oneAsync(id: string): Promise<HtmlAsset>;
    oneContentAsync(id: string): Promise<string>;
    addOneAsync(resource: IAddHtmlAssetResource): Promise<HtmlAsset>;
    updateOneAsync(id: string, operations: Operation[]): Promise<HtmlAsset>;
    deleteOneAsync(id: string): Promise<HtmlAsset>;
}