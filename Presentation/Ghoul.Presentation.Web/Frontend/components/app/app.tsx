import * as React from 'react';
import loadable from '@loadable/component';
import { Route, Link } from 'react-router-dom';
import { Modal } from '~/components/modal/modal';
import { useModal } from '~/components/modal/modal-hooks';
import './app.scss';
import { Build } from '../pages/build/build';
import { useDashboardPageLoad } from '../pages/dashboard/dashboard-hook';

export const App = () => {

    const DashboardPage = loadable(() => import('~/components/pages/dashboard/dashboard'));
    const { go } = useDashboardPageLoad();

    const onLogoClick = () => go();

    return <div className="app__component">
        <h1 onClick={onLogoClick} className="__logo">Ghoul</h1>
        <Route exact path="/">
            <DashboardPage />
        </Route>
        <Route path="/build/:id">
            <Build />
        </Route>
    </div>;
};