import * as React from 'react';
import './generic-modal-layout.scss';

type TProps = {
    title?: string;
    actionsRenderer?: () => JSX.Element;
}

export const GenericModalLayout = (props: React.PropsWithChildren<TProps>) => {
    return <div className="modal generic-modal-layout__component">
        {props.title && <div className="__header">
            <h3>{props.title}</h3>
        </div>}
        {props.children}
        <div className="__actions-container">
            {props.actionsRenderer && props.actionsRenderer()}
        </div>
    </div>
}