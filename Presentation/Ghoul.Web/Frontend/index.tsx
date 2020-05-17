import * as React from 'react';
import { render } from 'react-dom';
import { AppContainer } from '~/components/app/app-container';
import { StoreProvider } from '~/state/store';

render(
    <React.StrictMode>
        <StoreProvider>
            <AppContainer />
        </StoreProvider>
    </React.StrictMode>, document.getElementById('app'));