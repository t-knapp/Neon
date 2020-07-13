import TestRenderer, {ReactTestRenderer} from 'react-test-renderer';
import React from 'react';
import App from '../App';

describe('AppComponent', () => {
    describe('render', () => {
        test('should render default', () => {
            const component: ReactTestRenderer = TestRenderer.create(<App title={'Hallo Welt.'}/>);
            expect(component.toJSON()).toMatchSnapshot();
        });
    });

    describe('interactions', () => {
        test('should call xyz when click on abc', async (done) => {
            done();
        });
    });
});
