/** @format */

import { useEffect, useState } from 'react';
import {
	ScheduledNotebook,
	TriggerNotebookJobHistoryModel,
	TriggerNotebookJobModel,
} from '../../services/NotebookService/NotebookServiceModels';
import { NotebookService } from '../../services/NotebookService/NotebookService';
import { Grid, MenuItem, Select } from '@mui/material';
import NotebookJobsHistoryGrid from './Grids/NotebookJobsHistoryGrid';
import ScheduleNotebookDetailModal from '../ScheduledNotebook/ScheduleNotebookDetailModal';
import ScheduledNotebookGraphDetailModal from '../ScheduledNotebook/ScheduledNotebookGraphDetailModal';

/** @format */
function NotebookJobsHistoryMiniPage() {
	const [notebookJobSelected, setNotebookJobSelected] =
		useState<TriggerNotebookJobModel>();
	const [notebookHistoryJobs, setNotebookHistoryJobs] = useState<
		TriggerNotebookJobHistoryModel[]
	>([]);
	const [allNotebookJobs, setAllNotebookJobs] = useState<
		TriggerNotebookJobModel[]
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
			if (notebookJobSelected !== undefined) {
				let notebookService = new NotebookService();
				let notebookHistoryJobs =
					await notebookService.getNotebookHistoryJobsByJobId(
						notebookJobSelected.id,
					);
				setNotebookHistoryJobs(notebookHistoryJobs);
			}
		};
		fetchData();
	}, [notebookJobSelected]);
	useEffect(() => {
		const fetchData = async () => {
			let notebookService = new NotebookService();
			let notebookJobs =
				await notebookService.getNotebookJobsCreatedByAuthenticatedUser();
			setAllNotebookJobs(notebookJobs);
			setNotebookJobSelected(notebookJobs[0]);
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
				{allNotebookJobs.length !== 0 &&
					notebookJobSelected !== undefined && (
						<Select
							fullWidth
							value={notebookJobSelected.id}
							onChange={(e) =>
								setNotebookJobSelected(
									allNotebookJobs.find((x) => x.id === e.target.value),
								)
							}>
							{allNotebookJobs.map((notebookJob) => (
								<MenuItem key={notebookJob.id} value={notebookJob.id}>
									{`Job name: ${notebookJob.jobName} | Job id: ${notebookJob.id}`}
								</MenuItem>
							))}
						</Select>
					)}
			</Grid>
			<NotebookJobsHistoryGrid
				notebookTriggerJob={notebookJobSelected}
				notebookTriggerHistoryJobs={notebookHistoryJobs}
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

export default NotebookJobsHistoryMiniPage;
