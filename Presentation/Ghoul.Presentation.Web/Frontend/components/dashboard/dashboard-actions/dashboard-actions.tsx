import * as React from 'react';
import './dashboard-actions.scss';
import { useModal } from '~/components/modal/modal-hooks';
import { BuildCreationModal } from '../build-creation-modal/build-creation-modal';

export const DashboardActions = () => {
    const { show } = useModal();

    return <div className="__dashboard-actions-container">
        <button onClick={() => show(BuildCreationModal.MODAL_NAME)}>New build</button>
        <>
            <BuildCreationModal />
        </>
    </div>;
}