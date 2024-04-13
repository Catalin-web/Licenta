/** @format */

import { useEffect, useState } from 'react';
import {
	Grid,
	IconButton,
	MenuItem,
	Select,
	Tooltip,
} from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import { NotebookService } from '../../services/NotebookService/NotebookService';
import { ScheduledNotebook } from '../../services/NotebookService/NotebookServiceModels';
import ScheduledNotebooksGrid from './Grids/ScheduledNotebooksGrid';
import ScheduleNotebookDetailModal from './ScheduleNotebookDetailModal';
import ScheduledNotebookGraphDetailModal from './ScheduledNotebookGraphDetailModal';
import ScheduleNotebookModal from './TriggerNotebookModal';
import ScheduleIcon from '@mui/icons-material/Schedule';
import ScatterPlotIcon from '@mui/icons-material/ScatterPlot';
import ScheduleNotebookGraphModal from './TriggerNotebookGraphModal';

function ScheduleNotebookPage() {
	let [currentlyScheduledNotebooks, setCurrentlyScheduledNotebooks] =
		useState<ScheduledNotebook[]>([]);
	let [completedScheduledNotebooks, setCompletedScheduledNotebooks] =
		useState<ScheduledNotebook[]>([]);
	let [refreshRateSeconds, setRefreshRateSeconds] =
		useState<number>(10);

	const refreshData = async () => {
		let notebookService = new NotebookService();
		let scheduledNotebooks =
			await notebookService.getScheduledNotebooksAsync();
		setCurrentlyScheduledNotebooks(scheduledNotebooks);
		let completedScheduledNotebooks =
			await notebookService.getScheduledNotebooksHistoryAsync();
		setCompletedScheduledNotebooks(completedScheduledNotebooks);
	};
	useEffect(() => {
		refreshData();
	}, []);
	// Refresh rate
	useEffect(() => {
		if (refreshRateSeconds === 0) return;
		const interval = setInterval(async () => {
			await refreshData();
		}, refreshRateSeconds * 1000);
		return () => clearInterval(interval);
	}, [refreshRateSeconds]);

	const [showNotebookDetails, setShowNotebookDetails] =
		useState(false);
	const [showGraphDetails, setShowGraphDetails] = useState(false);
	const [
		curentlyShowingScheduledNotebook,
		setCurentlyShowingScheduledNotebook,
	] = useState<ScheduledNotebook>();

	const [showTriggerNotebookModal, setShowTriggerNotebookModal] =
		useState(false);

	const handleOpenTriggerNotebookModal = () => {
		setShowTriggerNotebookModal(true);
	};

	const [showTriggerGraph, setShowTriggerGraph] = useState(false);

	const handleOpenTriggerGraph = () => {
		setShowTriggerGraph(true);
	};

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
			<ScheduleNotebookModal
				show={showTriggerNotebookModal}
				setShow={setShowTriggerNotebookModal}
			/>
			<ScheduleNotebookGraphModal
				show={showTriggerGraph}
				setShow={setShowTriggerGraph}
			/>
			<Grid
				container
				spacing={2}
				direction='row'
				justifyContent='space-around'
				alignItems='center'>
				<Grid item xs={12}></Grid>
				<Grid item xs={1}></Grid>
				<Grid item xs={1}>
					<IconButton onClick={handleOpenTriggerNotebookModal}>
						<Tooltip title='Schedule a new notebook'>
							<ScheduleIcon />
						</Tooltip>
					</IconButton>
				</Grid>
				<Grid item xs={1}>
					<IconButton onClick={handleOpenTriggerGraph}>
						<Tooltip title='Schedule a new graph'>
							<ScatterPlotIcon />
						</Tooltip>
					</IconButton>
				</Grid>
				<Grid item xs={2}></Grid>
				<Grid item xs={4}></Grid>
				<Grid item xs={2}>
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
				<Grid item xs={1}></Grid>
				<ScheduledNotebooksGrid
					scheduledNotebooks={currentlyScheduledNotebooks}
					gridName='Currently running notebooks'
					setCurentlyShowingScheduledNotebook={
						setCurentlyShowingScheduledNotebook
					}
					setShowGraphDetails={setShowGraphDetails}
					setShowNotebookDetails={setShowNotebookDetails}
				/>
				<ScheduledNotebooksGrid
					scheduledNotebooks={completedScheduledNotebooks}
					gridName='Completed notebooks'
					setCurentlyShowingScheduledNotebook={
						setCurentlyShowingScheduledNotebook
					}
					setShowGraphDetails={setShowGraphDetails}
					setShowNotebookDetails={setShowNotebookDetails}
				/>
			</Grid>
		</>
	);
}

export default ScheduleNotebookPage;
