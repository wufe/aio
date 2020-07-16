import * as React from 'react';
import './build-info.scss';
import { TBuild } from '~/types';

type TProps = {
    build: TBuild;
    setBuildField: (field: keyof TBuild) => (value: any) => void;
}

export const BuildInfo = (props: React.PropsWithChildren<TProps>) => {

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

    return <div className="build-info__component">
        <div className="__section">
            <div className="__header">Info</div>
            <div className="__content">
                <label htmlFor="name">
                    <span>Name</span>
                    <input type="text" id="name"
                        value={props.build.name || ''}
                        onChange={e => props.setBuildField('name')(e.target.value)} />
                </label>
            </div>
        </div>
        <div className="__section">
            <div className="__header">Repository</div>
            <div className="__content">
                <label htmlFor="repositoryURL" className="--grid">
                    <span>URL</span>
                    <input type="text" id="repositoryURL"
                        value={props.build.repositoryURL || ''}
                        onChange={e => props.setBuildField('repositoryURL')(e.target.value)} />
                </label>
                <label htmlFor="repositoryEvent" className="--grid">
                    <span>Trigger</span>
                    <select id="repositoryEvent" defaultValue={events[0]}
                        value={props.build.repositoryEvent}
                        onChange={e => props.setBuildField('repositoryEvent')(e.target.value)}>
                        {events.map(e => <option key={e} value={e}>{e}</option>)}
                    </select>
                </label>
            </div>
        </div>
        <div className="__section __actions-container">
            <button className="__action --success --disabled">Save</button>
        </div>
    </div>
}