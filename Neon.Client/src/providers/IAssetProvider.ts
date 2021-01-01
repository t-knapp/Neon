import { Operation } from 'fast-json-patch';
import Asset from '../models/Asset';

export default interface IAssetProvider {
    allAsync(): Promise<Asset[]>;
    oneAsync(id: string): Promise<Asset>;
    updateOneAsync(id: string, operations: Operation[]): Promise<Asset>;
    deleteOneAsync(id: string): Promise<Asset>;
}