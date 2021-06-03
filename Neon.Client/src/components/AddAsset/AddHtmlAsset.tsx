import React, { ReactElement, useState } from 'react';
import { Redirect } from 'react-router-dom';
import SunEditor, { buttonList } from 'suneditor-react';
import IAddHtmlAssetResource from '../../models/IAddHtmlAssetResource';
import IHtmlAssetProvider from '../../providers/IHtmlAssetProvider';
import ModalLoading from '../Loading/ModalLoading';

type Props = {
    provider: IHtmlAssetProvider;
};

enum ADDSTATE {
    NONE,
    ADDING,
    ERROR,
    DONE
}

export default function AddHtmlAsset(props: Props): ReactElement {
    const [name, setName] = useState('');
    const [displayTime, setDisplayTime] = useState(10);
    const [notBefore, setNotBefore] = useState('');
    const [notAfter, setNotAfter] = useState('');
    const [content, setContent] = useState('');
    const [addState, setAddState] = useState(ADDSTATE.NONE);

    const options: (string | string[])[] = buttonList.formatting
        .concat([['table']])
        .concat([['font', 'fontSize', 'formatBlock']])
        .concat([['paragraphStyle', 'blockquote']])
        .concat([['fontColor', 'hiliteColor']]);

    const onSubmit: (e: React.FormEvent) => Promise<void> = async (e) => {
        e.preventDefault();
        setAddState(ADDSTATE.ADDING);
        const resource: IAddHtmlAssetResource = {
            name,
            content,
            displayTime,
            isActive: true,
            notAfter,
            notBefore,
            order: Math.round(Date.now() / 1000)
        };
        try {
            await props.provider.addOneAsync(resource);
            await (new Promise((resolve) => setTimeout(resolve, 3000)));
            setAddState(ADDSTATE.DONE);
        } catch (ex) {
            // TODO: Error handling
            setAddState(ADDSTATE.ERROR);
        }
    };

    if (addState === ADDSTATE.DONE)
        return <Redirect to='/assets' />;

    const isAdding: boolean = addState === ADDSTATE.ADDING;
    return (
        <div>
            { isAdding && <ModalLoading /> }
            <form onSubmit={(e) => onSubmit(e)}>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Name</label>
                    <div className='col-sm-8'>
                        <input type='text' className='form-control' value={name} onChange={(e) => setName(e.target.value)} disabled={isAdding} />
                    </div>
                </div>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Anzeigezeit (Sekunden)</label>
                    <div className='col-sm-8'>
                        <input type='number' min='5' max='300' step='1' className='form-control' value={displayTime} onChange={(e) => setDisplayTime(parseInt(e.target.value, 10))} disabled={isAdding} />
                    </div>
                </div>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Nicht anzeigen vor (optional)</label>
                    <div className='col-sm-8'>
                        <input type='date' className='form-control' value={notBefore} onChange={(e) => setNotBefore(e.target.value)} disabled={isAdding} />
                    </div>
                </div>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Nicht anzeigen nach (optional)</label>
                    <div className='col-sm-8'>
                        <input type='date' className='form-control' value={notAfter} onChange={(e) => setNotAfter(e.target.value)} disabled={isAdding}/>
                    </div>
                </div>
                <div className='form-group row'>
                    <label className='col-sm-4 col-form-label'>Inhalt</label>
                    <div className='col-sm-8'>
                        <SunEditor setOptions={{ buttonList: options }} onChange={(c) => setContent(c)} />
                    </div>
                </div>
                <div className='form-group row'>
                    <div className='col-sm-8'>
                        { !isAdding
                            ? <button type='submit' className='btn btn-primary'>Speichern</button>
                            : <button type='button' className='btn btn-primary' disabled={true}>
                                <span className='spinner-border spinner-border-sm' role='status' aria-hidden='true'/>
                            </button>
                        }
                    </div>
                </div>
            </form>
        </div>
    );
}