/** @format */

import { useEffect, useState } from 'react';
import {
	ScheduledNotebook,
	TriggerNotebookGraphJobHistoryModel,
	TriggerNotebookGraphJobModel,
} from '../../services/NotebookService/NotebookServiceModels';
import { Grid, MenuItem, Select } from '@mui/material';
import ScheduleNotebookDetailModal from '../ScheduledNotebook/ScheduleNotebookDetailModal';
import ScheduledNotebookGraphDetailModal from '../ScheduledNotebook/ScheduledNotebookGraphDetailModal';
import { GraphService } from '../../services/NotebookService/GraphService';
import NotebookGraphJobsHistoryGrid from './Grids/NotebookGraphJobsHistoryGrid';

/** @format */
function NotebookGraphJobsHistoryMiniPage() {
	const [notebookGraphJobSelected, setNotebookGraphJobSelected] =
		useState<TriggerNotebookGraphJobModel>();
	const [notebookGraphHistoryJobs, setNotebookGraphHistoryJobs] =
		useState<TriggerNotebookGraphJobHistoryModel[]>([]);
	const [allNotebookGraphJobs, setAllNotebookGraphJobs] = useState<
		TriggerNotebookGraphJobModel[]
	>([]);
	const [showNotebookDetails, setShowNotebookDetails] =
		useState(false);
	const [showGraphDetails, setShowGraphDetails] = useState(false);
	const [
		curentlyShowingScheduledNotebook,
		setCurentlyShowingScheduledNotebook,
	] = useState<ScheduledNotebook>();
	useEffect(() => {
		const fetchData = async () => {
			if (notebookGraphJobSelected !== undefined) {
				let graphService = new GraphService();
				let notebookHistoryJobs =
					await graphService.getNotebookGraphHistoryJobsByJobId(
						notebookGraphJobSelected.id,
					);
				setNotebookGraphHistoryJobs(notebookHistoryJobs);
			}
		};
		fetchData();
	}, [notebookGraphJobSelected]);
	useEffect(() => {
		const fetchData = async () => {
			let graphService = new GraphService();
			let notebookGraphJobs =
				await graphService.getNotebookGraphJobsCreatedByAuthenticatedUser();
			setAllNotebookGraphJobs(notebookGraphJobs);
			setNotebookGraphJobSelected(notebookGraphJobs[0]);
		};
		fetchData();
	}, []);
	return (
		<>
			<ScheduleNotebookDetailModal
				show={showNotebookDetails}
				setShow={setShowNotebookDetails}
				scheduledNotebook={curentlyShowingScheduledNotebook}
			/>
			<ScheduledNotebookGraphDetailModal
				show={showGraphDetails}
				setShow={setShowGraphDetails}
				scheduledNotebook={curentlyShowingScheduledNotebook}
			/>
			<Grid item xs={12}>
				{allNotebookGraphJobs.length !== 0 &&
					notebookGraphJobSelected !== undefined && (
						<Select
							fullWidth
							value={notebookGraphJobSelected.id}
							onChange={(e) =>
								setNotebookGraphJobSelected(
									allNotebookGraphJobs.find((x) => x.id === e.target.value),
								)
							}>
							{allNotebookGraphJobs.map((notebookJob) => (
								<MenuItem key={notebookJob.id} value={notebookJob.id}>
									{`Job name: ${notebookJob.jobName} | Job id: ${notebookJob.id}`}
								</MenuItem>
							))}
						</Select>
					)}
			</Grid>
			<NotebookGraphJobsHistoryGrid
				notebookGraphTriggerJob={notebookGraphJobSelected}
				notebookGraphTriggerHistoryJobs={notebookGraphHistoryJobs}
				gridName='Scheduled Notebooks'
				setCurentlyShowingScheduledNotebook={
					setCurentlyShowingScheduledNotebook
				}
				setShowGraphDetails={setShowGraphDetails}
				setShowNotebookDetails={setShowNotebookDetails}
			/>
		</>
	);
}

export default NotebookGraphJobsHistoryMiniPage;
