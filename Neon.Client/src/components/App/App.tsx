import { ReactElement } from 'react';
import React from 'react';
import './App.less';
import ImageAsset from '../ImageAsset/ImageAsset';
import RotatorService from '../../services/RotatorService';

type Props = {
    title: string,
    rotator: RotatorService;
};

export default function App(props: Props): ReactElement {
    return (
        <div className='AppComponent'>
            <ImageAsset rotator={props.rotator} />
        </div>
    );
}
