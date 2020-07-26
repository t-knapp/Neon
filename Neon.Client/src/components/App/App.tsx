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
                    <div className='container-fluid'>
                        <Switch>
                            <Route path='/assets'>
                                <Menu>
                                    <AssetList provider={this.props.provider} />
                                </Menu>
                            </Route>
                            <Route path='/add'>
                                <Menu>
                                    <AddAsset provider={this.props.provider} />
                                </Menu>
                            </Route>
                            <Route path='/'>
                                <ImageAssetWrapper rotator={this.props.rotator} />
                            </Route>
                        </Switch>
                    </div>
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