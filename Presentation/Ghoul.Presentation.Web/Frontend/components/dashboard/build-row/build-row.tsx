import * as React from 'react';
import './build-row.scss';
import { TBuild, TRun } from '~/types';
import { BuildSummary } from '~/components/build-summary/build-summary';
import { useBuildAPI } from '~/components/pages/build/build-hook';

type TBuildRowProps = {
    build: TBuild;
    active: boolean;
    expand: () => void;
    collapse: () => void;
}

export const BuildRow = (props: React.PropsWithChildren<TBuildRowProps>) => {
    return <div
        className={`build-row__component ${props.active ? '--active' : ''}`}>
        <BuildSummary
            active={props.active}
            build={props.build}
            onHeaderClick={() => {
                props.active ? props.collapse() : props.expand();
            }} />
    </div>;
}