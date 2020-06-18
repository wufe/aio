import * as React from 'react';
import loadable from '@loadable/component';
import { Route } from 'react-router-dom';
import { Modal } from '~/components/modal/modal';
import { useModal } from '~/components/modal/modal-hooks';
import './app.scss';

export const App = () => {

    const DashboardPage = loadable(() => import('~/components/pages/dashboard/dashboard'));

    return <div className="app__component">
        <h1>Ghoul</h1>
        <Route exact path="/">
            <DashboardPage />
        </Route>
    </div>;
};