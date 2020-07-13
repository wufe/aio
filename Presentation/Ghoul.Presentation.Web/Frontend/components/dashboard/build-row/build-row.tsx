import * as React from 'react';
import './build-row.scss';
import { TBuild } from '~/types';
import { BuildSummary } from '~/components/build-summary/build-summary';

type TBuildRowProps = {
    build: TBuild;
    active: boolean;
    expand: () => void;
    collapse: () => void;
}

export const BuildRow = (props: React.PropsWithChildren<TBuildRowProps>) => {
    return <div
        className={`build-row__component ${props.active ? '--active' : ''}`}>
        <BuildSummary build={props.build} onHeaderClick={() => {
            console.log('on header click')
            props.active ? props.collapse() : props.expand();
        }} />
    </div>;
}