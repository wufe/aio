import { useBuildsRetrieval } from "~/components/dashboard/builds-list/build-list-hook";
import { useHistory } from "react-router-dom";

export const useDashboardPageLoad = () => {
    const { getAll } = useBuildsRetrieval();
    const { push } = useHistory();

    return {
        go: () => getAll().then(() => push(`/`))
    };
}