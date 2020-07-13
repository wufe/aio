import { Context, Dispatch, createContext, Reducer } from "react";
import { TAction } from "~/state/types";
import { ActionType } from "~/state/actions";
import { TAppState, initialAppState, appReducer } from "~/state/app/app-state";
import { combineReducers } from "redux";
import { buildReducer, initialBuildState, TBuildState } from "./build/build-state";

export type TGlobalState = {
    app: TAppState;
    build: TBuildState;
}

export const initialGlobalState: TGlobalState = {
    app: initialAppState,
    build: initialBuildState
};

export const rootContext: Context<{ state: TGlobalState, dispatch: Dispatch<TAction> }>
    = createContext({ state: initialGlobalState, dispatch: _ => {}});

export const rootReducer = combineReducers({
    app: appReducer,
    build: buildReducer
});