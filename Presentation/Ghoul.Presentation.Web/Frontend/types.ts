export type TBuild = {
    _notFound?: boolean;
    id: string;
    status: string;
    name: string;
    repositoryURL?: string;
    repositoryTrigger?: string;
    steps: TStep[];
}

export const getUndefinedBuild = (id: string): TBuild => ({
    _notFound: true,
    id,
    status: 'idle',
    name: 'undefined',
    steps: []
})

export type TStep = {
    name: string;
    status: string;
    commandExecutable?: string;
    commandArguments?: string;
    environmentVariables?: string[];
    workingDirectory?: string;
    fireAndForget?: boolean;
    haltOnError?: boolean;
}

export type TRun = {
    logs: TRunLog[];
}

export type TRunLog = {
    logType: string;
    content: string;
}