import React from 'react';
import ReactDOM from 'react-dom';

import '@fortawesome/fontawesome-free/js/fontawesome';
import '@fortawesome/fontawesome-free/js/solid';
import '@fortawesome/fontawesome-free/js/regular';
import '@fortawesome/fontawesome-free/js/brands';

import App from './components/App/App';
import RotatorService from './services/RotatorService';
import HttpImageAssetProvider from './providers/HttpImageAssetProvider';

const PROVIDER: HttpImageAssetProvider = new HttpImageAssetProvider('https://localhost:5001/');
const ROTATOR: RotatorService = new RotatorService(PROVIDER);

ReactDOM.render (
    <App title='Neon.Client' rotator={ROTATOR} provider={PROVIDER} />,
    document.getElementById('root')
);
