import React, { FunctionComponent, useState, ChangeEvent, FormEvent, useEffect } from 'react';
import { useParams, Redirect } from 'react-router-dom';
import HttpImageAssetProvider from '../../providers/HttpImageAssetProvider';
import ImageAsset from '../../models/ImageAsset';

type Props = {
    provider: HttpImageAssetProvider
};

const EditAsset: FunctionComponent<Props> = ({provider}) => {

    const { id } = useParams();
    const [state, setState] = useState({
        name: '',
        displayTime: 0,
        notBefore: '',
        notAfter: '',
        done: false
    });

    useEffect(() => {
        provider.oneAsync(id).then((asset: ImageAsset) => {
            setState((prevState) => ({...prevState, name: asset.name, displayTime: asset.displayTime, notBefore: asset.notBefore || '', notAfter: asset.notAfter || ''}));
        });
    }, []);

    const onFormSubmit = async (event: FormEvent<HTMLElement>): Promise<void> => {
        event.preventDefault();
        await provider.updateOneAsync({
            id,
            name: state.name,
            displayTime: state.displayTime,
            notBefore: state.notBefore ? state.notBefore : null,
            notAfter: state.notAfter ? state.notAfter : null
        });
        setState((prevState) => ({...prevState, done: true}));
    };

    return (
        <div className='EditAssetComponent'>
            { state.done &&
                <Redirect to='/assets' />
            }
            <form onSubmit={onFormSubmit}>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Name</label>
                    <div className='col-sm-8'>
                        <input type='text' className='form-control' value={state.name} onChange={(event: ChangeEvent<HTMLInputElement>) => {
                            event.persist();
                            setState(prevState => ({...prevState, name: event.target.value}));
                        }} />
                    </div>
                </div>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Anzeigezeit (Sekunden)</label>
                    <div className='col-sm-8'>
                        <input type='number' min='5' max='300' step='1' className='form-control' value={state.displayTime} onChange={(event: ChangeEvent<HTMLInputElement>) => {
                            event.persist();
                            setState(prevState => ({...prevState, displayTime: parseInt(event.target.value, 10)}));
                        }}/>
                    </div>
                </div>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Nicht anzeigen vor (optional)</label>
                    <div className='col-sm-8'>
                        <input type='date' className='form-control' value={state.notBefore} onChange={(event: ChangeEvent<HTMLInputElement>) => {
                            event.persist();
                            setState(prevState => ({...prevState, notBefore: event.target.value}));
                        }} />
                    </div>
                </div>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Nicht anzeigen nach (optional)</label>
                    <div className='col-sm-8'>
                        <input type='date' className='form-control' value={state.notAfter} onChange={(event: ChangeEvent<HTMLInputElement>) => {
                            event.persist();
                            setState(prevState => ({...prevState, notAfter: event.target.value}));
                        }} />
                    </div>
                </div>
                <div className='form-group row'>
                    <div className='col-sm-8'>
                        <button type='submit' className='btn btn-primary'>Speichern</button>
                    </div>
                </div>
            </form>
        </div>
    );
};

export default EditAsset;