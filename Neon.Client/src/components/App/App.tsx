import { ReactElement } from 'react';
import React from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    useHistory
  } from 'react-router-dom';
import './App.less';
import ImageAsset from '../ImageAsset/ImageAsset';
import RotatorService from '../../services/RotatorService';

import 'bootstrap/dist/css/bootstrap.min.css';
import HttpImageAssetProvider from '../../providers/HttpImageAssetProvider';
import AssetList from '../AssetList/AssetList';
import AddAsset from '../AddAsset/AddAsset';
import Menu from '../Menu/Menu';
import EditAsset from '../EditAsset/EditAsset';

type Props = {
    title: string,
    rotator: RotatorService;
    provider: HttpImageAssetProvider;
};

export default class App extends React.Component<Props> {
    public render(): ReactElement {
        return (
            <Router>
                <div className='AppComponent'>
                    <Switch>
                        <Route path='/assets'>
                            <div className='container-fluid'>
                                <Menu>
                                    <AssetList provider={this.props.provider} />
                                </Menu>
                            </div>
                        </Route>
                        <Route path='/add'>
                            <div className='container-fluid'>
                                <Menu>
                                    <AddAsset provider={this.props.provider} />
                                </Menu>
                            </div>
                        </Route>
                        <Route path='/edit/:id'>
                            <div className='container-fluid'>
                                <Menu>
                                    <EditAsset provider={this.props.provider} />
                                </Menu>
                            </div>
                        </Route>
                        <Route path='/'>
                            <div className='container-fluid' style={{paddingLeft: 0, paddingRight: 0}} >
                                <ImageAssetWrapper rotator={this.props.rotator} />
                            </div>
                        </Route>
                    </Switch>
                </div>
            </Router>
        );
    }
}

type WrapperProps = {
    rotator: RotatorService
};
function ImageAssetWrapper(props: WrapperProps): ReactElement {
    const history = useHistory();

    const onClick: () => void = () => {
        history.push('/assets');
    };

    return (
        <React.Fragment>
            <ImageAsset rotator={props.rotator} onClick={onClick} />
        </React.Fragment>
    );
}