import { ReactElement } from 'react';
import React from 'react';
import { observer } from 'mobx-react';
import RotatorService from '../../services/RotatorService';
import neon from './../../../assets/images/Neon.jpg';

import './ImageAsset.less';

type Props = {
    rotator: RotatorService;
    onClick: () => void;
};

@observer
export default class ImageAsset extends React.Component<Props> {

    public componentDidMount(): void {
        this.props.rotator.start();
    }

    public componentWillUnmount(): void {
        this.props.rotator.stop();
    }

    public render(): ReactElement {
        const imageUrl: string = this.props.rotator.currentImage ? this.props.rotator.currentImage : neon;
        return (
            <div className='ImageAssetComponent' onClick={this.props.onClick}>
                <img src={imageUrl} />
            </div>
        );
    }
}