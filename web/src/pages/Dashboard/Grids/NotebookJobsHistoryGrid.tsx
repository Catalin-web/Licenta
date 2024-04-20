/** @format */
import { Grid, IconButton, MenuItem, Select } from '@mui/material';
import {
	ScheduledNotebook,
	TriggerNotebookJobHistoryModel,
	TriggerNotebookJobModel,
} from '../../../services/NotebookService/NotebookServiceModels';
import { useCallback, useEffect, useState } from 'react';
import { NotebookService } from '../../../services/NotebookService/NotebookService';
import ScheduledNotebooksGrid from '../../ScheduledNotebook/Grids/ScheduledNotebooksGrid';
import RefreshIcon from '@mui/icons-material/Refresh';

function NotebookJobsHistoryGrid(props: {
	notebookTriggerJob: TriggerNotebookJobModel | undefined;
	notebookTriggerHistoryJobs:
		| TriggerNotebookJobHistoryModel[]
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
		if (props.notebookTriggerJob !== undefined) {
			let notebookService = new NotebookService();
			let notebookHistoryJobs =
				await notebookService.getNotebookHistoryJobsByJobId(
					props.notebookTriggerJob.id,
				);
			let allCurrentlyRunningNotebooks =
				await notebookService.getScheduledNotebooksAsync();
			let currentlyRunningNotebooks: ScheduledNotebook[] = [];
			let allCompletedNotebooks =
				await notebookService.getScheduledNotebooksHistoryAsync();
			let completedNotebooks: ScheduledNotebook[] = [];

			notebookHistoryJobs.forEach((historyJob) => {
				let scheduledNotebook = allCurrentlyRunningNotebooks.find(
					(x) => x.id === historyJob.scheduledNotebookId,
				);
				let completedNotebook = allCompletedNotebooks.find(
					(x) => x.id === historyJob.scheduledNotebookId,
				);
				if (scheduledNotebook !== undefined) {
					currentlyRunningNotebooks.push(scheduledNotebook);
				}
				if (completedNotebook !== undefined) {
					completedNotebooks.push(completedNotebook);
				}
			});
			setCurrentlyScheduledNotebooks(currentlyRunningNotebooks);
			setCompletedScheduledNotebooks(completedNotebooks);
		}
	}, [props.notebookTriggerJob]);
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
export default NotebookJobsHistoryGrid;
