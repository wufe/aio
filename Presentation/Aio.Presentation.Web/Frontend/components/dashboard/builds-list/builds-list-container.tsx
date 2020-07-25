import * as React from 'react';
import { useBuildAPI } from '~/components/pages/build/build-hook';
import { BuildsList } from './builds-list';
import { useSelector } from 'react-redux';
import { TGlobalState } from '~/state/reducer';
import { TBuild } from '~/types';

export const BuildsListContainer = () => {
    const builds = useSelector<TGlobalState, TBuild[]>(state => state.build.builds);
    const [buildsDraft, setBuildsDraft] = React.useState<TBuild[]>([]);
    const { getAll, updateBuildsOrder } = useBuildAPI();

    const reorder = <T extends unknown>(list: T[], startIndex: number, endIndex: number) => {
        const result = Array.from(list);
        const [removed] = result.splice(startIndex, 1);
        result.splice(endIndex, 0, removed);
        return result;
    }

    const onBuildsReordered = async (startIndex: number, endIndex: number) => {
        setBuildsDraft(reorder(buildsDraft, startIndex, endIndex));
        await updateBuildsOrder(startIndex, endIndex);
        return await getAll();
    }

    React.useEffect(() => {
        setBuildsDraft([...builds]);
    }, [builds])

    return <BuildsList
        onBuildsReordered={onBuildsReordered}
        builds={buildsDraft} />
}