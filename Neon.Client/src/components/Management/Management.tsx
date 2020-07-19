import React, { ReactElement, ChangeEvent, FormEvent } from 'react';
import { boundMethod } from 'autobind-decorator';

type Props = { };
type State = {
    name: string,
    displayTime: number,
    context: string,
    file: File,
    notBefore: string,
    notAfter: string
};

export default class Management extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = {
            name: '',
            displayTime: 10,
            context: 'Default',
            file: null,
            notBefore: null,
            notAfter: null
        };
    }

    public render(): ReactElement {
        return (
            <div className='ManagementComponent'>
                <div className='container'>
                <form onSubmit={this._onFormSubmit}>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Name</label>
                        <div className='col-sm-8'>
                            <input type='text' className='form-control' value={this.state.name} onChange={this._onNameChanged} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Kontext</label>
                        <div className='col-sm-8'>
                            <input type='text' className='form-control' value={this.state.context} onChange={this._onContextChanged} readOnly={true} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Anzeigezeit (Sekunden)</label>
                        <div className='col-sm-8'>
                            <input type='number' min='5' max='300' step='1' className='form-control' value={this.state.displayTime} onChange={this._onDisplayTimeChanged} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Bilddatei</label>
                        <div className='col-sm-8'>
                            <input type='file' className='form-control' onChange={this._onFileChanged} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Nicht anzeigen vor (optional)</label>
                        <div className='col-sm-8'>
                            <input type='date' className='form-control' value={this.state.notBefore} onChange={this._onNotBeforeChanged} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <label className='col-sm-4 col-form-label'>Nicht anzeigen nach (optional)</label>
                        <div className='col-sm-8'>
                            <input type='date' className='form-control' value={this.state.notAfter} onChange={this._onNotAfterChanged} />
                        </div>
                    </div>
                    <div className='form-group row'>
                        <div className='col-sm-8'>
                            <button type='submit' className='btn btn-primary'>Speichern</button>
                        </div>
                    </div>
                </form>
                </div>
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
    private _onContextChanged(event: ChangeEvent<HTMLInputElement>): void {
        this.setState({context: event.target.value});
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
    private _onFormSubmit(event: FormEvent<HTMLElement>): void {
        event.preventDefault();

        // TODO: Move to service
        const formData: FormData = new FormData();
        formData.append('Name', this.state.name);
        formData.append('ContextName', this.state.context);
        formData.append('DisplayTime', this.state.displayTime.toString());
        formData.append('Image', this.state.file);
        if (this.state.notBefore)
            formData.append('NotBefore', this.state.notBefore);

        if (this.state.notAfter)
            formData.append('NotAfter', this.state.notAfter);

        fetch('https://localhost:5001/imageassets', {
            method: 'POST',
            body: formData
        });
    }
}