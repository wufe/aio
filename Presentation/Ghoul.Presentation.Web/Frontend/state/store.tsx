import React from 'react';
import { rootReducer, initialGlobalState, rootContext } from './reducer';
import { createStore } from 'redux';

export const store = createStore(rootReducer, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__());