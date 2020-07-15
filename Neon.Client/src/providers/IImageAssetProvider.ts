import ImageAsset from '../models/ImageAsset';

export default interface IImageAssetProvider {
    allAsync(): Promise<ImageAsset[]>;
    oneAsync(id: string): Promise<ImageAsset>;
}