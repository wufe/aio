import Axios from "axios";
import { TBuild } from "~/types";
import { AppAction } from "~/state/app/app-state";
import { useDispatch } from "react-redux";
import { BuildAction } from "~/state/build/build-state";

export const useBuildsRetrieval = () => {
	const dispatch = useDispatch();
	return {
		getAll: () =>
			new Promise((resolve, reject) => {
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
			}),
	};
};
