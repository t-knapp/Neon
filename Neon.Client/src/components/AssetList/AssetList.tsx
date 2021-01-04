import React, { ReactElement } from 'react';
import moment from 'moment';
import { NavLink } from 'react-router-dom';
import Asset, { EAssetType } from '../../models/Asset';
import IAssetProvider from '../../providers/IAssetProvider';
import Loading from '../Loading/Loading';
import ModalLoading from '../Loading/ModalLoading';

type Props = {
    provider: IAssetProvider;
};
type State = {
    assets: Asset[];
    initialized: boolean;
    loading: boolean;
};

export default class AssetList extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = {
            assets: [],
            initialized: false,
            loading: true
        };
    }

    public async componentDidMount(): Promise<void> {
        await this._fetchList();
        this.setState({initialized: true});
    }

    private async _fetchList(): Promise<void> {
        try {
            this.setState({loading: true});
            const assets: Asset[] = (await this.props.provider.allAsync()).sort(this._sort);
            this.setState({assets, loading: false});
        } catch {
            this.setState({assets: [], loading: false});
        }
    }

    private _sort(a: Asset, b: Asset): number {
        if (a.order < b.order)
            return -1;
        if (a.order > b.order)
            return 1;
        return 0;
    }

    private async _onDelete(id: string): Promise<void> {
        try {
            this.setState({loading: true});
            const deletedAsset: Asset = await this.props.provider.deleteOneAsync(id);
            const assets: Asset[] = this.state.assets.filter((asset: Asset) => asset.id !== deletedAsset.id);
            this.setState({assets});
        } catch (ex) {
            console.error('Exception deleting', id, ex);
        } finally {
            this.setState({loading: false});
        }
    }

    private async _onSetActive(id: string, isActive: boolean): Promise<void> {
        try {
            this.setState({loading: true});
            await this.props.provider.updateOneAsync(id, [{op: 'replace', path: 'isActive', value: isActive}]);
            await this._fetchList();
        } catch (ex) {
            console.error('Exception while setting state', id, ex);
        } finally {
            this.setState({loading: false});
        }
    }

    private async _onSetOrder(id: string, order: number): Promise<void> {
        try {
            this.setState({loading: true});
            await this.props.provider.updateOneAsync(id, [{op: 'replace', path: 'order', value: order}]);
            await this._fetchList();
        } catch (ex) {
            console.error('Exception while setting order', id, order);
        } finally {
            this.setState({loading: false});
        }
    }

    private _editRoute(asset: Asset): string {
        return `/edit/${(asset.type === EAssetType.IMAGE) ? 'image' : 'html'}/${asset.id}`;
    }

    private _type(asset: Asset): ReactElement {
        if (asset.type === EAssetType.IMAGE)
            return <i className='fas fa-file-image' />;
        
        if (asset.type === EAssetType.HTML)
            return <i className='fas fa-file-alt' />;
    }

    public render(): ReactElement {
        if (!this.state.initialized && this.state.loading)
            return <Loading />;

        const rows: ReactElement[] = this.state.assets.map((asset: Asset, index: number) => {
            const beforeOrder: number = ((this.state.assets[index - 1]?.order) ?? asset.order) - 1;
            const afterOrder: number = ((this.state.assets[index + 1]?.order) ?? asset.order) + 1;
            return (
                <tr key={asset.id}>
                    <td>
                        <button type='button' onClick={() => this._onSetOrder(asset.id, beforeOrder)} className='btn btn-outline-primary btn-sm' disabled={index === 0}><i className='fas fa-arrow-up'/></button>
                        &nbsp;
                        <button type='button' onClick={() => this._onSetOrder(asset.id, afterOrder)} className='btn btn-outline-primary btn-sm' disabled={index === this.state.assets.length - 1}><i className='fas fa-arrow-down'/></button>
                    </td>
                    <td>{this._type(asset)}</td>
                    <td>{asset.name}</td>
                    <td>{asset.displayTime}</td>
                    <td>{asset.notBefore ? moment.utc(asset.notBefore).format('DD.MM.YYYY') : ''}</td>
                    <td>{asset.notAfter ? moment.utc(asset.notAfter).format('DD.MM.YYYY') : ''}</td>
                    <td>{asset.isActive ? <span className='badge badge-primary'>Aktiv</span> : <span className='badge badge-secondary'>Inaktiv</span>}</td>
                    <td>
                        <button type='button' onClick={() => this._onSetActive(asset.id, !asset.isActive)} className='btn btn-outline-primary btn-sm'>
                            {asset.isActive ? 'Aus' : 'Ein'}
                        </button>
                        &nbsp;
                        <NavLink className='btn btn-outline-primary btn-sm' to={this._editRoute(asset)}><i className='fas fa-edit'/></NavLink>
                        &nbsp;
                        <button type='button' onClick={() => this._onDelete(asset.id)} className='btn btn-outline-danger btn-sm'><i className='fas fa-trash-alt'/></button>
                    </td>
                </tr>
            );
        });
        return (
            <>
            { this.state.loading && <ModalLoading /> }
            <table className='table table-hover'>
                <thead>
                    <tr>
                        <th scope='col'>Position</th>
                        <th scope='col'>Art</th>
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
            </>
        );
    }
}