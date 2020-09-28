import React, { FunctionComponent, useState, ChangeEvent, FormEvent, useEffect } from 'react';
import { useParams, Redirect } from 'react-router-dom';
import moment from 'moment';
import { compare } from 'fast-json-patch';
import HttpImageAssetProvider from '../../providers/HttpImageAssetProvider';
import ImageAsset from '../../models/ImageAsset';
import IUpdateImageAssetResource from '../../models/IUpdateImageAssetResource';
import { toString } from '../../helpers/BlobHelper';

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
        base64image: '',
        done: false,
        originalAsset: null
    });

    useEffect(() => {
        provider.oneAsync(id)
            .then(async (asset: ImageAsset) => {
                const imageData: string = await toString(await provider.oneContentAsync(asset.id));
                setState((prevState) => ({
                    ...prevState,
                    originalAsset: asset,
                    name: asset.name,
                    displayTime: asset.displayTime,
                    notBefore: asset.notBefore ? moment.utc(asset.notBefore).format('YYYY-MM-DD') : '',
                    notAfter: asset.notAfter ? moment.utc(asset.notBefore).format('YYYY-MM-DD') : '',
                    base64image: imageData
                }));
            });
    }, []);

    const onFormSubmit = async (event: FormEvent<HTMLElement>): Promise<void> => {
        event.preventDefault();
        const newAsset: IUpdateImageAssetResource = {
            id: id,
            name: state.name,
            displayTime: state.displayTime,
            notBefore: state.notBefore,
            notAfter: state.notAfter,
            isActive: state.originalAsset.isActive,
            order: state.originalAsset.order,
        };
        await provider.updateOneAsync(id, compare(state.originalAsset, newAsset));
        setState((prevState) => ({...prevState, done: true}));
    };

    return (
        <div className='EditAssetComponent'>
            { state.done &&
                <Redirect to='/assets' />
            }
            <form onSubmit={onFormSubmit}>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Vorschau</label>
                    <div className='col-sm-4'>
                        <div className='card bg-dark text-white'>
                            <img src={state.base64image} className='card-img' alt='Vorschau-Bild'></img>
                        </div>
                    </div>
                    <div className='col-sm-4'></div>
                </div>
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