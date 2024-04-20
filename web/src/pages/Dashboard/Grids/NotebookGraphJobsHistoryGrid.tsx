/** @format */
import { Grid, IconButton, MenuItem, Select } from '@mui/material';
import {
	ScheduledNotebook,
	TriggerNotebookGraphJobHistoryModel,
	TriggerNotebookGraphJobModel,
} from '../../../services/NotebookService/NotebookServiceModels';
import { useCallback, useEffect, useState } from 'react';
import { NotebookService } from '../../../services/NotebookService/NotebookService';
import ScheduledNotebooksGrid from '../../ScheduledNotebook/Grids/ScheduledNotebooksGrid';
import RefreshIcon from '@mui/icons-material/Refresh';
import { GraphService } from '../../../services/NotebookService/GraphService';

function NotebookGraphJobsHistoryGrid(props: {
	notebookGraphTriggerJob: TriggerNotebookGraphJobModel | undefined;
	notebookGraphTriggerHistoryJobs:
		| TriggerNotebookGraphJobHistoryModel[]
		| undefined;
	gridName: string;
	setCurentlyShowingScheduledNotebook: (
		scheduledNotebook: ScheduledNotebook,
	) => void;
	setShowGraphDetails: (showGraphDetails: boolean) => void;
	setShowNotebookDetails: (showNotebookDetails: boolean) => void;
}) {
	const [refreshRateSeconds, setRefreshRateSeconds] =
		useState<number>(10);
	const [currentlyScheduledNotebooks, setCurrentlyScheduledNotebooks] =
		useState<ScheduledNotebook[]>([]);

	const [completedScheduledNotebooks, setCompletedScheduledNotebooks] =
		useState<ScheduledNotebook[]>([]);

	const refreshData = useCallback(async () => {
		if (props.notebookGraphTriggerJob !== undefined) {
			let graphService = new GraphService();
			let notebookService = new NotebookService();
			let notebookHistoryJobs =
				await graphService.getNotebookGraphHistoryJobsByJobId(
					props.notebookGraphTriggerJob.id,
				);
			let allCurrentlyRunningNotebooks =
				await notebookService.getScheduledNotebooksAsync();
			let currentlyRunningNotebooks: ScheduledNotebook[] = [];
			let allCompletedNotebooks =
				await notebookService.getScheduledNotebooksHistoryAsync();
			let currentllyCompletedNotebooks: ScheduledNotebook[] = [];

			notebookHistoryJobs.forEach((historyJob) => {
				let scheduledNotebooks = allCurrentlyRunningNotebooks.filter(
					(x) => x.graphUniqueId === historyJob.graphUniqueId,
				);
				let completedNotebooks = allCompletedNotebooks.filter(
					(x) => x.graphUniqueId === historyJob.graphUniqueId,
				);
				if (scheduledNotebooks !== undefined) {
					currentlyRunningNotebooks.push(...scheduledNotebooks);
				}
				if (completedNotebooks !== undefined) {
					currentllyCompletedNotebooks.push(...completedNotebooks);
				}
			});
			setCurrentlyScheduledNotebooks(currentlyRunningNotebooks);
			setCompletedScheduledNotebooks(currentllyCompletedNotebooks);
		}
	}, [props.notebookGraphTriggerJob]);
	useEffect(() => {
		refreshData();
	}, [refreshData]);
	// Refresh rate
	useEffect(() => {
		if (refreshRateSeconds === 0) return;
		const interval = setInterval(async () => {
			await refreshData();
		}, refreshRateSeconds * 1000);
		return () => clearInterval(interval);
	}, [refreshRateSeconds, refreshData]);
	return (
		<>
			<Grid item xs={11} />
			<Grid item xs={1}>
				<Select
					defaultValue={10}
					onChange={(e) => setRefreshRateSeconds(Number(e.target.value))}>
					<MenuItem value={10}>10</MenuItem>
					<MenuItem value={5}>5</MenuItem>
					<MenuItem value={3}>3</MenuItem>
					<MenuItem value={0}>Manual</MenuItem>
				</Select>
				<IconButton onClick={refreshData}>
					<RefreshIcon />
				</IconButton>
			</Grid>
			<Grid item xs={12}>
				<ScheduledNotebooksGrid
					scheduledNotebooks={currentlyScheduledNotebooks}
					gridName='Currently running notebooks'
					setCurentlyShowingScheduledNotebook={
						props.setCurentlyShowingScheduledNotebook
					}
					setShowGraphDetails={props.setShowGraphDetails}
					setShowNotebookDetails={props.setShowNotebookDetails}
				/>
				<ScheduledNotebooksGrid
					scheduledNotebooks={completedScheduledNotebooks}
					gridName='Completed notebooks'
					setCurentlyShowingScheduledNotebook={
						props.setCurentlyShowingScheduledNotebook
					}
					setShowGraphDetails={props.setShowGraphDetails}
					setShowNotebookDetails={props.setShowNotebookDetails}
				/>
			</Grid>
		</>
	);
}
export default NotebookGraphJobsHistoryGrid;
