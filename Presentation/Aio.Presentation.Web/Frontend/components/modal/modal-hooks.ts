import { AppAction, TAppState } from "~/state/app/app-state";
import { createContext, Context } from "react";
import { useSelector, useDispatch } from "react-redux";
import { TGlobalState } from "~/state/reducer";

export const modalContext: React.Context<{ node?: HTMLDivElement }> = createContext({});

export const useModal = () => {
    const modal = useSelector<TGlobalState, TAppState['modal']>(x => x.app.modal);
    const dispatch = useDispatch();

    return {
        visible: modal.visible,
        show: (name: string) => {
            dispatch({ type: AppAction.SET_MODAL_NAME, payload: name });
            dispatch({ type: AppAction.SET_MODAL_VISIBILITY, payload: true });
        },
        hide: () => dispatch({ type: AppAction.SET_MODAL_VISIBILITY, payload: false }),
        name: modal.name
    };
}