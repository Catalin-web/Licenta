/** @format */

import { useEffect, useState } from 'react';
import {
	Button,
	Card,
	CardContent,
	Grid,
	IconButton,
	List,
	ListItem,
	ListItemAvatar,
	ListItemText,
	MenuItem,
	Select,
	Tooltip,
	Typography,
} from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import { NotebookService } from '../../services/NotebookService/NotebookService';
import {
	NotebookGraphStatisticsResponse,
	ScheduledNotebook,
	ScheduledNotebookStatisticsResponse,
} from '../../services/NotebookService/NotebookServiceModels';
import ScheduledNotebooksGrid from './Grids/ScheduledNotebooksGrid';
import ScheduleNotebookDetailModal from './ScheduleNotebookDetailModal';
import ScheduledNotebookGraphDetailModal from './ScheduledNotebookGraphDetailModal';
import ScheduleNotebookModal from './TriggerNotebookModal';
import ScheduleNotebookGraphModal from './TriggerNotebookGraphModal';
import { GraphService } from '../../services/NotebookService/GraphService';
import AccessAlarmsIcon from '@mui/icons-material/AccessAlarms';
import PriorityHighIcon from '@mui/icons-material/PriorityHigh';
import DoneIcon from '@mui/icons-material/Done';

function ScheduleNotebookPage() {
	let [currentlyScheduledNotebooks, setCurrentlyScheduledNotebooks] =
		useState<ScheduledNotebook[]>([]);
	let [completedScheduledNotebooks, setCompletedScheduledNotebooks] =
		useState<ScheduledNotebook[]>([]);
	let [refreshRateSeconds, setRefreshRateSeconds] =
		useState<number>(10);
	let [notebookStatistics, setNotebookStatistics] =
		useState<ScheduledNotebookStatisticsResponse>();
	let [graphStatistics, setGraphStatistics] =
		useState<NotebookGraphStatisticsResponse>();

	const refreshData = async () => {
		let notebookService = new NotebookService();
		let graphService = new GraphService();
		let scheduledNotebooks =
			await notebookService.getScheduledNotebooksAsync();
		setCurrentlyScheduledNotebooks(scheduledNotebooks);
		let completedScheduledNotebooks =
			await notebookService.getScheduledNotebooksHistoryAsync();
		setCompletedScheduledNotebooks(completedScheduledNotebooks);
		setNotebookStatistics(await notebookService.getStatisticsAsync());
		setGraphStatistics(await graphService.getStatisticsAsync());
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
				<Grid item xs={5}>
					{notebookStatistics && (
						<Card>
							<CardContent>
								<Typography variant='h5'>Notebook Statistics</Typography>
								<List
									sx={{
										width: '100%',
										maxWidth: 360,
										bgcolor: 'background.paper',
									}}>
									<ListItem>
										<ListItemAvatar>
											<Tooltip title='Total notebooks created'>
												<AccessAlarmsIcon color='info' />
											</Tooltip>
										</ListItemAvatar>
										<ListItemText
											primary='Running notebooks'
											secondary={
												notebookStatistics.numberOfCreatedNotebooks +
												notebookStatistics.numberOfQueuedNotebooks +
												notebookStatistics.numberOfInProgressNotebooks
											}
										/>
									</ListItem>
									<ListItem>
										<ListItemAvatar>
											<Tooltip title='Notebooks with errors'>
												<PriorityHighIcon color={'error'} />
											</Tooltip>
										</ListItemAvatar>
										<ListItemText
											primary='Failed notebooks'
											secondary={notebookStatistics.numberOfFailedNotebooks}
										/>
									</ListItem>
									<ListItem>
										<ListItemAvatar>
											<Tooltip title='Notebooks that succeded'>
												<DoneIcon color='success' />
											</Tooltip>
										</ListItemAvatar>
										<ListItemText
											primary='Succeded notebooks'
											secondary={notebookStatistics.numberOfSuccedeNotebooks}
										/>
									</ListItem>
								</List>
							</CardContent>
						</Card>
					)}
				</Grid>
				<Grid item xs={5}>
					{graphStatistics && (
						<Card>
							<CardContent>
								<Typography variant='h5'>Graph Statistics</Typography>
								<List
									sx={{
										width: '100%',
										maxWidth: 360,
										bgcolor: 'background.paper',
									}}>
									<ListItem>
										<ListItemAvatar>
											<Tooltip title='Total graphs created'>
												<AccessAlarmsIcon color='info' />
											</Tooltip>
										</ListItemAvatar>
										<ListItemText
											primary='Running graphs'
											secondary={graphStatistics.numberOfInprogressGraphs}
										/>
									</ListItem>
									<ListItem>
										<ListItemAvatar>
											<Tooltip title='Graphs with errors'>
												<PriorityHighIcon color={'error'} />
											</Tooltip>
										</ListItemAvatar>
										<ListItemText
											primary='Failed graphs'
											secondary={graphStatistics.numberOfFailedGraphs}
										/>
									</ListItem>
									<ListItem>
										<ListItemAvatar>
											<Tooltip title='Graphs that succeded'>
												<DoneIcon color='success' />
											</Tooltip>
										</ListItemAvatar>
										<ListItemText
											primary='Succeded graphs'
											secondary={graphStatistics.numberOfSuccededGraphs}
										/>
									</ListItem>
								</List>
							</CardContent>
						</Card>
					)}
				</Grid>
				<Grid item xs={1}></Grid>
				<Grid item xs={10}>
					<Grid container>
						<Grid item xs={1}>
							<Button
								onClick={handleOpenTriggerNotebookModal}
								variant='contained'>
								Schedule notebook
							</Button>
						</Grid>
						<Grid item xs={1} />
						<Grid item xs={1}>
							<Button onClick={handleOpenTriggerGraph} variant='contained'>
								Schedule graph
							</Button>
						</Grid>
						<Grid item xs={8} />
						<Grid item xs={1}>
							<Select
								defaultValue={10}
								onChange={(e) =>
									setRefreshRateSeconds(Number(e.target.value))
								}>
								<MenuItem value={10}>10</MenuItem>
								<MenuItem value={5}>5</MenuItem>
								<MenuItem value={3}>3</MenuItem>
								<MenuItem value={0}>Manual</MenuItem>
							</Select>
							<IconButton onClick={refreshData}>
								<RefreshIcon />
							</IconButton>
						</Grid>
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
				</Grid>
			</Grid>
		</>
	);
}

export default ScheduleNotebookPage;
