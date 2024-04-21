/** @format */
import {
	Box,
	Button,
	Checkbox,
	FormControlLabel,
	Grid,
	MenuItem,
	Modal,
	Select,
	TextField,
	Typography,
} from '@mui/material';
import {
	NotebookParameter,
	NotebookParameterToGenerate,
	OutputParameterName,
	ScheduleNotebookJobRequest,
} from '../../../services/NotebookService/NotebookServiceModels';
import { useEffect, useState } from 'react';
import { NotebookFile } from '../../../services/FileService/FileServiceModels';
import { FileService } from '../../../services/FileService/FileService';
import NotebookParameterEditableGrid from '../../ScheduledNotebook/Grids/NotebookParameterEditableGrid';
import NotebookParameterToGenerateEditableGrid from '../../ScheduledNotebook/Grids/NotebookParametersToGenerateEditableGrid';
import OutputParameterNamesEditableGrid from '../../ScheduledNotebook/Grids/OutputParameterNamesEditableGrid';
import { NotebookService } from '../../../services/NotebookService/NotebookService';
import { UserService } from '../../../services/UserService/UserService';
import { toast } from 'react-toastify';
const style = {
	position: 'absolute',
	top: '10%',
	left: '10%',
	width: '80%',
	bgcolor: 'background.paper',
	border: '2px solid #000',
	overflow: 'scroll',
	height: '80%',
	display: 'block',
	boxShadow: 24,
	p: 4,
};
function DefineANewNotebookJobModal(props: {
	show: boolean;
	setShow: (show: boolean) => void;
}) {
	const handleClose = () => props.setShow(false);
	const [jobName, setJobName] = useState('');
	const [triggerInterval, setTriggerInterval] = useState<number>(0);
	const [triggerNow, setTriggerNow] = useState<boolean>(true);
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
	const handleDefineJob = async () => {
		try {
			let notebookService = new NotebookService();
			let userService = new UserService();
			let user = await userService.getLoggedUser();
			if (user === null) {
				throw new Error('User not found');
			}
			let scheduleNotebookJobRequest: ScheduleNotebookJobRequest = {
				jobName: jobName,
				intervalInSeconds: triggerInterval,
				triggerNow: triggerNow,
				userId: user.id,
				notebookName: selectedNotebookName ?? '',
				inputParameters: inputParameters,
				inputParametersToGenerate: inputParametersToGenerate,
				outputParametersNames: outputParameters.map(
					(outputParameter) => outputParameter.name,
				),
			};
			await notebookService.scheduleNotebookJobAsync(
				scheduleNotebookJobRequest,
			);
			props.setShow(false);
			setInputParameters([]);
			setInputParametersToGenerate([]);
			setOutputParameters([]);
			toast.success('Job defined successfully');
		} catch (e) {
			toast.error('Job creation failed');
		}
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
						<TextField
							label='Job Name'
							fullWidth
							onChange={(e) => setJobName(e.target.value)}></TextField>
					</Grid>
					<Grid item xs={1}></Grid>
					<Grid item xs={1}></Grid>
					<Grid item xs={9}>
						<TextField
							label='Trigger Interval'
							fullWidth
							type='number'
							onChange={(e) =>
								setTriggerInterval(Number(e.target.value))
							}></TextField>
					</Grid>
					<Grid item xs={1}>
						<FormControlLabel
							control={
								<Checkbox
									defaultChecked
									onChange={(e) => setTriggerNow(e.target.checked)}
								/>
							}
							label='Trigger now'
						/>
					</Grid>
					<Grid item xs={1}></Grid>
					<Grid item xs={1}></Grid>
					<Grid item xs={10}>
						<Typography id='modal-modal-title' variant='h6' component='h2'>
							Notebook
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
						<Button variant='contained' onClick={handleDefineJob}>
							Define a new job
						</Button>
					</Grid>
				</Grid>
			</Box>
		</Modal>
	);
}

export default DefineANewNotebookJobModal;
