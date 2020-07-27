import React, { ReactElement } from 'react';
import moment from 'moment';
import ImageAsset from '../../models/ImageAsset';
import HttpImageAssetProvider from '../../providers/HttpImageAssetProvider';
import { NavLink } from 'react-router-dom';

type Props = {
    provider: HttpImageAssetProvider;
};
type State = {
    imageAssets: ImageAsset[];
};

export default class AssetList extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = {
            imageAssets: []
        };
    }

    public componentDidMount(): void {
        this._fetchList();
    }

    private async _fetchList(): Promise<void> {
        try {
            const assets: ImageAsset[] = (await this.props.provider.allAsync()).sort(this._sort);
            this.setState({
                imageAssets: assets
            });
        } catch {
            this.setState({imageAssets: []});
        }
    }

    private _sort(a: ImageAsset, b: ImageAsset): number {
        if (a.order < b.order)
            return -1;
        if (a.order > b.order)
            return 1;
        return 0;
    }

    private async _onDelete(id: string): Promise<void> {
        try {
            const deletedAsset: ImageAsset = await this.props.provider.deleteOneAsync(id);
            const assets: ImageAsset[] = this.state.imageAssets.filter((asset: ImageAsset) => asset.id !== deletedAsset.id);
            this.setState({imageAssets: assets});
        } catch (ex) {
            console.error('Exception deleting', id, ex);
        }
    }

    private async _onSetActive(id: string, isActive: boolean): Promise<void> {
        try {
            await this.props.provider.updateOneAsync({id, isActive});
            await this._fetchList();
        } catch (ex) {
            console.error('Exception while setting state', id, ex);
        }
    }

    private async _onSetOrder(id: string, order: number): Promise<void> {
        try {
            await this.props.provider.updateOneAsync({id, order});
            await this._fetchList();
        } catch (ex) {
            console.error('Exception while setting order', id, order);
        }
    }

    private _onEdit(id: string): void {
        
    }

    public render(): ReactElement {
        const rows: ReactElement[] = this.state.imageAssets.map((asset: ImageAsset, index: number) => {
            const beforeOrder: number = ((this.state.imageAssets[index - 1]?.order) ?? asset.order) - 1;
            const afterOrder: number = ((this.state.imageAssets[index + 1]?.order) ?? asset.order) + 1;
            return (
                <tr key={asset.id}>
                    <td>
                        <button type='button' onClick={() => this._onSetOrder(asset.id, beforeOrder)} className='btn btn-outline-primary btn-sm' disabled={index === 0}><i className='fas fa-arrow-up'/></button>
                        &nbsp;
                        <button type='button' onClick={() => this._onSetOrder(asset.id, afterOrder)} className='btn btn-outline-primary btn-sm' disabled={index === this.state.imageAssets.length - 1}><i className='fas fa-arrow-down'/></button>
                        &nbsp;
                        {asset.name}
                    </td>
                    <td>{asset.displayTime}</td>
                    <td>{asset.notBefore ? moment.utc(asset.notBefore).format('DD.MM.YYYY') : ''}</td>
                    <td>{asset.notAfter ? moment.utc(asset.notAfter).format('DD.MM.YYYY') : ''}</td>
                    <td>{asset.isActive ? <span className='badge badge-primary'>Aktiv</span> : <span className='badge badge-secondary'>Inaktiv</span>}</td>
                    <td>
                        <button type='button' onClick={() => this._onSetActive(asset.id, !asset.isActive)} className='btn btn-outline-primary btn-sm'>
                            {asset.isActive ? 'Aus' : 'Ein'}
                        </button>
                        &nbsp;
                        <NavLink className='btn btn-outline-primary btn-sm' to={'/edit/' + asset.id}><i className='fas fa-edit'/></NavLink>
                        &nbsp;
                        <button type='button' onClick={() => this._onDelete(asset.id)} className='btn btn-outline-danger btn-sm'><i className='fas fa-trash-alt'/></button>
                    </td>
                </tr>
            );
        });
        return (
            <table className='table table-hover'>
                <thead>
                    <tr>
                        <th scope='col'>Name</th>
                        <th scope='col'>Anzeigedauer</th>
                        <th scope='col'>Nicht zeigen vor</th>
                        <th scope='col'>Nicht zeigen nach</th>
                        <th scope='col'>Status</th>
                        <th scope='col'/>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
        );
    }
}