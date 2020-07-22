import * as React from 'react';
import './dashboard.scss';
import { DashboardActions } from '~/components/dashboard/dashboard-actions/dashboard-actions';
import { BuildsList } from '~/components/dashboard/builds-list/builds-list';
import { useBuildAPI } from '../build/build-hook';
import { BuildsListContainer } from '~/components/dashboard/builds-list/builds-list-container';

export const Dashboard = React.memo(() => {

    // const { getAll } = useBuildAPI();

    React.useEffect(() => {
    }, []);

    return <div className="dashboard__page">
        <header className="__header">
            <h2 className="__subtitle">Dashboard</h2>
            <DashboardActions />
        </header>
        <BuildsListContainer />
    </div>
})

export default Dashboard;