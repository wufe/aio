import * as React from 'react';
import Oidc from 'oidc-client';

export const LoginCallback = () => {

    React.useEffect(() => {
        new Oidc.UserManager({ response_mode: "query" })
            .signinPopupCallback()
            .then(function (user) {
                // alert();
            }).catch(function (e) {
                console.error(e);
            });
    }, []);

    return <></>;
}

export default LoginCallback;