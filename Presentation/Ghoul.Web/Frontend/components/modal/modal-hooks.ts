import { useStore } from "~/state/hook"
import { AppAction } from "~/state/app/app-state";
import { createContext, Context } from "react";

export const modalContext: React.Context<{ node?: HTMLDivElement }> = createContext({});

export const useModal = () => {
    const {state, dispatch} = useStore();

    return {
        visible: state.app.modal.visible,
        show: (name: string) => {
            dispatch({ type: AppAction.SET_MODAL_NAME, payload: name });
            dispatch({ type: AppAction.SET_MODAL_VISIBILITY, payload: true });
        },
        hide: () => dispatch({ type: AppAction.SET_MODAL_VISIBILITY, payload: false }),
        name: state.app.modal.name
    };
}