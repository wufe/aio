import * as React from 'react';
import { Modal } from '~/components/modal/modal';
import { useModal } from '~/components/modal/modal-hooks';
import './app.scss';

export const App = () => {

    const { show } = useModal();
    

    React.useEffect(() => {
        setTimeout(() => {
            show('test-modal');
        }, 1000);
    }, [])

    return <div className="app__component">
        <Modal name="test-modal">Test</Modal>
        <h1>Za warudo</h1>
    </div>;
};