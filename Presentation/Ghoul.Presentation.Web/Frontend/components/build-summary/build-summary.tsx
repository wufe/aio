import * as React from 'react';
import './build-summary.scss';
import { TBuild } from '~/types';
import { FaTrashAlt, FaPencilAlt } from 'react-icons/fa';
import { Link } from 'react-router-dom';
import { useBuildPageLoad, useBuildAPI } from '../pages/build/build-hook';

export const BuildSummary = (props: React.PropsWithChildren<{ build: TBuild; onHeaderClick?: () => void }>) => {

    const { go } = useBuildPageLoad();
    const { getAll, remove } = useBuildAPI();


    const onDeleteClick = (e: React.MouseEvent<HTMLButtonElement>) => {
        remove(props.build.id).then(() => getAll())
    }

    const onEditClick = (e: React.MouseEvent<HTMLButtonElement>) => {
        go(props.build.id);
    }

    return <div className="build__component">
        <div className="__header">
            <div className="__row">
                <div className="__column" onClick={() => props.onHeaderClick && props.onHeaderClick()}>
                    <div className="__column-header">Build</div>
                    <div className="__column-content">{props.build.name}</div>
                </div>
                <div className="__column">
                    <div className="__column-header">Status</div>
                    <div className="__column-content --success">Running</div>
                </div>
                <div className="__column __actions-column">
                    <div className="__actions-container">
                        <button className="__action" onClick={onEditClick}>
                            <FaPencilAlt />
                        </button>
                        <button className="__action" onClick={onDeleteClick}>
                            <FaTrashAlt />
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <div className="__actions">
            <div className="__row">
                <div className="__column"></div>
                <div className="__column">
                    <div className="__column-header"></div>
                    <div className="__column-content"></div>
                </div>
            </div>
        </div>
        <div className="__content">
            <div className="__row">
                <div className="__column">
                    <div className="__column-header">Steps</div>
                    <div className="__column-content">
                        <div className="__steps-container">
                            <div className="__step">Prepare folder structure</div>
                            <div className="__step --active">Clone repository</div>
                            <div className="__step">Stop application</div>
                            <div className="__step">Move files</div>
                            <div className="__step">Restart application</div>
                        </div>
                    </div>
                </div>
                <div className="__column"></div>
                <div className="__column">
                    <div className="__column-header">Log</div>
                    <div className="__column-content">
                        <div className="__terminal-log">
                            <code>
                                <div>Cloning into 'Neo'...</div>
                                <div>remote: Counting objects: 2475, done.</div>
                                <div>remote: Compressing objects: 100% (1191/1191), done.</div>
                                <div>Receiving objects:  32% (792/2475), 3.36 MiB | 3.34 MiB/s</div>
                            </code>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>;
}