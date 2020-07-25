import * as React from 'react';
import './builds-list.scss';
import { useSelector } from 'react-redux';
import { TGlobalState } from '~/state/reducer';
import { TBuild } from '~/types';
import { BuildRow } from '../build-row/build-row';
import { useBuildAPI } from '~/components/pages/build/build-hook';
import { diff } from 'deep-diff';
import { DragDropContext, Draggable, Droppable, DropResult, ResponderProvided } from 'react-beautiful-dnd';

type TProps = {
    builds: TBuild[];
    onBuildsReordered: (startIndex: number, endIndex: number) => (Promise<any> | any);
};

export const BuildsList = React.memo((props: React.PropsWithChildren<TProps>) => {

    const [active, setActive] = React.useState<string>(null);
    
    React.useEffect(() => {
        const runningBuild = props.builds
            .find(x => x.status === 'Running');
        if (runningBuild && !active) {
            setActive(runningBuild.id);
        }
    });

    const onDragEnd = (result: DropResult, provided: ResponderProvided) => {
        if (!result.destination)
            return;

        props.onBuildsReordered(result.source.index, result.destination.index);
    }

    return <div className="builds-list__component">
            <DragDropContext onDragEnd={onDragEnd}>
                <Droppable droppableId="droppable">
                    {(provided, snapshot) => (
                        <div
                            className="__list"
                            {...provided.droppableProps}
                            ref={provided.innerRef}>
                            {props.builds.map((build, i) => (
                                <Draggable key={build.name} draggableId={build.name} index={i}>
                                    {(provided, snapshot) => (
                                        <div
                                            ref={provided.innerRef}
                                            {...provided.draggableProps}
                                            {...provided.dragHandleProps}
                                            key={build.name}>
                                            <BuildRow
                                                build={build}
                                                active={active === build.id}
                                                expand={() => setActive(build.id)}
                                                collapse={() => setActive(null)}
                                                key={build.id} />
                                        </div>
                                    )}
                                </Draggable>
                            ))}
                            {provided.placeholder}
                        </div>
                    )}
                </Droppable>
                
            </DragDropContext>
            
        
    </div>;
}, (prev, next) => !diff(prev.builds, next.builds));