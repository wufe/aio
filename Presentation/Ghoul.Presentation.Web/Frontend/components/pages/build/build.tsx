import * as React from 'react';
import './build.scss';
import { useRouteMatch } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { TGlobalState } from '~/state/reducer';
import { TBuild } from '~/types';
import { BuildAction } from '~/state/build/build-state';
import { useBuildRetrieval } from './build-hook';

export const Build = () => {

    const { params } = useRouteMatch<{ id: string }>();
    const buildID = params.id;

    const build = useSelector<TGlobalState, TBuild>(x => x.build.current);
    const [buildName, setBuildName] = React.useState(buildID);
    const dispatch = useDispatch();
    const { get } = useBuildRetrieval();

    React.useEffect(() => {

        (window as any).ciccio = get;
        console.log(build);

        if (build === null || build.id !== buildID) {
            get(buildID);
        } else {
            setBuildName(build.name);
        }

    }, [build])

    return <div className="build__page">
        <div className="__header">
            <h2><span className="__build-name">{buildName}</span></h2>
        </div>
        <div className="__content">
            <div className="__build-info">

            </div>
            <div className="__build-steps">
                <div className="__steps-container">

                </div>
                <div className="__step-instructions">

                </div>
            </div>
        </div>
    </div>
}