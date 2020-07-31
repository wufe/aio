import { TAction } from "~/state/types";
import { Reducer } from "react";

export type TAppState = {
    loading: boolean;
    identity: TIdentity;
    modal: {
        name: string;
        visible: boolean;
    };
}

export type TIdentity = {
    logged: boolean | undefined;
    token: string;
};

export const initialAppState: TAppState = {
    loading: false,
    identity: {
        logged: undefined,
        token: ''
    },
    modal: {
        name: '',
        visible: false,
    }
};

export enum AppAction {
    SET_LOADING          = '@@app/setLoading',
    SET_MODAL_VISIBILITY = '@@app/setModalVisibility',
    SET_MODAL_NAME       = '@@app/setModalName',
    SET_LOGGED           = '@@app/setLogged',
    SET_ACCESS_TOKEN     = '@@app/setAccessToken'
}

export const appReducer: Reducer<TAppState, TAction> = (state = initialAppState, action) => {
    switch (action.type) {
        case AppAction.SET_LOADING:
            return {
                ...state,
                loading: action.payload
            };
        case AppAction.SET_MODAL_VISIBILITY:
            return {
                ...state,
                modal: {
                    ...state.modal,
                    visible: action.payload || false
                }
            };
        case AppAction.SET_MODAL_NAME:
            return {
                ...state,
                modal: {
                    ...state.modal,
                    name: action.payload || ''
                }
            };
        case AppAction.SET_LOGGED:
            return {
                ...state,
                identity: {
                    ...state.identity,
                    logged: action.payload
                }
            };
        case AppAction.SET_ACCESS_TOKEN:
            return {
                ...state,
                identity: {
                    ...state.identity,
                    token: action.payload
                }
            };
        default:
            return state;
    }
};