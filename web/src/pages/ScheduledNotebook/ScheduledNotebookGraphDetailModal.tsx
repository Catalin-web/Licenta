/** @format */
import {
	Alert,
	Box,
	Grid,
	IconButton,
	Modal,
	Typography,
} from '@mui/material';
import {
	NotebookNode,
	NotebookScheduledGraph,
	ScheduledNotebook,
} from '../../services/NotebookService/NotebookServiceModels';
import RefreshIcon from '@mui/icons-material/Refresh';
import PriorityHighIcon from '@mui/icons-material/PriorityHigh';
import { Status } from '../../services/GeneratorService/GeneratorServiceModels';
import { useCallback, useEffect, useState } from 'react';
import { GraphService } from '../../services/NotebookService/GraphService';
import 'reactflow/dist/style.css';
import ScheduledNotebookUIGraph from './Graph/ScheduledNotebookUIGraph';
import React from 'react';
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
function ScheduledNotebookGraphDetailModal(props: {
	show: boolean;
	setShow: (show: boolean) => void;
	scheduledNotebook: ScheduledNotebook | undefined;
}) {
	const [notebookNode, setNotebookNode] = useState<NotebookNode>();
	const [notebookScheduledGraph, setNotebookScheduledGraph] =
		useState<NotebookScheduledGraph>();
	const handleClose = () => props.setShow(false);
	const getGraphNodeAndScheduledGraph = useCallback(async () => {
		if (props.show === true && props.scheduledNotebook !== undefined) {
			try {
				let graphService = new GraphService();
				let notebookNode = await graphService.getNotebookNodeById(
					props.scheduledNotebook.notebookNodeId,
				);
				let scheduledNotebookGraph =
					await graphService.getNotebookScheduledGraphById(
						props.scheduledNotebook.graphUniqueId,
					);
				setNotebookNode(notebookNode);
				setNotebookScheduledGraph(scheduledNotebookGraph);
			} catch (error) {
				console.log(error);
			}
		}
	}, [props.scheduledNotebook, props.show]);
	useEffect(() => {
		getGraphNodeAndScheduledGraph();
		let interval = setInterval(() => {
			getGraphNodeAndScheduledGraph();
		}, 3000);
		return () => clearInterval(interval);
	}, [
		props.scheduledNotebook,
		props.show,
		getGraphNodeAndScheduledGraph,
	]);
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
					<Grid item xs={1}>
						<IconButton onClick={() => getGraphNodeAndScheduledGraph()}>
							<RefreshIcon />
						</IconButton>
					</Grid>
					<Grid item xs={10}>
						<Typography id='modal-modal-title' variant='h6' component='h2'>
							Scheduled Notebook Details, {props.scheduledNotebook?.id}
						</Typography>
					</Grid>
					<Grid item xs={1}></Grid>
					<ScheduledNotebookUIGraph
						notebookNode={notebookNode}
						notebookScheduledGraph={notebookScheduledGraph}
					/>
				</Grid>
			</Box>
		</Modal>
	);
}

export default React.memo(ScheduledNotebookGraphDetailModal);
