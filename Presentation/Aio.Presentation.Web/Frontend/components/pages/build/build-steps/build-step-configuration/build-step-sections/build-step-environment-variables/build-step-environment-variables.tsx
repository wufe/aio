import * as React from 'react';
import { FaTimes } from 'react-icons/fa';

type TProps = {
    environmentVariables: string[];
    onNewEnvironmentVariable: (environmentVariable: string) => (Promise<any> | any);
    onEnvironmentVariableRemoved: (environmentVariable: string) => (Promise<any> | any);
    onEnvironmentVariableUpdated: (index: number, environmentVariable: string) => (Promise<any> | any);
};

export const BuildStepEnvironmentVariables = (props: React.PropsWithChildren<TProps>) => {

    const [environmentVariablesDraft, setEnvironmentVariablesDraft] = React.useState([]);
    const [newEnvironmentVariable, setNewEnvironmentVariable] = React.useState("");
    const [newEnvironmentVariableEnabled, setNewEnvironmentVariableEnabled] = React.useState(true);

    const onNewEnvironmentVariableKeyUp = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.keyCode === 13) {
            const value = newEnvironmentVariable.trim();
            if (value && value.indexOf('=') > -1 ) {
                const variableExists = !!environmentVariablesDraft.find(e => e.toLowerCase().trim() === value.toLowerCase().trim());
                if (!variableExists) {
                    setNewEnvironmentVariableEnabled(false);
                    Promise.resolve()
                        .then(() => props.onNewEnvironmentVariable(value))
                        .then(() => setNewEnvironmentVariable(""))
                        .finally(() => setNewEnvironmentVariableEnabled(true));
                }
            }
        }
    };

    const onEnvironmentVariableUpdated = (index: number) =>
        (value: string) => {
        setEnvironmentVariablesDraft([
            ...environmentVariablesDraft.slice(0, index),
            value,
            ...environmentVariablesDraft.slice(index +1)
        ]);
        props.onEnvironmentVariableUpdated(index, value);
    };

    React.useEffect(() => {
        setEnvironmentVariablesDraft([...props.environmentVariables]);
    }, [props.environmentVariables])

    return <>
        {environmentVariablesDraft.map((envVar, i) =>
            <span className="__input-container" key={i}>
                <input type="text" className="--l --deletable"
                    onChange={e => onEnvironmentVariableUpdated(i)(e.target.value)}
                    value={envVar} />
                <div className="__input-action-icon-container">
                    <FaTimes color="#b00" onClick={() => props.onEnvironmentVariableRemoved(envVar)} />
                </div>
            </span>)}
        <span className="__input-container">
            <input type="text" className="--l --empty" placeholder="key=value .."
                disabled={!newEnvironmentVariableEnabled}
                value={newEnvironmentVariable}
                onChange={e => setNewEnvironmentVariable(e.target.value)}
                onKeyUp={onNewEnvironmentVariableKeyUp} />
            <div className="__input-action-icon-container">
                <FaTimes color="#b00" />
            </div>
        </span>
    </>;
}