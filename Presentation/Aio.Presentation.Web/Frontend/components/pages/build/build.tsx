import * as React from 'react';
import './build.scss';
import { useRouteMatch } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { TGlobalState } from '~/state/reducer';
import { TBuild, TStep } from '~/types';
import { BuildAction } from '~/state/build/build-state';
import { useBuildAPI } from './build-hook';
import { FaTimes, FaCheck } from 'react-icons/fa';
import { BuildInfo } from './build-info/build-info';
import { BuildSteps } from './build-steps/build-steps';

export const Build = () => {

    const { params } = useRouteMatch<{ id: string }>();
    const buildID = params.id;

    const build = useSelector<TGlobalState, TBuild>(x => x.build.current);
    const [buildDraft, setBuildDraft] = React.useState({ ...build });
    const [stepsDraft, setStepsDraft] = React.useState<TStep[]>([]);
    
    const { get, update, addStep, updateStep, deleteStep, updateStepsOrder } = useBuildAPI();

    const onNewStep = async (name: string) => {
        await addStep(build.id, name);
        return await get(buildID);
    }

    const onBuildUpdate = async (build: TBuild) => {
        await update(build.id, build);
        return await get(buildID);
    }

    const onStepUpdate = async (step: TStep, stepIndex: number) => {
        await updateStep(build.id, stepIndex, step);
        return await get(buildID);
    }

    const onStepDeletion = async (stepIndex: number) => {
        await deleteStep(build.id, stepIndex);
        return await get(buildID);
    }

    const reorder = <T extends unknown>(list: T[], startIndex: number, endIndex: number) => {
        const result = Array.from(list);
        const [removed] = result.splice(startIndex, 1);
        result.splice(endIndex, 0, removed);
        return result;
    }

    const onStepsReordered = async (startIndex: number, endIndex: number) => {
        setStepsDraft(reorder(stepsDraft, startIndex, endIndex));
        await updateStepsOrder(build.id, startIndex, endIndex);
        return await get(buildID);
    }

    React.useEffect(() => {

        if (build === null || build.id !== buildID) {
            get(buildID);
        } else {
            // custom deep clone
            setBuildDraft({
                ...build,
                steps: build.steps.map(step => ({ ...step }))
            });
            setStepsDraft([ ...build.steps ]);
        }

    }, [build]);

    return <div className="build__page">
        <div className="__header">
            <h2><span className="__build-name">{buildDraft.name || buildID}</span></h2>
        </div>
        <div className="__content">
            <BuildInfo
                build={buildDraft}
                onBuildUpdate={onBuildUpdate} />
            <BuildSteps
                steps={stepsDraft}
                onNewStep={onNewStep}
                onStepDeletion={onStepDeletion}
                onStepUpdate={onStepUpdate}
                onStepsReordered={onStepsReordered} />
        </div>
    </div>
}