import { useDispatch } from "react-redux"
import { AppAction } from "~/state/app/app-state";
import Axios from "axios";
import { BuildAction } from "~/state/build/build-state";
import { getUndefinedBuild, TStep, TBuild } from "~/types";
import { useHistory } from "react-router-dom";
import { useState, useEffect, useRef } from "react";

export const useBuildAPI = () => {
    const dispatch = useDispatch();
    
    const getAll = () =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.get(`/api/build/`))
            .then(response => dispatch({ type: BuildAction.SET_BUILDS, payload: response.data }))
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    const get = (buildID: string) =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.get(`/api/build/${buildID}`))
            .then(response => dispatch({ type: BuildAction.SET_CURRENT_BUILD, payload: response.data }))
            .catch(() => dispatch({ type: BuildAction.SET_CURRENT_BUILD, payload: getUndefinedBuild(buildID) }))
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    const update = (buildID: string, build: TBuild) =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.patch(`/api/build/${buildID}`, build))
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    const remove = (buildID: string) =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.delete(`/api/build/${buildID}`))
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    const addStep = (buildID: string, name: string) =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.post(`/api/build/${buildID}/step`, { name }))
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    const deleteStep = (buildID: string, stepIndex: number) =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.delete(`/api/build/${buildID}/step/${stepIndex}`))
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    const updateStep = (buildID: string, stepIndex: number, step: TStep) =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.patch(`/api/build/${buildID}/step/${stepIndex}`, step))
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    const getLatestRun = (buildID: string) =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.get(`/api/build/${buildID}/run/latest`))
            .then(response => response.data)
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    const enqueueNewRun = (buildID: string) =>
        Promise.resolve()
            .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
            .then(() => Axios.post(`/api/build/${buildID}/run`))
            .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    return {
        getAll,
        get,
        update,
        remove,
        addStep,
        deleteStep,
        updateStep,
        getLatestRun,
        enqueueNewRun
    };
}

export const useBuildPageLoad = () => {
    const { get } = useBuildAPI();
    const { push } = useHistory();

    return {
        go: (buildID: string) =>
            get(buildID)
                .then(() => push(`/build/${buildID}`))
    }
}

export const usePolling = (callback: () => any, timeout: number, condition = true) => {
    const timeoutHandle = useRef<number>(null);

    const poll = () => {
        if (condition) {
            callback();
            timeoutHandle.current = setTimeout(poll, timeout);
        }
    }

    useEffect(() => {
        if (condition && !timeoutHandle.current) {
            timeoutHandle.current = setTimeout(poll, timeout);
        } else if (!condition && timeoutHandle.current) {
            clearTimeout(timeoutHandle.current);
            timeoutHandle.current = null;
        }
        return () => {
            if (timeoutHandle.current) {
                clearTimeout(timeoutHandle.current);
                timeoutHandle.current = null;
            }
        }
    })
}

export const delay = (ms: number) =>
    new Promise(resolve => setTimeout(resolve, ms));