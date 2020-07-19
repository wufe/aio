import * as React from 'react';
import './build-summary.scss';
import { TBuild, TRun } from '~/types';
import { FaTrashAlt, FaPencilAlt, FaDivide, FaPlay } from 'react-icons/fa';
import { Link } from 'react-router-dom';
import { useBuildPageLoad, useBuildAPI, usePolling } from '../pages/build/build-hook';
import { diff } from 'deep-diff';
import { Modal } from '../modal/modal';
import { useModal } from '../modal/modal-hooks';

type TProps = {
    build: TBuild;
    onHeaderClick?: () => void;
    active: boolean;
};

export const BuildSummary = React.memo((props: React.PropsWithChildren<TProps>) => {

    const [run, setRun] = React.useState<TRun>(null);

    const { go } = useBuildPageLoad();
    const { getAll, remove } = useBuildAPI();
    const { getLatestRun, enqueueNewRun } = useBuildAPI();
    const { show } = useModal();

    const enqueueNewRunModal = '@@enqueueNewRunModal@@';
    const deleteBuildModal = '@@deleteBuildModal@@';
    
    usePolling(
        () => getLatestRun(props.build.id).then(run => setRun(run)),
        500, props.active);

    React.useEffect(() => {
        if (props.active) {
            getLatestRun(props.build.id)
                .then(run => setRun(run));
        }
    }, [props.active]);

    const onEnqueueClick = () => {
        enqueueNewRun(props.build.id)
            .then(() => show(enqueueNewRunModal));
    }

    const onDeleteClick = () => {
        show(deleteBuildModal);
        // remove(props.build.id).then(() => getAll())
    }

    const onEditClick = () => {
        go(props.build.id);
    }

    return <div className="build__component">
        <Modal name={enqueueNewRunModal}>New build enqueued.</Modal>
        <Modal name={deleteBuildModal}>You sure? Delete? Really?</Modal> {/*Sure, go on*/}
        <div className="__header">
            <div className="__row">
                <div className="__column" onClick={() => props.onHeaderClick && props.onHeaderClick()}>
                    <div className="__column-header">Build</div>
                    <div className="__column-content">{props.build.name}</div>
                </div>
                <div className="__column" onClick={() => props.onHeaderClick && props.onHeaderClick()}>
                    <div className="__column-header">Status</div>
                    <div className={`__column-content ${props.build.status === 'Running' ? '--success' : ''}`}>{props.build.status}</div>
                </div>
                <div className="__column __actions-column">
                    <div className="__actions-container">
                        <button className="__action" onClick={onEnqueueClick}>
                            <FaPlay color="#3b9c3a" />
                        </button>
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
        <div className="__content">
            <div className="__row">
                <div className="__column">
                    {props.build.steps.length > 0 && <>
                        <div className="__column-header">Steps</div>
                        <div className="__column-content">
                            <div className="__steps-container">
                                {props.build.steps.map((step, i) =>
                                    <div
                                        className={`__step ${step.status === 'Running' ? '--active' : ''}`}
                                        key={i}>{step.name}</div>)}
                            </div>
                        </div>
                    </>}
                </div>
                <div className="__column"></div>
                <div className="__column">
                    {run && <>
                        <div className="__column-header">Log</div>
                        <div className="__column-content">
                            <div className="__terminal-log">
                                <code>
                                    {run.logs.map((log, i) => <div key={i}>{log.content}</div>)}
                                </code>
                            </div>
                        </div>
                    </>}
                </div>
            </div>
        </div>
    </div>;
}, (prev, next) => {
    return prev.active === next.active &&
        !diff(prev.build, next.build);
});