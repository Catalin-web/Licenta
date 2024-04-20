/** @format */
import { Grid, Typography } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import {
	Progress,
	ScheduledNotebook,
} from '../../../services/NotebookService/NotebookServiceModels';
import { Status } from '../../../services/GeneratorService/GeneratorServiceModels';
import ViewDetailsColumn from './ViewDetailsColumn';
import StatusToolTip from '../Tooltips/StatusToolTip';
import ProgressToolTip from '../Tooltips/ProgressToolTip';

function ScheduledNotebooksGrid(props: {
	scheduledNotebooks: ScheduledNotebook[];
	gridName: string;
	setCurentlyShowingScheduledNotebook: (
		scheduledNotebook: ScheduledNotebook,
	) => void;
	setShowGraphDetails: (showGraphDetails: boolean) => void;
	setShowNotebookDetails: (showNotebookDetails: boolean) => void;
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
						sorting: { sortModel: [{ field: 'createdAt', sort: 'desc' }] },
					}}
					pageSizeOptions={[10]}
					rows={props.scheduledNotebooks}
					columns={[
						{ field: 'id', headerName: 'ID', flex: 1 },
						{
							field: 'notebookName',
							headerName: 'Notebook Name',
							flex: 1,
						},
						{
							field: 'status',
							headerName: 'Status',
							flex: 1,
							renderCell: (params) => {
								const value = params.value as Status;
								return <StatusToolTip status={value} />;
							},
						},
						{
							field: 'progress',
							headerName: 'Progress',
							flex: 1,
							renderCell: (params) => {
								const value = params.value as Progress;
								return <ProgressToolTip progress={value} />;
							},
						},
						{
							field: 'createdAt',
							headerName: 'Created',
							flex: 1,
						},
						{
							field: 'viewdetails',
							headerName: '',
							flex: 1,
							sortable: false,
							renderCell: (params: { row: ScheduledNotebook }) => (
								<ViewDetailsColumn
									scheduledNotebook={params.row}
									setCurentlyShowingScheduledNotebook={
										props.setCurentlyShowingScheduledNotebook
									}
									setShowGraphDetails={props.setShowGraphDetails}
									setShowNotebookDetails={props.setShowNotebookDetails}
								/>
							),
						},
					]}
					getRowId={(row: ScheduledNotebook) => row.id}
					checkboxSelection
					disableRowSelectionOnClick
				/>
			</Grid>
		</>
	);
}

export default ScheduledNotebooksGrid;
