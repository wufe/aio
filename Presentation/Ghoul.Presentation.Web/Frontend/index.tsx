import 'core-js/stable';
import 'regenerator-runtime';
import * as React from 'react';
import { render } from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { AppContainer } from '~/components/app/app-container';
import { store } from '~/state/store';
import { Provider } from 'react-redux';
import { LoadingBarContainer } from './components/loading/loading-bar-container';

render(
    <React.StrictMode>
        <Provider store={store}>
            <BrowserRouter>
                <AppContainer />
            </BrowserRouter>
        </Provider>
    </React.StrictMode>, document.getElementById('app'));

render(
    <React.StrictMode>
        <Provider store={store}>
            <LoadingBarContainer />
        </Provider>
    </React.StrictMode>,
    document.getElementById('loading'));

declare let module: any;

if (module.hot) {
    module.hot.accept()
}