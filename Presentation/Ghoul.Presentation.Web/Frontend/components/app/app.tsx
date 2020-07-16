import * as React from 'react';
import loadable from '@loadable/component';
import { Route, Link, useHistory } from 'react-router-dom';
import { Modal } from '~/components/modal/modal';
import { useModal } from '~/components/modal/modal-hooks';
import './app.scss';
import { Build } from '../pages/build/build';
import { useDashboardPageLoad } from '../pages/dashboard/dashboard-hook';
import { useBuildAPI } from '../pages/build/build-hook';

export const App = () => {

    const { push } = useHistory();
    const DashboardPage = loadable(() => import('~/components/pages/dashboard/dashboard'));

    const onLogoClick = () => push('/');

    const { getAll } = useBuildAPI();
    React.useEffect(() => {
        getAll();
    }, []);

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