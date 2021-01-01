import { ReactElement } from 'react';
import React from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route,
  } from 'react-router-dom';
import RotatorService from '../../services/RotatorService';

import 'bootstrap/dist/css/bootstrap.min.css';
import AssetList from '../AssetList/AssetList';
import AddAsset from '../AddAsset/AddAsset';
import Menu from '../Menu/Menu';
import EditAsset from '../EditAsset/EditAsset';
import AddHtml from '../AddHtml/AddHtml';
import IHtmlAssetProvider from '../../providers/IHtmlAssetProvider';
import IAssetProvider from '../../providers/IAssetProvider';
import IImageAssetProvider from '../../providers/IImageAssetProvider';
import AssetWrapper from '../AssetWrapper/AssetWrapper';

type Props = {
    title: string,
    rotator: RotatorService;
    assetProvider: IAssetProvider;
    imageProvider: IImageAssetProvider;
    htmlProvider: IHtmlAssetProvider;
};

export default class App extends React.Component<Props> {
    public render(): ReactElement {
        return (
            <Router>
                <div>
                    <Switch>
                        <Route path='/assets'>
                            <div className='container-fluid'>
                                <Menu>
                                    <AssetList provider={this.props.assetProvider} />
                                </Menu>
                            </div>
                        </Route>
                        <Route path='/add/html'>
                            <div className='container-fluid'>
                                <Menu>
                                    <AddHtml provider={this.props.htmlProvider} />
                                </Menu>
                            </div>
                        </Route>
                        <Route path='/add/image'>
                            <div className='container-fluid'>
                                <Menu>
                                    <AddAsset provider={this.props.imageProvider} />
                                </Menu>
                            </div>
                        </Route>
                        <Route path='/edit/:id'>
                            <div className='container-fluid'>
                                <Menu>
                                    <EditAsset provider={this.props.imageProvider} />
                                </Menu>
                            </div>
                        </Route>
                        <Route path='/'>
                            <div className='container-fluid' style={{paddingLeft: 0, paddingRight: 0}} >
                                <AssetWrapper rotator={this.props.rotator} />
                            </div>
                        </Route>
                    </Switch>
                </div>
            </Router>
        );
    }
};