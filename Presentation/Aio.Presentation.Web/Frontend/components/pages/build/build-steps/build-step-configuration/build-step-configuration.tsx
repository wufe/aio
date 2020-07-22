import * as React from 'react';
import './build-step-configuration.scss';
import { FaTimes } from 'react-icons/fa';
import { TStep } from '~/types';
import { BuildStepEnvironmentVariables } from './build-step-sections/build-step-environment-variables/build-step-environment-variables';
import { useBuildAPI } from '../../build-hook';

type TProps = {
    step: TStep;
    onStepUpdate: (step: TStep) => (Promise<any> | any);
    onStepDeletion: () => (Promise<any> | any);
};

export const BuildStepConfiguration = (props: React.PropsWithChildren<TProps>) => {

    const [stepDraft, setStepDraft] = React.useState({ ...props.step });
    const [dirty, setDirty] = React.useState(false);

    const setDraftField = (field: keyof TStep) => (value: any) => {
        setStepDraft({ ...stepDraft, [field] : value });
        setDirty(true);
    };

    const addDraftEnvironmentVariable = (environmentVariable: string) => {
        setDraftField('environmentVariables')([...stepDraft.environmentVariables, environmentVariable]);
    };

    const removeDraftEnvironmentVariable = (environmentVariable: string) => {
        setDraftField('environmentVariables')(stepDraft.environmentVariables.filter(env => env !== environmentVariable));
    };

    const updateDraftEnvironmentVariable = (index: number, environmentVariable: string) => {
        setDraftField('environmentVariables')([
            ...stepDraft.environmentVariables.slice(0, index),
            environmentVariable,
            ...stepDraft.environmentVariables.slice(index +1)
        ]);
    };

    React.useEffect(() => {
        setStepDraft({ ...props.step });
        setDirty(false);
    }, [props.step]);

    const onSaveClick = () => {
        setDirty(false);
        Promise.resolve()
            .then(() => props.onStepUpdate(stepDraft))
            .catch(() => setDirty(true));
    }

    const onDeleteClick = () => {
        Promise.resolve()
            .then(() => props.onStepDeletion());
    }

    return <div className="build-step-configuration__component">
        <div className="__section">
            <div className="__header">Command</div>
            <div className="__content">
                <label>
                    <span>Executable</span>
                </label>
                <input type="text" className="--l" id="executable"
                    onChange={e => setDraftField('commandExecutable')(e.target.value)}
                    value={stepDraft.commandExecutable || ''} />

                <label>
                    <span>Arguments</span>
                </label>
                <span className="__input-container">
                    <input type="text" className="--l --suggestion-w-200" id="arguments"
                        onChange={e => setDraftField('commandArguments')(e.target.value)}
                        value={stepDraft.commandArguments || ''} />
                    <div className="__input-action-suggestion-container">
                        <span>placeholders allowed</span>
                    </div>
                </span>
            </div>
        </div>
        <div className="__section">
            <div className="__header">Environment</div>
            <div className="__content">
                <div className="__content">
                    <label>
                        <span>Variables</span>
                    </label>
                    <BuildStepEnvironmentVariables
                        environmentVariables={stepDraft.environmentVariables}
                        onNewEnvironmentVariable={addDraftEnvironmentVariable}
                        onEnvironmentVariableRemoved={removeDraftEnvironmentVariable}
                        onEnvironmentVariableUpdated={updateDraftEnvironmentVariable} />
                </div>
                <div className="__content">
                    <label>
                        <span>Working directory</span>
                    </label>
                    <input type="text" className="--l" id="cwd"
                        onChange={e => setDraftField('workingDirectory')(e.target.value)}
                        value={stepDraft.workingDirectory} />
                </div>
            </div>
        </div>
        <div className="__section">
            <div className="__header">Options</div>
            <div className="__content">
                <label>
                    <span>Fire and forget</span>
                    <input type="checkbox" name="" id="fireAndForget"
                        onChange={e => setDraftField('fireAndForget')(e.target.checked)}
                        checked={stepDraft.fireAndForget} />
                </label>
                <label>
                    <span>Halt build on error</span>
                    <input type="checkbox" name="" id="haltOnError"
                        onChange={e => setDraftField('haltOnError')(e.target.checked)}
                        checked={stepDraft.haltOnError} />
                </label>
            </div>
        </div>
        <div className="__section __actions-container">
            <button
                className={`__action --success ${dirty ? '' : '--disabled'}`}
                onClick={onSaveClick}>Save</button>
            <button className="__action --danger"
                onClick={onDeleteClick}>Delete</button>
        </div>
    </div>;
};