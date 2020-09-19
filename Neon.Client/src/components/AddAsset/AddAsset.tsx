import React, { ReactElement, ChangeEvent, FormEvent } from 'react';
import { Redirect } from 'react-router-dom';
import { boundMethod } from 'autobind-decorator';
import HttpImageAssetProvider from './../../providers/HttpImageAssetProvider';

type Props = {
    provider: HttpImageAssetProvider;
};

type State = {
    name: string,
    displayTime: number,
    context: string,
    file: File,
    notBefore: string,
    notAfter: string,
    addRunning: boolean,
    addFinished: boolean
};

const DEFAULT_STATE: State = {
    name: '',
    displayTime: 10,
    context: 'Default',
    file: undefined,
    notBefore: '',
    notAfter: '',
    addRunning: false,
    addFinished: false
};

export default class AddAsset extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = DEFAULT_STATE;
    }

    public render(): ReactElement {
        return (
            <div className='AddAssetComponent'>
                { this.state.addFinished &&
                    <Redirect to='/assets' />
                }
                <form onSubmit={this._onFormSubmit}>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Name</label>
                        <div className='col-sm-8'>
                            <input type='text' className='form-control' value={this.state.name} onChange={this._onNameChanged} disabled={this.state.addRunning} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Anzeigezeit (Sekunden)</label>
                        <div className='col-sm-8'>
                            <input type='number' min='5' max='300' step='1' className='form-control' value={this.state.displayTime} onChange={this._onDisplayTimeChanged} disabled={this.state.addRunning} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Bilddatei</label>
                        <div className='col-sm-8'>
                            <input type='file' className='form-control' onChange={this._onFileChanged} disabled={this.state.addRunning} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Nicht anzeigen vor (optional)</label>
                        <div className='col-sm-8'>
                            <input type='date' className='form-control' value={this.state.notBefore} onChange={this._onNotBeforeChanged} disabled={this.state.addRunning} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Nicht anzeigen nach (optional)</label>
                        <div className='col-sm-8'>
                            <input type='date' className='form-control' value={this.state.notAfter} onChange={this._onNotAfterChanged} disabled={this.state.addRunning}/>
                        </div>
                    </div>
                    <div className='form-group row'>
                        <div className='col-sm-8'>
                            { !this.state.addRunning
                                ? <button type='submit' className='btn btn-primary'>Speichern</button>
                                : <button type='button' className='btn btn-primary' disabled>
                                    <span className='spinner-border spinner-border-sm' role='status' aria-hidden='true'></span>
                                </button>
                            }
                        </div>
                    </div>
                </form>
            </div>
        );
    }

    @boundMethod
    private _onNameChanged(event: ChangeEvent<HTMLInputElement>): void {
        this.setState({name: event.target.value});
    }

    @boundMethod
    private _onDisplayTimeChanged(event: ChangeEvent<HTMLInputElement>): void {
        this.setState({displayTime: parseInt(event.target.value, 10)});
    }

    @boundMethod
    private _onFileChanged(event: ChangeEvent<HTMLInputElement>): void {
        this.setState({file: event.target.files[0]});
    }

    @boundMethod
    private _onNotBeforeChanged(event: ChangeEvent<HTMLInputElement>): void {
        this.setState({notBefore: event.target.value});
    }

    @boundMethod
    private _onNotAfterChanged(event: ChangeEvent<HTMLInputElement>): void {
        this.setState({notAfter: event.target.value});
    }

    @boundMethod
    private async _onFormSubmit(event: FormEvent<HTMLElement>): Promise<void> {
        event.preventDefault();
        this.setState({addRunning: true});
        try {
            await this.props.provider.addOneAsync({
                name: this.state.name,
                isActive: true,
                order: Math.round(Date.now() / 1000),
                displayTime: this.state.displayTime,
                image: this.state.file,
                notBefore: this.state.notBefore,
                notAfter: this.state.notAfter
            });
            this.setState(Object.assign(DEFAULT_STATE, {addFinished: true}));
        } catch (ex) {
            console.error('Add error', ex);
        }
    }
}