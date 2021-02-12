import React from 'react';
import ReactDOM from 'react-dom';
import 'bootstrap';
import '@fortawesome/fontawesome-free/js/fontawesome';
import '@fortawesome/fontawesome-free/js/solid';
import '@fortawesome/fontawesome-free/js/regular';
import '@fortawesome/fontawesome-free/js/brands';
import 'animate.css';

import App from './components/App/App';
import RotatorService from './services/RotatorService';
import HttpImageAssetProvider from './providers/HttpImageAssetProvider';
import IHtmlAssetProvider from './providers/IHtmlAssetProvider';
import HttpHtmlAssetProvider from './providers/HttpHtmlAssetProvider';
import IAssetProvider from './providers/IAssetProvider';
import HttpAssetProvider from './providers/HttpAssetProvider';

const BASEURL: string = 'http://localhost:5000';

const ASSETPROVIDER: IAssetProvider = new HttpAssetProvider(BASEURL);
const IMAGEPROVIDER: HttpImageAssetProvider = new HttpImageAssetProvider(BASEURL);
const HTMLPROVIDER: IHtmlAssetProvider = new HttpHtmlAssetProvider(BASEURL);
const ROTATOR: RotatorService = new RotatorService(ASSETPROVIDER, IMAGEPROVIDER, HTMLPROVIDER);

ReactDOM.render (
    <App title='Neon.Client' rotator={ROTATOR} assetProvider={ASSETPROVIDER} htmlProvider={HTMLPROVIDER} imageProvider={IMAGEPROVIDER} />,
    document.getElementById('root')
);
