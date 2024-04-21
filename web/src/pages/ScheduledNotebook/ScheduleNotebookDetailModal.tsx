/** @format */
import { Alert, Box, Grid, Modal, Typography } from '@mui/material';
import { ScheduledNotebook } from '../../services/NotebookService/NotebookServiceModels';
import NotebookParameterGrid from './Grids/NotebookParameterGrid';
import NotebookParametersToGenerateGrid from './Grids/NotebookParametersToGenerateGrid';
import PriorityHighIcon from '@mui/icons-material/PriorityHigh';
import { Status } from '../../services/GeneratorService/GeneratorServiceModels';
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
function ScheduleNotebookDetailModal(props: {
	show: boolean;
	setShow: (show: boolean) => void;
	scheduledNotebook: ScheduledNotebook | undefined;
}) {
	const handleClose = () => props.setShow(false);

	return (
		<Modal
			open={props.show}
			onClose={handleClose}
			aria-labelledby='modal-modal-title'
			aria-describedby='modal-modal-description'>
			<Box sx={style}>
				<Grid container spacing={2}>
					{props.scheduledNotebook?.status === Status.FAILED && (
						<Grid item xs={12}>
							<Alert
								icon={<PriorityHighIcon fontSize='inherit' />}
								severity='error'>
								{props.scheduledNotebook.errorMessage}
							</Alert>
						</Grid>
					)}
					<Grid item xs={1}></Grid>
					<Grid item xs={10}>
						<Typography id='modal-modal-title' variant='h6' component='h2'>
							Scheduled Notebook Details, {props.scheduledNotebook?.id}
						</Typography>
					</Grid>
					<Grid item xs={1}></Grid>
					<NotebookParameterGrid
						notebookParameters={props.scheduledNotebook?.inputParameters}
						gridName='Input Parameters'
					/>
					<NotebookParametersToGenerateGrid
						notebookParametersToGenerate={
							props.scheduledNotebook?.inputParametersToGenerate
						}
						gridName='Parameters to Generate'
					/>
					<NotebookParameterGrid
						notebookParameters={props.scheduledNotebook?.outputParameters}
						gridName='Output Parameters'
					/>
				</Grid>
			</Box>
		</Modal>
	);
}

export default ScheduleNotebookDetailModal;
