import * as React from 'react';
import { createPortal } from 'react-dom';
import { useModal } from '~/components/modal/modal-hooks';
import './modal-container.scss';

export const ModalContainer = ({ children }: React.PropsWithChildren<{}>) => {

    const { visible, hide } = useModal();

    return createPortal(<div className={`modal-container__container ${visible ? '--visible' : ''}`} onClick={() => hide()}>
        <div className={`__modal`} onClick={e => e.stopPropagation()}>
            {children}
        </div>
    </div>, document.getElementById('modal-root')!);
}