import { ReactElement } from 'react';
import React from 'react';
import { useObserver } from 'mobx-react';
import './ImageAsset.less';
import RotatorService from '../../services/RotatorService';

type Props = {
    rotator: RotatorService;
};

export default function ImageAsset(props: Props): ReactElement {
    return useObserver(() => (
        <div className='ImageAssetComponent'>
            <img src={props.rotator.currentImageAssetUrl} />
        </div>
    ));
}