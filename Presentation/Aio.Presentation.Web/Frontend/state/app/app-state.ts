import { TAction } from "~/state/types";
import { Reducer } from "react";

export type TAppState = {
    loading: boolean;
    modal: {
        name: string;
        visible: boolean;
    };
}

export const initialAppState = {
    loading: false,
    modal: {
        name: '',
        visible: false,
    }
};

export enum AppAction {
    SET_LOADING          = '@@app/setLoading',
    SET_MODAL_VISIBILITY = '@@app/setModalVisibility',
    SET_MODAL_NAME       = '@@app/setModalName',
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
        default:
            return state;
    }
};