import React from 'react';
import { ReactElement } from 'react';
import { useObserver } from 'mobx-react';
import RotatorService from '../../services/RotatorService';

import neon from './../../../assets/images/Neon.jpg';
import './ImageAsset.less';

type Props = {
    rotator: RotatorService;
};

export default function ImageAsset(props: Props): ReactElement {
    return useObserver(() => {
        const imageUrl: string = props.rotator.currentContent ? props.rotator.currentContent : neon;
        return (
            <div className='image-asset'>
                <img src={imageUrl} />
            </div>
        );
    });
}