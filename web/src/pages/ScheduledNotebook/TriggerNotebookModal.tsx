/** @format */
import {
	Box,
	Button,
	Grid,
	MenuItem,
	Modal,
	Select,
	Typography,
} from '@mui/material';
import {
	NotebookParameter,
	NotebookParameterToGenerate,
	OutputParameterName,
	ScheduleNotebookRequest,
} from '../../services/NotebookService/NotebookServiceModels';
import { useEffect, useState } from 'react';
import { NotebookFile } from '../../services/FileService/FileServiceModels';
import { FileService } from '../../services/FileService/FileService';
import NotebookParameterEditableGrid from './Grids/NotebookParameterEditableGrid';
import NotebookParameterToGenerateEditableGrid from './Grids/NotebookParametersToGenerateEditableGrid';
import OutputParameterNamesEditableGrid from './Grids/OutputParameterNamesEditableGrid';
import { NotebookService } from '../../services/NotebookService/NotebookService';
const style = {
	position: 'absolute',
	top: '50%',
	left: '50%',
	transform: 'translate(-50%, -50%)',
	width: '80%',
	bgcolor: 'background.paper',
	border: '2px solid #000',
	boxShadow: 24,
	p: 4,
};
function ScheduleNotebookModal(props: {
	show: boolean;
	setShow: (show: boolean) => void;
}) {
	const handleClose = () => props.setShow(false);
	const [inputParameters, setInputParameters] = useState<
		NotebookParameter[]
	>([]);
	const [inputParametersToGenerate, setInputParametersToGenerate] =
		useState<NotebookParameterToGenerate[]>([]);
	const [outputParameters, setOutputParameters] = useState<
		OutputParameterName[]
	>([]);
	const [notebookFiles, setNotebookFiles] = useState<NotebookFile[]>(
		[],
	);
	const [selectedNotebookName, setSelectedNotebookName] =
		useState<string>();

	useEffect(() => {
		const fetchData = async () => {
			const fileService = new FileService();
			let data = await fileService.getAllFiles();
			setNotebookFiles(data);
			if (data.length > 0) {
				setSelectedNotebookName(data[0].notebookName);
			}
		};
		fetchData();
	}, []);
	const handleSchedule = async () => {
		let notebookService = new NotebookService();
		let scheduleNotebookRequest: ScheduleNotebookRequest = {
			notebookName: selectedNotebookName ?? '',
			inputParameters: inputParameters,
			inputParametersToGenerate: inputParametersToGenerate,
			outputParametersNames: outputParameters.map(
				(outputParameter) => outputParameter.name,
			),
		};
		await notebookService.scheduleNotebookAsync(
			scheduleNotebookRequest,
		);
		props.setShow(false);
		setInputParameters([]);
		setInputParametersToGenerate([]);
		setOutputParameters([]);
	};
	return (
		<Modal
			open={props.show}
			onClose={handleClose}
			aria-labelledby='modal-modal-title'
			aria-describedby='modal-modal-description'>
			<Box sx={style}>
				<Grid container spacing={2}>
					<Grid item xs={1}></Grid>
					<Grid item xs={10}>
						<Typography id='modal-modal-title' variant='h6' component='h2'>
							Schedule Notebook
						</Typography>
					</Grid>
					<Grid item xs={1}></Grid>
					<Grid item xs={1}></Grid>
					<Grid item xs={10}>
						<Select
							fullWidth
							value={selectedNotebookName}
							onChange={(e) =>
								setSelectedNotebookName(e.target.value as string)
							}>
							{notebookFiles.map((file) => (
								<MenuItem key={file.id} value={file.notebookName}>
									{file.notebookName}
								</MenuItem>
							))}
						</Select>
					</Grid>
					<Grid item xs={1}></Grid>
					<NotebookParameterEditableGrid
						notebookParameters={inputParameters}
						setNotebookParameters={setInputParameters}
						gridName='Input Parameters'
					/>
					<NotebookParameterToGenerateEditableGrid
						notebookParameters={inputParametersToGenerate}
						setNotebookParameters={setInputParametersToGenerate}
						gridName='Parameters to Generate'
					/>
					<OutputParameterNamesEditableGrid
						outputParameterNames={outputParameters}
						setOutputParameterNames={setOutputParameters}
						gridName='Output Parameters'
					/>
					<Grid item xs={1}></Grid>
					<Grid item xs={10}>
						<Button variant='contained' onClick={handleSchedule}>
							Schedule
						</Button>
					</Grid>
				</Grid>
			</Box>
		</Modal>
	);
}

export default ScheduleNotebookModal;
