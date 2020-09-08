import Oidc, { UserManagerSettings } from "oidc-client";

declare let process: any;

export class Identity {

    private static _instance: Identity;

    private config: UserManagerSettings = {
        authority: process.env.NODE_ENV === 'development' ?
            "https://localhost:9999" : "https://ido.bembi.dev",
        client_id: "aio",
        redirect_uri: location.origin + "/login-callback",
        response_type: "code",
        scope: "openid profile role",
        post_logout_redirect_uri: location.origin + "/logout-callback"
    };

    public manager: Oidc.UserManager;

    private constructor() {
        if (process.env.NODE_ENV === 'development')
            Oidc.Log.logger = console;
        const manager = new Oidc.UserManager(this.config);
        this.manager = manager;
    }

    public static get Instance() {
        if (!Identity._instance)
            Identity._instance = new Identity();
        return Identity._instance;
    }
}