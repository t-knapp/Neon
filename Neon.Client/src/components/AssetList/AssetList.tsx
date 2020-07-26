import React, { ReactElement } from 'react';
import moment from 'moment';
import ImageAsset from '../../models/ImageAsset';
import HttpImageAssetProvider from '../../providers/HttpImageAssetProvider';

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
            const assets: ImageAsset[] = await this.props.provider.allAsync();
            this.setState({
                imageAssets: assets
            });
        } catch {
            this.setState({imageAssets: []});
        }
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

    private async _onSetActive(id: string, state: boolean): Promise<void> {
        try {
            await this.props.provider.updateOneAsync({id, isActive: state});
            await this._fetchList();
        } catch (ex) {
            console.error('Exception patching', id, ex);
        }
    }

    public render(): ReactElement {
        const rows: ReactElement[] = this.state.imageAssets.map((asset: ImageAsset) => (
            <tr key={asset.id}>
                <td>{asset.id}</td>
                <td>{asset.name}</td>
                <td>{asset.displayTime}</td>
                <td>{asset.notBefore ? moment.utc(asset.notBefore).format('DD.MM.YYYY') : ''}</td>
                <td>{asset.notAfter ? moment.utc(asset.notAfter).format('DD.MM.YYYY') : ''}</td>
                <td>{asset.isActive ? <span className='badge badge-primary'>Aktiv</span> : <span className='badge badge-secondary'>Inaktiv</span>}</td>
                <td>
                    <button type='button' onClick={() => this._onSetActive(asset.id, !(!!asset.isActive))} className='btn btn-primary btn-sm'>{asset.isActive ? 'Deaktivieren' : 'Aktivieren'}</button>
                    <button type='button' onClick={() => this._onDelete(asset.id)} className='btn btn-danger btn-sm'>Löschen</button>
                </td>
            </tr>
        ));
        return (
            <table className='table table-hover'>
                <thead>
                    <tr>
                        <th scope='col'>#</th>
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