import * as React from 'react';
import './dashboard.scss';
import { DashboardActions } from '~/components/dashboard/dashboard-actions/dashboard-actions';

export const Dashboard = () => <div className="dashboard__page">
    <header className="__header">
        <h2 className="__subtitle">Dashboard</h2>
        <DashboardActions />
    </header>
</div>;

export default Dashboard;