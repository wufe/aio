import { TBuild } from "~/types";
import { Reducer } from "react";
import { TAction } from "../types";
import { Build } from "~/components/pages/build/build";

export type TBuildState = {
    builds: TBuild[];
    current: TBuild;
};

export const initialBuildState: TBuildState = {
    builds: [],
    current: null
};

export enum BuildAction {
    SET_BUILDS        = '@@build/setBuilds',
    SET_CURRENT_BUILD = '@@build/setCurrentBuild',
}

export const buildReducer: Reducer<TBuildState, TAction> = (state = initialBuildState, action) => {
    switch (action.type) {
        case BuildAction.SET_BUILDS:
            return {
                ...state,
                builds: [...action.payload]
            };
        case BuildAction.SET_CURRENT_BUILD:
            return {
                ...state,
                current: action.payload
            };
        default:
            return state;
    }
}