import * as React from 'react';
import './build-steps.scss';
import { TStep } from '~/types';
import { BuildStepsList } from './build-steps-list/build-steps-list';
import { BuildStepConfiguration } from './build-step-configuration/build-step-configuration';

type TProps = {
    steps: TStep[];
    onNewStep: (name: string) => Promise<any>;
    onStepUpdate: (step: TStep, stepIndex: number) => (Promise<any> | any);
}

export const BuildSteps = (props: React.PropsWithChildren<TProps>) => {

    const [activeStepIndex, setActiveStepIndex] = React.useState(-1);

    const onStepClick = (name: string) => {
        setActiveStepIndex(props.steps.findIndex(s => s.name === name));
    }

    const onStepUpdate = (step: TStep) => {
        return props.onStepUpdate(step, activeStepIndex);
    }

    return <div className="build-steps__component">
        <BuildStepsList
            steps={props.steps}
            onNewStep={props.onNewStep}
            onStepClick={onStepClick} />
        {activeStepIndex > -1 &&
            <BuildStepConfiguration
                step={props.steps[activeStepIndex]}
                onStepUpdate={onStepUpdate} />}
    </div>;
}