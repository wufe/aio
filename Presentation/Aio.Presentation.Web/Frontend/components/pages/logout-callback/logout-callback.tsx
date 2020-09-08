import * as React from 'react';
import Oidc from 'oidc-client';

export const LogoutCallbackPage = () => {
    React.useEffect(() => {
        new Oidc.UserManager({ response_mode: "query" })
            .signoutPopupCallback();
    });

    return <></>;
}