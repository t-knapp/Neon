import { useObserver } from 'mobx-react';
import React, { ReactElement } from 'react';
import RotatorService from '../../services/RotatorService';
import './HtmlAsset.less';

type Props = {
    rotator: RotatorService;
};

export default function HtmlAsset(props: Props): ReactElement {
    return useObserver(() => (
        <div className={'html-asset animate__animated ' + (props.rotator.available) ? 'animate__fadeIn' : 'animate__fadeOut' }>
            <div className='content' dangerouslySetInnerHTML={{__html: props.rotator.currentContent}} />
        </div>
    ));
}