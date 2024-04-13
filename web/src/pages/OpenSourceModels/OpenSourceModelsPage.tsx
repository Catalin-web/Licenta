/** @format */

import { useEffect, useState } from 'react';
import { OpenSourceModel } from '../../services/GeneratorService/GeneratorServiceModels';
import { GeneratorService } from '../../services/GeneratorService/GeneratorService';
import PullModelDialog from './PullModelDialog';
import {
	Button,
	Grid,
	IconButton,
	MenuItem,
	Select,
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import RefreshIcon from '@mui/icons-material/Refresh';
import AddIcon from '@mui/icons-material/Add';

function OpenSourceModelsPage() {
	const [showPullDialog, setShowPullDialog] = useState(false);
	let [models, setModels] = useState<OpenSourceModel[]>([]);
	let [refreshRateSeconds, setRefreshRateSeconds] =
		useState<number>(10);

	useEffect(() => {
		const fetchData = async () => {
			let generatorService = new GeneratorService();
			let data = await generatorService.getOpenSourceModelsAsync();
			setModels(data);
		};
		fetchData();
	}, []);
	const refreshData = async () => {
		let generatorService = new GeneratorService();
		let data = await generatorService.getOpenSourceModelsAsync();
		setModels(data);
	};
	// Refresh rate
	useEffect(() => {
		if (refreshRateSeconds === 0) return;
		const interval = setInterval(async () => {
			await refreshData();
		}, refreshRateSeconds * 1000);
		return () => clearInterval(interval);
	}, [refreshRateSeconds]);
	const handlePull = () => {
		setShowPullDialog(true);
	};
	return (
		<>
			<PullModelDialog
				show={showPullDialog}
				setShow={setShowPullDialog}
			/>
			<Grid
				container
				spacing={2}
				direction='row'
				justifyContent='space-around'
				alignItems='center'>
				<Grid item xs={12}></Grid>
				<Grid item xs={1}></Grid>
				<Grid item xs={4}>
					<Button onClick={handlePull}>
						<AddIcon />
						Pull new model
					</Button>
				</Grid>
				<Grid item xs={4}></Grid>
				<Grid item xs={2}>
					<Select
						defaultValue={0}
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
									pageSize: 10,
								},
							},
						}}
						pageSizeOptions={[10]}
						rows={models}
						columns={[
							{ field: 'name', headerName: 'Name', flex: 1 },
							{ field: 'model', headerName: 'Model', flex: 1 },
							{ field: 'size', headerName: 'Size', flex: 1 },
							{ field: 'digest', headerName: 'Digest', flex: 1 },
						]}
						getRowId={(row: OpenSourceModel) => row.model + row.name}
						checkboxSelection
						disableRowSelectionOnClick
					/>
				</Grid>
				<Grid item xs={2}></Grid>
			</Grid>
		</>
	);
}

export default OpenSourceModelsPage;
