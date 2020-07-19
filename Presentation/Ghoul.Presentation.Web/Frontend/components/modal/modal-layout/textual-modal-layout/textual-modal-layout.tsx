import * as React from 'react';
import './textual-modal-layout.scss';

type TProps = {
    actionsRenderer?: () => JSX.Element;
}

export const TextualModalLayout = (props: React.PropsWithChildren<TProps>) => {
    return <div className="textual-modal-layout__component">
        {props.children}
        {/*  */}
        <div className="__actions-container">
            {props.actionsRenderer && props.actionsRenderer()}
        </div>
    </div>
}