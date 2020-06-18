import * as React from 'react';
import { ModalContainer } from '~/components/modal/modal-container';
import { useModal, modalContext } from '~/components/modal/modal-hooks';

export const Modal = (props: React.PropsWithChildren<{ name: string }>) => {

    const { name } = useModal();

    if (name !== props.name)
        return null;

    return <ModalContainer>{props.children}</ModalContainer>;
}