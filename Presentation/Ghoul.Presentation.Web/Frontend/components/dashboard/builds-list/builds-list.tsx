import * as React from 'react';
import './builds-list.scss';
import { useBuildsRetrieval } from './build-list-hook';
import { useSelector } from 'react-redux';
import { TGlobalState } from '~/state/reducer';
import { TBuild } from '~/types';
import { BuildRow } from '../build-row/build-row';

export const BuildsList = () => {

    const [active, setActive] = React.useState<string>(null);
    const { getAll } = useBuildsRetrieval();

    const builds = useSelector<TGlobalState, TBuild[]>(state => state.build.builds);

    React.useEffect(() => {
        if (!builds.length)
            getAll().then(() => setActive(null))
    }, [builds]);

    return <div className="builds-list__component">
        <div className="__list">
            {builds.map(build => <BuildRow
                build={build}
                active={active === build.id}
                expand={() => setActive(build.id)}
                collapse={() => setActive(null)}
                key={build.id} />)}
        </div>
    </div>;
}