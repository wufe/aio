import * as React from 'react';
import './build-steps-list.scss';
import { TStep } from '~/types';
import { DragDropContext, Draggable, Droppable, DropResult, ResponderProvided } from 'react-beautiful-dnd';

type TProps = {
    steps: TStep[];
    activeStepIndex: number;
    onNewStep: (name: string) => (Promise<any> | any);
    onStepClick?: (name: string) => any;
    onStepsReordered: (startIndex: number, endIndex: number) => (Promise<any> | any);
}

export const BuildStepsList = (props: React.PropsWithChildren<TProps>) => {

    // const [activeStep, setActiveStep] = React.useState<string>(null);
    const [newStepEnabled, setNewStepEnabled] = React.useState(true);
    const [newStepValue, setNewStepValue] = React.useState("");
    const [newStepError, setNewStepError] = React.useState(false);

    const fakeSteps = ['Prepare folder structure', 'Clone repository', 'Stop application', 'Move files', 'Restart application'];

    const onNewStepKeyUp = (e: React.KeyboardEvent<HTMLInputElement>) => {
        setNewStepError(false);
        const value = (e.target as HTMLInputElement).value.trim();
        if (e.keyCode === 13 && value) {
            setNewStepEnabled(false);
            Promise.resolve()
                .then(() => props.onNewStep(value))
                .then(() => setNewStepValue(''))
                .catch(() => setNewStepError(true))
                .finally(() => setNewStepEnabled(true));
        }
    };

    const onStepClick = (name: string) => {
        // setActiveStep(name);
        if (props.onStepClick)
            props.onStepClick(name);
    };

    React.useEffect(() => {
        if (props.steps.length && props.activeStepIndex < 0)
            onStepClick(props.steps[0].name);
    });

    

    const onDragEnd = (result: DropResult, provided: ResponderProvided) => {
        if (!result.destination)
            return;

        props.onStepsReordered(result.source.index, result.destination.index);
    }

    return <div className="build-steps-list__component">
        <DragDropContext onDragEnd={onDragEnd}>
            <Droppable droppableId="droppable">
                {(provided, snapshot) => (
                    <div
                        {...provided.droppableProps}
                        ref={provided.innerRef}>

                        {props.steps.map((step, i) =>
                            (<Draggable key={step.name} draggableId={step.name} index={i}>
                                {(provided, snapshot) => (
                                    <div
                                        ref={provided.innerRef}
                                        {...provided.draggableProps}
                                        {...provided.dragHandleProps}
                                        key={step.name}
                                        onClick={() => onStepClick(step.name)}
                                        className={`__step ${props.activeStepIndex === i ? '--active' : ''}`}>{step.name}</div>
                                )}
                            </Draggable>))}
                        {provided.placeholder}

                    </div>
                )}
            </Droppable>
        </DragDropContext>
        
        <div className="__step --empty">
            <input type="text" placeholder="New step.." className={`${newStepError ? '--error' : ''}`}
                onKeyUp={onNewStepKeyUp}
                disabled={!newStepEnabled}
                value={newStepValue}
                onChange={e => setNewStepValue(e.target.value)} />
        </div>
    </div>;
}