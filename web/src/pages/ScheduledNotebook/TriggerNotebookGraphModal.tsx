/** @format */
import { Box, Grid, Modal } from '@mui/material';
import { NotebookNode } from '../../services/NotebookService/NotebookServiceModels';
import { useEffect, useState } from 'react';
import { GraphService } from '../../services/NotebookService/GraphService';
import NotebookNodeGrid from './Grids/NotebookNodeGrid';
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
function ScheduleNotebookGraphModal(props: {
	show: boolean;
	setShow: (show: boolean) => void;
}) {
	const handleClose = () => props.setShow(false);
	const [startingNotebookNodes, setStartingNotebookNodes] = useState<
		NotebookNode[]
	>([]);

	useEffect(() => {
		const fetchData = async () => {
			const graphService = new GraphService();
			setStartingNotebookNodes(
				await graphService.getStartingNotebookNodes(),
			);
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
					<NotebookNodeGrid
						startingNotebookNodes={startingNotebookNodes}
						setStartingNotebookNodes={setStartingNotebookNodes}
						gridName='Starting notebook nodes'
					/>
				</Grid>
			</Box>
		</Modal>
	);
}

export default ScheduleNotebookGraphModal;
