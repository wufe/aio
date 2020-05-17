import { useContext } from "react";
import { rootContext } from "./reducer";

export const useStore = () => useContext(rootContext);