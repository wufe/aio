import * as React from 'react';
import './builds-list.scss';
import { useSelector } from 'react-redux';
import { TGlobalState } from '~/state/reducer';
import { TBuild } from '~/types';
import { BuildRow } from '../build-row/build-row';
import { useBuildAPI } from '~/components/pages/build/build-hook';
import { diff } from 'deep-diff';

export const BuildsList = React.memo((props: React.PropsWithChildren<{ builds: TBuild[] }>) => {

    const [active, setActive] = React.useState<string>(null);
    
    React.useEffect(() => {
        const runningBuild = props.builds
            .find(x => x.status === 'Running');
        if (runningBuild && !active) {
            setActive(runningBuild.id);
        }
    });

    return <div className="builds-list__component">
        <div className="__list">
            {props.builds.map(build => <BuildRow
                build={build}
                active={active === build.id}
                expand={() => setActive(build.id)}
                collapse={() => setActive(null)}
                key={build.id} />)}
        </div>
    </div>;
}, (prev, next) => !diff(prev.builds, next.builds));