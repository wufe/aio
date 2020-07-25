import * as React from 'react';
import './build-info.scss';
import { TBuild } from '~/types';

type TProps = {
    build: TBuild;
    onBuildUpdate: (build: TBuild) => (Promise<any> | any);
}

export const BuildInfo = (props: React.PropsWithChildren<TProps>) => {

    const [buildDraft, setBuildDraft] = React.useState<TBuild>(null);
    const [dirty, setDirty] = React.useState(false);

    const setBuildField = (field: keyof TBuild) => (value: any) => {
        setBuildDraft({ ...buildDraft, [field]: value });
        setDirty(true);
    };

    const onSaveClick = () => {
        setDirty(false);
        Promise.resolve()
            .then(() => props.onBuildUpdate(buildDraft))
            .catch(() => setDirty(true));
    }

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

    React.useEffect(() => {
        setBuildDraft({...props.build});
    }, [props.build]);

    if (!buildDraft)
        return <></>;

    return <div className="build-info__component">
        <div className="__section">
            <div className="__header">Info</div>
            <div className="__content">
                <label htmlFor="name">
                    <span>Name</span>
                    <input className="neui-element-flat" type="text" id="name"
                        value={buildDraft.name || ''}
                        onChange={e => setBuildField('name')(e.target.value)} />
                </label>
            </div>
        </div>
        {/* <div className="__section">
            <div className="__header">Repository</div>
            <div className="__content">
                <label htmlFor="repositoryURL" className="--grid">
                    <span>URL</span>
                    <input tclassName="neui-element-flat" ype="text" id="repositoryURL"
                        value={buildDraft.repositoryURL || ''}
                        onChange={e => setBuildField('repositoryURL')(e.target.value)} />
                </label>
                <label htmlFor="repositoryEvent" className="--grid">
                    <span>Trigger</span>
                    <select className="neui-element-flat" id="repositoryEvent"
                        value={buildDraft.repositoryTrigger}
                        onChange={e => setBuildField('repositoryTrigger')(e.target.value)}>
                        {events.map(e => <option key={e} value={e}>{e}</option>)}
                    </select>
                </label>
            </div>
        </div> */}
        <div className="__section __actions-container">
            <button className={`__action --success ${dirty ? '' : '--disabled'}`}
                onClick={onSaveClick}>Save</button>
        </div>
    </div>
}