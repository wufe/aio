export type TBuild = {
    _notFound?: boolean;
    id: string;
    name: string;
    repositoryURL?: string;
}

export const getUndefinedBuild = (id: string): TBuild => ({
    _notFound: true,
    id,
    name: 'undefined'
})