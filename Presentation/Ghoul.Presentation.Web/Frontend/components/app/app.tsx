import * as React from 'react';
import loadable from '@loadable/component';
import { Route } from 'react-router-dom';
import './app.scss';
import { Build } from '../pages/build/build';
import { useDashboardPageLoad } from '../pages/dashboard/dashboard-hook';
import { useBuildAPI, usePolling } from '../pages/build/build-hook';

export const App = () => {

    const { getAll } = useBuildAPI();
    usePolling(getAll, 2000);

    const { go } = useDashboardPageLoad();
    const DashboardPage = loadable(() => import('~/components/pages/dashboard/dashboard'));

    const onLogoClick = () => go();
    
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