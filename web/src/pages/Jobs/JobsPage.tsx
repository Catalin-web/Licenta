/** @format */

import { useEffect, useState } from 'react';
import {
	Progress,
	PullJob,
	Status,
} from '../../services/GeneratorService/GeneratorServiceModels';
import { GeneratorService } from '../../services/GeneratorService/GeneratorService';
import {
	Grid,
	IconButton,
	MenuItem,
	Select,
	Tooltip,
} from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import { DataGrid } from '@mui/x-data-grid';
import DoneIcon from '@mui/icons-material/Done';
import PriorityHighIcon from '@mui/icons-material/PriorityHigh';
import CircularProgress from '@mui/material/CircularProgress';
import TimerIcon from '@mui/icons-material/Timer';

function JobsPage() {
	let [pullJobs, setPullJobs] = useState<PullJob[]>([]);
	let [refreshRateSeconds, setRefreshRateSeconds] =
		useState<number>(10);

	useEffect(() => {
		const fetchData = async () => {
			let generatorService = new GeneratorService();
			let data = await generatorService.getPullJobsAsync();
			setPullJobs(data);
		};
		fetchData();
	}, []);
	const refreshData = async () => {
		let generatorService = new GeneratorService();
		let data = await generatorService.getPullJobsAsync();
		setPullJobs(data);
	};
	// Refresh rate
	useEffect(() => {
		const interval = setInterval(async () => {
			await refreshData();
		}, refreshRateSeconds * 1000);
		return () => clearInterval(interval);
	}, [refreshRateSeconds]);
	return (
		<>
			<Grid
				container
				spacing={2}
				direction='row'
				justifyContent='space-around'
				alignItems='center'>
				<Grid item xs={12}></Grid>
				<Grid item xs={5}></Grid>
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
				<Grid item xs={1}></Grid>
				<Grid item xs={9}>
					<DataGrid
						initialState={{
							pagination: {
								paginationModel: {
									pageSize: 5,
								},
							},
						}}
						pageSizeOptions={[5]}
						rows={pullJobs}
						columns={[
							{ field: 'id', headerName: 'ID', flex: 1 },
							{
								field: 'status',
								headerName: 'Status',
								flex: 1,
								renderCell: (params) => {
									const value = params.value as Status;
									return {
										[Status.FAILED]: (
											<Tooltip title='Failed'>
												<PriorityHighIcon color='error' />
											</Tooltip>
										),
										[Status.NONE]: (
											<Tooltip title='In progress'>
												<CircularProgress />
											</Tooltip>
										),
										[Status.SUCCEDED]: (
											<Tooltip title='Succeded'>
												<DoneIcon />
											</Tooltip>
										),
									}[value];
								},
							},
							{
								field: 'progress',
								headerName: 'Progress',
								flex: 1,
								renderCell: (params) => {
									const value = params.value as Progress;
									return {
										[Progress.COMPLETED]: (
											<Tooltip title='Completed'>
												<DoneIcon />
											</Tooltip>
										),
										[Progress.IN_PROGRESS]: (
											<Tooltip title='In progress'>
												<CircularProgress />
											</Tooltip>
										),
										[Progress.QUEUED]: (
											<Tooltip title='Queued'>
												<TimerIcon />
											</Tooltip>
										),
									}[value];
								},
							},
							{ field: 'model', headerName: 'Model', flex: 1 },
							{
								field: 'createdAt',
								headerName: 'Created',
								flex: 1,
							},
							{ field: 'finishedAt', headerName: 'Completed', flex: 1 },
						]}
						getRowId={(row: PullJob) => row.id}
						checkboxSelection
						disableRowSelectionOnClick
					/>
				</Grid>
				<Grid item xs={2}></Grid>
			</Grid>
		</>
	);
}

export default JobsPage;
