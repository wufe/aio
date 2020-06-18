import { Context, Dispatch, createContext, Reducer } from "react";
import { TAction } from "~/state/types";
import { ActionType } from "~/state/actions";
import { TAppState, initialAppState, appReducer } from "~/state/app/app-state";
import { combineReducers } from "redux";

export type TGlobalState = {
    app: TAppState;
}

export const initialGlobalState: TGlobalState = {
    app: initialAppState
};

export const rootContext: Context<{ state: TGlobalState, dispatch: Dispatch<TAction> }>
    = createContext({ state: initialGlobalState, dispatch: _ => {}});

export const rootReducer = combineReducers({
    app: appReducer
});