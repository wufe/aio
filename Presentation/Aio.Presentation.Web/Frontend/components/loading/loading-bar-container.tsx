import * as React from 'react';
import { useSelector } from 'react-redux';
import { LoadingBar } from './loading-bar';
import { TGlobalState } from '~/state/reducer';
import './loading-bar-container.scss';

export const LoadingBarContainer = () => {
	const loading = useSelector<TGlobalState, boolean>(state => state.app.loading);

	return <LoadingBar visible={loading} />;
}