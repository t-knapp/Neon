import React from 'react';
import ReactDOM from 'react-dom';

import App from './components/App/App';
import RotatorService from './services/RotatorService';
import HttpImageAssetProvider from './providers/HttpImageAssetProvider';

const ROTATOR: RotatorService = new RotatorService(new HttpImageAssetProvider('https://localhost:5001/'));
ROTATOR.start();

ReactDOM.render (
    <App title='Neon.Client' rotator={ROTATOR} />,
    document.getElementById('root')
);
