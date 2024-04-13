/** @format */
import {
	Alert,
	Box,
	Grid,
	MenuItem,
	Modal,
	Select,
	Typography,
} from '@mui/material';
import { ScheduledNotebook } from '../../services/NotebookService/NotebookServiceModels';
import NotebookParameterGrid from './Grids/NotebookParameterGrid';
import NotebookParametersToGenerateGrid from './Grids/NotebookParametersToGenerateGrid';
import PriorityHighIcon from '@mui/icons-material/PriorityHigh';
import { Status } from '../../services/GeneratorService/GeneratorServiceModels';
import { useEffect, useState } from 'react';
import { NotebookFile } from '../../services/FileService/FileServiceModels';
import { FileService } from '../../services/FileService/FileService';
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
	const [inputParameters, setInputParameters] = useState([]);
	const [outputParameters, setOutputParameters] = useState([]);
	const [inputParametersToGenerate, setInputParametersToGenerate] =
		useState([]);
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
					<NotebookParameterGrid
						notebookParameters={inputParameters}
						gridName='Input Parameters'
					/>
					<NotebookParametersToGenerateGrid
						notebookParametersToGenerate={inputParametersToGenerate}
						gridName='Parameters to Generate'
					/>
					<NotebookParameterGrid
						notebookParameters={outputParameters}
						gridName='Output Parameters'
					/>
				</Grid>
			</Box>
		</Modal>
	);
}

export default ScheduleNotebookModal;
