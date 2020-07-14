import * as React from 'react';
import './build.scss';
import { useRouteMatch } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { TGlobalState } from '~/state/reducer';
import { TBuild } from '~/types';
import { BuildAction } from '~/state/build/build-state';
import { useBuildRetrieval } from './build-hook';
import { FaTimes, FaCheck } from 'react-icons/fa';

export const Build = () => {

    const { params } = useRouteMatch<{ id: string }>();
    const buildID = params.id;

    const build = useSelector<TGlobalState, TBuild>(x => x.build.current);
    const [buildDraft, setBuildDraft] = React.useState({ ...build });
    const [activeStep, setActiveStep] = React.useState('Prepare folder structure');
    const { get } = useBuildRetrieval();

    const setBuildField = (field: keyof TBuild) => (value: any) => {
        setBuildDraft({ ...buildDraft, [field]: value });
    }

    // const setBuildSteps

    React.useEffect(() => {

        if (build === null || build.id !== buildID) {
            get(buildID);
        } else {
            // custom deep clone
            setBuildDraft({
                ...build,
                // steps: build.steps.map(step => ({ ...step }))
            });
        }

    }, [build]);

    const events = [
        'CommitComment',
        'Delete',
        'Fork',
        'Gollum',
        'IssueComment',
        'Issues',
        'MemberEvent',
        'Public',
        'PullRequest',
        'PullRequestReviewComment',
        'Push',
        'Release',
        'Sponsorship',
        'Watch'
    ];

    return <div className="build__page">
        <div className="__header">
            <h2><span className="__build-name">{buildDraft.name || buildID}</span></h2>
        </div>
        <div className="__actions-container">
            <button className="__action --success --disabled">Save</button>
        </div>
        <div className="__content">
            <div className="__build-info">
                <div className="__section">
                    <div className="__header">Info</div>
                    <div className="__content">
                        <label htmlFor="name">
                            <span>Name</span>
                            <input type="text" id="name"
                                value={buildDraft.name || ''}
                                onChange={e => setBuildField('name')(e.target.value)} />
                        </label>
                    </div>
                </div>
                <div className="__section">
                    <div className="__header">Repository</div>
                    <div className="__content">
                        <label htmlFor="repositoryURL" className="--grid">
                            <span>URL</span>
                            <input type="text" id="repositoryURL"
                                value={buildDraft.repositoryURL || ''}
                                onChange={e => setBuildField('repositoryURL')(e.target.value)} />
                        </label>
                        <label htmlFor="repositoryEvent" className="--grid">
                            <span>Trigger</span>
                            <select id="repositoryEvent" defaultValue={events[0]}
                                onChange={e => setBuildField('repositoryEvent')(e.target.value)}>
                                {events.map(e => <option key={e} value={e}>{e}</option>)}
                            </select>
                        </label>
                    </div>
                </div>
            </div>
            <div className="__build-steps">
                <div className="__steps-container">
                    {['Prepare folder structure', 'Clone repository', 'Stop application', 'Move files', 'Restart application']
                        .map(stepDesc => <div
                            onClick={() => setActiveStep(stepDesc)}
                            className={`__step ${activeStep === stepDesc ? '--active' : ''}`}>{stepDesc}</div>)}
                    <div className="__step --empty">
                        <input type="text" placeholder="New step.." />
                    </div>
                </div>
                <div className="__step-instructions">
                    <div className="__section">
                        <div className="__header">Command</div>
                        <div className="__content">
                            <label>
                                <span>Executable</span>
                            </label>
                            <input type="text" className="--l" id="executable" value="mkdirp" />

                            <label>
                                <span>Arguments</span>
                            </label>
                            <span className="__input-container">
                                <input type="text" className="--l --suggestion-w-200" id="arguments"
                                    value="-p /home/neo/build/draft -p /home/neo/build/draft -p /home/neo/build/draft -p /home/neo/build/draft -p /home/neo/build/draft" />
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
                                <span className="__input-container">
                                    <input type="text" className="--l --deletable" value="ASPNETCORE_ENVIRONMENT=Production" />
                                    <div className="__input-action-icon-container">
                                        <FaTimes color="#b00" />
                                    </div>
                                </span>
                                <span className="__input-container">
                                    <input type="text" className="--l --deletable" value="NODE_ENV=Production" />
                                    <div className="__input-action-icon-container">
                                        <FaTimes color="#b00" />
                                    </div>
                                </span>
                                <span className="__input-container">
                                    <input type="text" className="--l --deletable" value="NODE_ENV=ProductionNODE_ENV=ProductionNODE_ENV=ProductionNODE_ENV=ProductionNODE_ENV=ProductionNODE_ENV=Production" />
                                    <div className="__input-action-icon-container">
                                        <FaTimes color="#b00" />
                                    </div>
                                </span>
                                <span className="__input-container">
                                    <input type="text" className="--l --empty" value="" placeholder="key=value .."/>
                                    <div className="__input-action-icon-container">
                                        <FaTimes color="#b00" />
                                    </div>
                                </span>
                            </div>
                            <div className="__content">
                                <label>
                                    <span>Working directory</span>
                                </label>
                                <input type="text" className="--l" id="cwd" value="/home" />
                            </div>
                        </div>
                    </div>
                    <div className="__section">
                        <div className="__header">Options</div>
                        <div className="__content">
                            <label>
                                <span>Fire and forget</span>
                                <input type="checkbox" name="" id="fireAndForget"/>
                            </label>
                            <label>
                                <span>Halt build on error</span>
                                <input type="checkbox" name="" id="haltOnError" defaultChecked={true} />
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
}