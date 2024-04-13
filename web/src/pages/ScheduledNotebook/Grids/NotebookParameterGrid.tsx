/** @format */
import { Grid, Typography } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import { NotebookParameter } from '../../../services/NotebookService/NotebookServiceModels';

function NotebookParameterGrid(props: {
	notebookParameters: NotebookParameter[] | undefined;
	gridName: string;
}) {
	return (
		<>
			<Grid item xs={1}></Grid>
			<Grid item xs={11}>
				<Typography>{props.gridName}</Typography>
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
					rows={props.notebookParameters}
					columns={[
						{ field: 'name', headerName: 'Name', flex: 1 },
						{ field: 'value', headerName: 'Value', flex: 1 },
					]}
					getRowId={(row: NotebookParameter) => row.name + row.value}
					checkboxSelection
					disableRowSelectionOnClick
				/>
			</Grid>
			<Grid item xs={1}></Grid>
		</>
	);
}
export default NotebookParameterGrid;
