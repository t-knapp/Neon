import { ReactElement } from 'react';
import React from 'react';
import emoji from '../../../assets/images/10-denker-rcm600x0.jpg';
import './App.less';
import ImageAsset from '../ImageAsset/ImageAsset';

type Props = {
    title: string,
};

export default function App(props: Props): ReactElement {
    return (
        <div className='AppComponent'>
            <h1>{props.title}</h1>
            <p>
                <img src={emoji} width={18} height={18} alt={'Emoji nachdenklich'} />
            </p>
            <ImageAsset />
        </div>
    );
}
