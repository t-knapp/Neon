import { ReactElement } from 'react';
import React from 'react';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Link
  } from 'react-router-dom';
import './App.less';
import ImageAsset from '../ImageAsset/ImageAsset';
import RotatorService from '../../services/RotatorService';
import Management from '../Management/Management';

import 'bootstrap/dist/css/bootstrap.min.css';

type Props = {
    title: string,
    rotator: RotatorService;
};

export default class App extends React.Component<Props> {
    public render(): ReactElement {
        return (
            <Router>
                <div className='AppComponent'>
                    <nav>
                        <ul>
                            <li>
                                <Link to='/'>Home</Link>
                            </li>
                            <li>
                                <Link to='/admin'>Verwaltung</Link>
                            </li>
                        </ul>
                    </nav>
                    <Switch>
                        <Route path='/admin'>
                            <Management />
                        </Route>
                        <Route path='/'>
                            <ImageAsset rotator={this.props.rotator} />
                        </Route>
                    </Switch>
                </div>
            </Router>
        );
    }
}
