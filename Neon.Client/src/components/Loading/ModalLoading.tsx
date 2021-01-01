import React, { ReactElement } from 'react';
import Loading from './Loading';
import './ModalLoading.less';

export default function ModalLoading(): ReactElement {
    return (
        <div className='modal-loading'>
            <div className='content'>
                <Loading />
            </div>
        </div>
    );
}