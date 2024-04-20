/** @format */
import { Grid, Typography } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import { TriggerNotebookJobModel } from '../../../services/NotebookService/NotebookServiceModels';

function NotebookJobsGrid(props: {
	notebookTriggerJobs: TriggerNotebookJobModel[] | undefined;
	gridName: string;
}) {
	return (
		<>
			<Grid item xs={12}>
				<Typography>{props.gridName}</Typography>
			</Grid>
			<Grid item xs={12}>
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
					rows={props.notebookTriggerJobs}
					columns={[
						{ field: 'jobName', headerName: 'Name', flex: 1 },
						{ field: 'notebookName', headerName: 'Notebook name', flex: 1 },
						{
							field: 'triggerJobInterval',
							headerName: 'Trigger job interval',
							flex: 1,
							renderCell: (params) => {
								let value = params.value as number;
								let seconds = value % 60;
								let minutes = Math.floor(value / 60) % 60;
								let hours = Math.floor(value / 3600);
								return `${hours}h ${minutes}m ${seconds}s`;
							},
						},
					]}
					getRowId={(row: TriggerNotebookJobModel) => row.id}
					checkboxSelection
					disableRowSelectionOnClick
				/>
			</Grid>
		</>
	);
}
export default NotebookJobsGrid;
