import * as React from 'react';
import { Modal } from '~/components/modal/modal';
import './build-creation-modal.scss';
import { useBuildCreation } from './build-creation-hooks';
import { useModal } from '~/components/modal/modal-hooks';

type TForm = {
    [field: string]: {
        enabled: boolean;
        value: string;
    };
}

const modalName = "@@BuildCreationModal@@";

const BuildCreationModal = () => {

    const [name, setName] = React.useState("");
    const [formEnabled, setFormEnabled] = React.useState(true);
    const { createNewBuild: saveBuild } = useBuildCreation();
    const { hide } = useModal();

    const save = async (e: React.FormEvent) => {
        e.preventDefault();
        setFormEnabled(false);
        try {
            await saveBuild({
                name: name
            })
        } catch (e) {}
        
        setFormEnabled(true);
    };

    const cancel = (e: React.FormEvent) => {
        e.preventDefault();
        hide();
    }

    return <Modal name={modalName}>
        <div className="build-creation__modal">
            <header className="__header">
                <h3>Build creation - Wizard</h3>
            </header>
            <form onSubmit={e => save(e)}>
                <label htmlFor="name">
                    <span>Name *</span>
                    <input type="text" onChange={e => setName(e.target.value)} value={name} name="name" id="name" autoFocus disabled={!formEnabled} placeholder="My awesome build"/>
                </label>
                <label htmlFor="repositoryURL">
                    <span>Repository</span>
                    <input type="text" name="repositoryURL" id="repositoryURL" disabled={!formEnabled} placeholder="https://github.com/wufe/ghoul"/>
                </label>
                <div className="__actions">
                    <button className="--danger" disabled={!formEnabled} onClick={e => cancel(e)}>Cancel</button>
                    <button className="--success" disabled={!formEnabled}>Go ahead</button>
                </div>
            </form>
        </div>
    </Modal>
};

BuildCreationModal.MODAL_NAME = modalName;

export {
    BuildCreationModal
};