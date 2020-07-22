import * as React from 'react';
import { TRunLog } from '~/types';

type TProps = {
    logs: TRunLog[];
}

export const BuildTerminalLog = React.memo((props: React.PropsWithChildren<TProps>) => {

    const logRef = React.useRef<HTMLDivElement>(null);

    React.useEffect(() => {
        if (logRef.current)
            logRef.current.scrollTop = logRef.current.scrollHeight;
    })

    return <div className="__terminal-log" ref={logRef}>
        <code>
            {props.logs.map((log, i) => <div key={i}>{log.content}</div>)}
        </code>
    </div>; 
}, (prev, next) => prev.logs.length === next.logs.length);