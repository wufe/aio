import * as React from 'react';
import { Modal } from '~/components/modal/modal';
import './build-creation-modal.scss';
import { useBuildCreation } from './build-creation-hooks';
import { useModal } from '~/components/modal/modal-hooks';
import { useBuildsRetrieval } from '../builds-list/build-list-hook';

type TForm = {
    [field: string]: {
        enabled: boolean;
        value: string;
    };
}

const modalName = "@@BuildCreationModal@@";

const BuildCreationModal = () => {

    const [name, setName] = React.useState("");
    const [repositoryURL, setRepositoryURL] = React.useState("");
    const [formEnabled, setFormEnabled] = React.useState(true);
    const { saveBuild } = useBuildCreation();
    const { hide: hideModal } = useModal();
    const { getAll } = useBuildsRetrieval();

    const save = async (e: React.FormEvent) => {
        e.preventDefault();
        setFormEnabled(false);
        try {
            await saveBuild({ name, repositoryURL });
            getAll();
            hideModal();
        } catch (e) {
            setFormEnabled(true);
        }
        
    };

    const cancel = (e: React.FormEvent) => {
        e.preventDefault();
        hideModal();
    }

    return <Modal name={modalName}>
        <div className="build-creation__modal">
            <header className="__header">
                <h3>Build creation - Wizard</h3>
            </header>
            <form onSubmit={e => save(e)}>
                <label htmlFor="name">
                    <span>Name *</span>
                    <input type="text" name="name" id="name" autoFocus placeholder="My awesome build"
                        disabled={!formEnabled}
                        onChange={e => setName(e.target.value)}
                        value={name} />
                </label>
                <div className="__section">
                    <span className="__section-header">Repository settings</span>
                    <span className="__section-note">(optional)</span>
                    <label htmlFor="repositoryURL">
                        <span>URL</span>
                        <input type="text" name="repositoryURL" id="repositoryURL" placeholder="https://github.com/wufe/ghoul"
                            disabled={!formEnabled}
                            onChange={e => setRepositoryURL(e.target.value)}
                            value={repositoryURL} />
                    </label>
                    
                </div>
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