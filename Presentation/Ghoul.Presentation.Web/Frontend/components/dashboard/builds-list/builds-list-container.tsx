import * as React from 'react';
import { useBuildAPI } from '~/components/pages/build/build-hook';
import { BuildsList } from './builds-list';
import { useSelector } from 'react-redux';
import { TGlobalState } from '~/state/reducer';
import { TBuild } from '~/types';

export const BuildsListContainer = () => {
    const builds = useSelector<TGlobalState, TBuild[]>(state => state.build.builds);

    return <BuildsList builds={builds} />
}