import { useDispatch } from "react-redux"
import { AppAction } from "~/state/app/app-state";
import Axios from "axios";
import { BuildAction } from "~/state/build/build-state";
import { getUndefinedBuild, TStep } from "~/types";
import { useHistory } from "react-router-dom";

export const useBuildAPI = () => {
    const dispatch = useDispatch();
    const getAll = () => new Promise((resolve, reject) => {
        dispatch({ type: AppAction.SET_LOADING, payload: true });
        Axios.get(`/api/build/`)
            .then(response => {
                dispatch({ type: BuildAction.SET_BUILDS, payload: response.data });
                resolve();
            })
            .catch(reject)
            .finally(() => {
                dispatch({ type: AppAction.SET_LOADING, payload: false });
            });
    });
    const get = (buildID: string) => new Promise((resolve, reject) => {
        dispatch({ type: AppAction.SET_LOADING, payload: true });
        Axios.get(`/api/build/${buildID}`)
            .then(response => {
                return new Promise<typeof response>(res => {
                    setTimeout(() => res(response), 1000);
                });
            })
            .then(response => {
                dispatch({ type: BuildAction.SET_CURRENT_BUILD, payload: response.data });
                resolve();
            })
            .catch(e => {
                dispatch({ type: BuildAction.SET_CURRENT_BUILD, payload: getUndefinedBuild(buildID) });
                reject(e);
            })
            .finally(() => {
                dispatch({ type: AppAction.SET_LOADING, payload: false });
            });
    });
    const remove = (buildID: string) => new Promise((resolve, reject) => {
        dispatch({ type: AppAction.SET_LOADING, payload: true });
        Axios.delete(`/api/build/${buildID}`)
            .then(resolve)
            .catch(reject)
            .finally(() => {
                dispatch({ type: AppAction.SET_LOADING, payload: false });
            });
    });
    const addStep = (buildID: string, name: string) => new Promise((resolve, reject) => {
        dispatch({ type: AppAction.SET_LOADING, payload: true });
        Axios.post(`/api/build/${buildID}/step`, { name })
            .then(resolve)
            .catch(reject)
            .finally(() => {
                dispatch({ type: AppAction.SET_LOADING, payload: false });
            });
    });
    const updateStep = (buildID: string, stepIndex: number, step: TStep) => Promise.resolve()
        .then(() => dispatch({ type: AppAction.SET_LOADING, payload: true }))
        .then(() => Axios.patch(`/api/build/${buildID}/step/${stepIndex}`, step))
        .finally(() => dispatch({ type: AppAction.SET_LOADING, payload: false }));

    return {
        get,
        getAll,
        remove,
        addStep,
        updateStep
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