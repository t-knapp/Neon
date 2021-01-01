import { Operation } from 'fast-json-patch';
import ImageAsset from '../models/ImageAsset';
import IAddImageAssetResource from '../models/IAddImageAssetResource';

export default interface IImageAssetProvider {
    allAsync(): Promise<ImageAsset[]>;
    oneAsync(id: string): Promise<ImageAsset>;
    oneContentAsync(id: string): Promise<Blob>;
    addOneAsync(resource: IAddImageAssetResource): Promise<ImageAsset>;
    updateOneAsync(id: string, operations: Operation[]): Promise<ImageAsset>;
    deleteOneAsync(id: string): Promise<ImageAsset>;
}