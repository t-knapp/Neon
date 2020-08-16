import { ReactElement } from 'react';
import React from 'react';
import { observer } from 'mobx-react';
import './ImageAsset.less';
import RotatorService from '../../services/RotatorService';
import { reaction, IReactionDisposer } from 'mobx';

type Props = {
    rotator: RotatorService;
    onClick: () => void;
};

@observer
export default class ImageAsset extends React.Component<Props> {

    private _disposer: IReactionDisposer;
    private _lastUrl: string;

    public componentDidMount(): void {
        this._disposer = reaction(
            () => this.props.rotator.currentImage,
            () => {
                if (this._lastUrl)
                    URL.revokeObjectURL(this._lastUrl);
            }
        );
        this.props.rotator.start();
    }

    public componentWillUnmount(): void {
        if (this._disposer)
            this._disposer();
        
        this.props.rotator.stop();
    }

    public render(): ReactElement {
        const imageUrl: string = this.props.rotator.currentImage ? URL.createObjectURL(this.props.rotator.currentImage) : '';
        this._lastUrl = imageUrl;
        return (
            <div className='ImageAssetComponent' onClick={this.props.onClick}>
                <img src={imageUrl} />
            </div>
        );
    }
}