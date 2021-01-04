import { compare } from 'fast-json-patch';
import moment from 'moment';
import React, { ReactElement, useEffect, useState } from 'react';
import { Redirect, useParams } from 'react-router-dom';
import SunEditor, { buttonList } from 'suneditor-react';
import 'suneditor/dist/css/suneditor.min.css'; // Import Sun Editor's CSS File
import HtmlAsset from '../../models/HtmlAsset';
import IUpdateHtmlAssetResource from '../../models/IUpdateHtmlAssetResource';
import IHtmlAssetProvider from '../../providers/IHtmlAssetProvider';
import Loading from '../Loading/Loading';
import ModalLoading from '../Loading/ModalLoading';

type Props = {
    provider: IHtmlAssetProvider;
};

type RouteParams = {
    id: string;
};

export default function EditHtmlAsset(props: Props): ReactElement {

    const { id }: RouteParams = useParams<RouteParams>();

    const [initialized, setInitialized] = useState(false);
    const [loading, setLoading] = useState(true);
    const [success, setSuccess] = useState(false);

    const [name, setName] = useState('');
    const [displayTime, setDisplayTime] = useState(0);
    const [notBefore, setNotBefore] = useState('');
    const [notAfter, setNotAfter] = useState('');
    const [content, setContent] = useState('');
    const [originalAsset, setOriginalAsset] = useState<HtmlAsset>(null);

    const options: (string | string[])[] = buttonList.formatting
        .concat([['table']])
        .concat([['font', 'fontSize', 'formatBlock']])
        .concat([['paragraphStyle', 'blockquote']])
        .concat([['fontColor', 'hiliteColor']]);

    // The "method" formerly known as componentDidMount
    useEffect(() => {
        props.provider.oneAsync(id).then((asset: HtmlAsset) => {
            setOriginalAsset(asset);
            setName(asset.name);
            setDisplayTime(asset.displayTime);
            setNotBefore(asset.notBefore ? moment.utc(asset.notBefore).format('YYYY-MM-DD') : '');
            setNotAfter(asset.notAfter ? moment.utc(asset.notAfter).format('YYYY-MM-DD') : '');
            setContent(asset.content);
            setInitialized(true);
            setLoading(false);
        });
    }, []);

    const onSubmit: (e: React.FormEvent) => Promise<void> = async (e) => {
        e.preventDefault();
        try {
            setLoading(true);
            const updatedAsset: IUpdateHtmlAssetResource = {
                id,
                name,
                content,
                displayTime,
                notAfter: notAfter ? notAfter : null,
                notBefore: notBefore ? notBefore : null,
                isActive: originalAsset.isActive,
                order: originalAsset.order
            };
            await props.provider.updateOneAsync(id, compare(originalAsset, updatedAsset));
            setSuccess(true);
        } catch (ex) {
            console.log('Error on update', ex);
            // TODO: Toast / Error-Modal
        } finally {
            setLoading(false);
        }
    };

    if (!initialized && loading)
        return <Loading />;

    // if (success)
    //    return <Redirect to='/assets' />

    return (
        <>
            { loading && <ModalLoading /> }
            <div>
                <form onSubmit={(e) => onSubmit(e)}>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Name</label>
                        <div className='col-sm-8'>
                            <input type='text' className='form-control' value={name} onChange={(e) => setName(e.target.value)} disabled={loading} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Anzeigezeit (Sekunden)</label>
                        <div className='col-sm-8'>
                            <input type='number' min='5' max='300' step='1' className='form-control' value={displayTime} onChange={(e) => setDisplayTime(parseInt(e.target.value, 10))} disabled={loading} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Nicht anzeigen vor (optional)</label>
                        <div className='col-sm-8'>
                            <input type='date' className='form-control' value={notBefore} onChange={(e) => setNotBefore(e.target.value)} disabled={loading} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Nicht anzeigen nach (optional)</label>
                        <div className='col-sm-8'>
                            <input type='date' className='form-control' value={notAfter} onChange={(e) => setNotAfter(e.target.value)} disabled={loading} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Inhalt</label>
                        <div className='col-sm-8'>
                            <SunEditor setOptions={{ buttonList: options }} setContents={originalAsset.content} onChange={(c) => setContent(c)} enable={!loading} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <div className='col-sm-8'>
                            { !loading
                                ? <button type='submit' className='btn btn-primary'>Speichern</button>
                                : <button type='button' className='btn btn-primary' disabled={true}>
                                    <span className='spinner-border spinner-border-sm' role='status' aria-hidden='true'/>
                                </button>
                            }
                        </div>
                    </div>
                </form>
            </div>
        </>
    );
}