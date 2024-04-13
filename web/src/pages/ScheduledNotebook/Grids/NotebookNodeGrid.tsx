/** @format */
import { Grid, IconButton, Tooltip, Typography } from '@mui/material';
import {
	DataGrid,
	GridRowId,
	GridRowSelectionModel,
} from '@mui/x-data-grid';
import {
	NotebookNode,
	ScheduleNotebookNodeRequest,
} from '../../../services/NotebookService/NotebookServiceModels';
import { Dispatch, SetStateAction, useState } from 'react';
import { GraphService } from '../../../services/NotebookService/GraphService';
import { toast } from 'react-toastify';
import ScheduleIcon from '@mui/icons-material/Schedule';
import DeleteIcon from '@mui/icons-material/Delete';

function NotebookNodeGrid(props: {
	setStartingNotebookNodes: Dispatch<SetStateAction<NotebookNode[]>>;
	startingNotebookNodes: NotebookNode[] | undefined;
	gridName: string;
}) {
	const [selectedIds, setSelectedIds] = useState<string[]>([]);
	const handleScheduleNotebook = async () => {
		const graphService = new GraphService();
		selectedIds.forEach(async (id) => {
			const scheduleNotebookNodeRequest: ScheduleNotebookNodeRequest = {
				notebookNodeId: id,
			};
			await graphService.scheduleNoteookGraph(
				scheduleNotebookNodeRequest,
			);
			toast.success(
				`Notebook node with id ${id} scheduled successfully!`,
			);
		});
	};
	const handleDelete = async () => {
		const graphService = new GraphService();
		selectedIds.forEach(async (id) => {
			await graphService.deleteNotebookNodeById(id);
			toast.success(`Notebook node with id ${id} deleted successfully!`);
		});
		setSelectedIds([]);
		props.setStartingNotebookNodes((prev) => {
			return prev?.filter((node) => !selectedIds.includes(node.id));
		});
	};
	return (
		<>
			<Grid item xs={1}></Grid>
			<Grid item xs={11}>
				<Typography>
					{props.gridName}
					{selectedIds.length > 0 && (
						<>
							<IconButton onClick={handleScheduleNotebook}>
								<Tooltip title='Schedule'>
									<ScheduleIcon />
								</Tooltip>
							</IconButton>
							<IconButton onClick={handleDelete}>
								<Tooltip title='Delete'>
									<DeleteIcon />
								</Tooltip>
							</IconButton>
						</>
					)}
				</Typography>
			</Grid>
			<Grid item xs={1}></Grid>
			<Grid item xs={10}>
				<DataGrid
					localeText={{ noRowsLabel: '' }}
					initialState={{
						pagination: {
							paginationModel: {
								pageSize: 10,
							},
						},
					}}
					pageSizeOptions={[10]}
					rows={props.startingNotebookNodes}
					columns={[
						{ field: 'id', headerName: 'Id', flex: 1 },
						{
							field: 'notebookName',
							headerName: 'Starting notebook name',
							flex: 1,
						},
					]}
					getRowId={(row: NotebookNode) => row.id}
					checkboxSelection
					disableRowSelectionOnClick
					onRowSelectionModelChange={(
						newSelection: GridRowSelectionModel,
					) => {
						let ids: string[] = [];
						newSelection.forEach((selectedRow: GridRowId) => {
							ids.push(selectedRow.toString());
						});
						setSelectedIds(ids);
					}}
				/>
			</Grid>
			<Grid item xs={1}></Grid>
		</>
	);
}
export default NotebookNodeGrid;
