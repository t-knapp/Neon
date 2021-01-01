import React, { ReactElement } from 'react';

export default function Loading(): ReactElement {
    return (
        <div className='d-flex justify-content-center'>
            <div className='spinner-border text-primary' style={{width: '4rem', height: '4rem', marginTop: '2rem'}} role='status'>
                <span className='sr-only'>Loading...</span>
            </div>
        </div>
    );
}