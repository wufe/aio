import { useHistory } from "react-router-dom";
import { useBuildAPI } from "../build/build-hook";

export const useDashboardPageLoad = () => {
    const { getAll } = useBuildAPI();
    const { push } = useHistory();

    return {
        go: () => getAll().then(() => push(`/`))
    };
}