import * as React from 'react';
import { Route, Redirect } from "react-router-dom";

type TProps = {
    component: React.ComponentType<any>;
    authorized: boolean;
    [k: string]: any;
};

export const GuardedRoute = ({ component: Component, authorized, ...rest }: React.PropsWithChildren<TProps>) => (
    <Route {...rest} render={(props) => (
        authorized ? <Component {...props} />
            : <Redirect to='/' />
    )} />
)