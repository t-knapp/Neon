import React, { ReactElement } from 'react';
import { boundMethod } from 'autobind-decorator'
import Jumbotron from 'react-bootstrap/Jumbotron';
import Toast from 'react-bootstrap/Toast';
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

type Props = { };
type State = {
    name: string,
    displayTime: number,
    context: string,
    notBefore: string,
    notAfter: string
};

export default class Management extends React.Component<Props, State> {

    constructor(props: Props) {
        super(props);
        this.state = {
            name: 'Name',
            displayTime: 10,
            context: 'Default',
            notBefore: null,
            notAfter: null
        }
    }

    public render(): ReactElement {
        return (
            <div className='ManagementComponent'>
                <Container className='p-3'>
                    <Jumbotron>
                        <h1 className='header'>Verwaltung</h1>
                        <Form onSubmit={this._onFormSubmit}>
                            <Form.Group as={Row} controlId='formHorizontalName'>
                                <Form.Label column sm={4}>Name</Form.Label>
                                <Col sm={8}>
                                    <Form.Control type='text' placeholder='Name' onChange={this._onNameChanged} />
                                </Col>
                            </Form.Group>

                            <Form.Group as={Row} controlId='formHorizontalDisplayTime'>
                                <Form.Label column sm={4}>Anzeigedauer</Form.Label>
                                <Col sm={8}>
                                    <Form.Control type='number' placeholder='Anzeigedauer in Sekunden' />
                                </Col>
                            </Form.Group>
                            
                            <Form.Group as={Row} controlId='formHorizontalContext'>
                                <Form.Label column sm={4}>Kontext</Form.Label>
                                <Col sm={8}>
                                    <Form.Control type='text' placeholder='Default' readOnly />
                                </Col>
                            </Form.Group>

                            <Form.Group as={Row} controlId='formHorizontalFile'>
                                <Form.Label column sm={4}>Bilddatei</Form.Label>
                                <Col sm={8}>
                                    <Form.File id='exampleFormControlFile' />
                                </Col>
                            </Form.Group>

                            <Form.Group as={Row} controlId='formHorizontalNotBefore'>
                                <Form.Label column sm={4}>Nicht zeigen vor Datum (optional)</Form.Label>
                                <Col sm={8}>
                                    <Form.Control type='text' placeholder='' />
                                </Col>
                            </Form.Group>

                            <Form.Group as={Row} controlId='formHorizontalNotAfter'>
                                <Form.Label column sm={4}>Nicht zeigen nach Datum (optional)</Form.Label>
                                <Col sm={8}>
                                    <Form.Control type='text' placeholder='' />
                                </Col>
                            </Form.Group>

                            <Form.Group as={Row}>
                                <Col sm={{ span: 10, offset: 2 }}>
                                    <Button type='submit'>Hochladen</Button>
                                </Col>
                            </Form.Group>
                        </Form>
                    </Jumbotron>
                </Container>
            </div>
        );
    }

    @boundMethod
    private _onNameChanged(): void {

    }

    @boundMethod
    private _onDisplayTimeChanged(): void {

    }

    @boundMethod
    private _onContextChanged(): void {

    }

    @boundMethod
    private _onFileChanged(): void {

    }

    @boundMethod
    private _onNotBeforeChanged(): void {

    }

    @boundMethod
    private _onNotAfterChanged(): void {

    }

    @boundMethod
    private _onFormSubmit(event: React.FormEvent<HTMLElement>): void {
        event.preventDefault();
    }
}