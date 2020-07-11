import Axios from 'axios';
import { TBuild } from '~/types';
import { AppAction } from '~/state/app/app-state';
import { useDispatch } from 'react-redux';

export const useBuildCreation = () => {

    const dispatch = useDispatch();

    return {
        createNewBuild: (build: Partial<TBuild>) => new Promise((resolve, reject) => {
            dispatch({ type: AppAction.SET_LOADING, payload: true });
            Axios.post(`/api/build/`, build)
                .then(resolve)
                .catch(reject)
                .finally(() => {
                    dispatch({ type: AppAction.SET_LOADING, payload: false });
                })
        })
            
            
        
    };
}