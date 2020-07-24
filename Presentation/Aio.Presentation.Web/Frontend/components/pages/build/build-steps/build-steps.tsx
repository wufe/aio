import * as React from 'react';
import './build-steps.scss';
import { TStep } from '~/types';
import { BuildStepsList } from './build-steps-list/build-steps-list';
import { BuildStepConfiguration } from './build-step-configuration/build-step-configuration';

type TProps = {
    steps: TStep[];
    onNewStep: (name: string) => Promise<any>;
    onStepUpdate: (step: TStep, stepIndex: number) => (Promise<any> | any);
    onStepDeletion: (stepIndex: number) => (Promise<any> | any);
    onStepsReordered: (startIndex: number, endIndex: number) => (Promise<any> | any);
}

export const BuildSteps = (props: React.PropsWithChildren<TProps>) => {

    const [activeStepIndex, setActiveStepIndex] = React.useState(-1);

    React.useEffect(() => {
        if (activeStepIndex > -1) {
            if (activeStepIndex >= props.steps.length)
                setActiveStepIndex(0);
        }
    });

    const onStepClick = (name: string) => {
        setActiveStepIndex(props.steps.findIndex(s => s.name === name));
    }

    const onStepUpdate = (step: TStep) => {
        return props.onStepUpdate(step, activeStepIndex);
    }

    const onStepDeletion = () => {
        return props.onStepDeletion(activeStepIndex);
    }

    const onStepsReordered = (startIndex: number, endIndex: number) => {
        if (activeStepIndex === startIndex) {
            setActiveStepIndex(endIndex);
        } else if (activeStepIndex === endIndex) {
            setActiveStepIndex(startIndex);
        }
        return props.onStepsReordered(startIndex, endIndex);
    };

    return <div className="build-steps__component">
        <BuildStepsList
            activeStepIndex={activeStepIndex}
            steps={props.steps}
            onNewStep={props.onNewStep}
            onStepClick={onStepClick}
            onStepsReordered={onStepsReordered} />
        {activeStepIndex > -1 && props.steps[activeStepIndex] &&
            <BuildStepConfiguration
                step={props.steps[activeStepIndex]}
                onStepDeletion={onStepDeletion}
                onStepUpdate={onStepUpdate} />}
    </div>;
}