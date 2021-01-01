import { useObserver } from 'mobx-react';
import React, { ReactElement, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { EAssetType } from '../../models/Asset';
import RotatorService from '../../services/RotatorService';
import HtmlAsset from '../HtmlAsset/HtmlAsset';
import ImageAsset from '../ImageAsset/ImageAsset';
import './AssetWrapper.less';

type Props = {
    rotator: RotatorService
};
export default function AssetWrapper(props: Props): ReactElement {
    const history = useHistory();

    const onClick: () => void = () => {
        history.push('/assets');
    };

    // The "method" formerly known as componentDidMount
    useEffect(() => props.rotator.start(), []);

    // The "method" formerly known as componentWillUnmount
    useEffect(() => {
        return () => {
            props.rotator.stop();
        };
    }, []);

    return useObserver(() => {
        const content: ReactElement = (props.rotator.available)
        ? (props.rotator.assetType === EAssetType.IMAGE)
            ? <ImageAsset rotator={props.rotator} />
            : <HtmlAsset rotator={props.rotator} />
        : null;
        return (
            <div className='asset-wrapper' onClick={onClick}>
                {content}
            </div>
        );
    });
}