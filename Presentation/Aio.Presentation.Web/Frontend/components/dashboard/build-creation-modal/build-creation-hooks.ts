import Axios from 'axios';
import { TBuild } from '~/types';
import { AppAction } from '~/state/app/app-state';
import { useDispatch } from 'react-redux';
import { useBuildAPI } from '~/components/pages/build/build-hook';

export const useBuildCreation = () => {
    const dispatch = useDispatch();
    const { save } = useBuildAPI();
    return {
        saveBuild: (build: Partial<TBuild>) => save(build)
    };
}