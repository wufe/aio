import { useDispatch } from "react-redux"
import { AppAction } from "~/state/app/app-state";
import Axios from "axios";
import { BuildAction } from "~/state/build/build-state";
import { getUndefinedBuild } from "~/types";
import { useHistory } from "react-router-dom";

export const useBuildRetrieval = () => {
    const dispatch = useDispatch();
    return {
        get: (buildID: string) => new Promise((resolve, reject) => {
                dispatch({type: AppAction.SET_LOADING, payload: true });
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
            })
    }
}

export const useBuildPageLoad = () => {
    const { get } = useBuildRetrieval();
    const { push } = useHistory();

    return {
        go: (buildID: string) =>
            get(buildID)
                .then(() => push(`/build/${buildID}`))
    }
}