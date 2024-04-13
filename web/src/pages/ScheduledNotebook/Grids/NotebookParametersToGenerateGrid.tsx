/** @format */
import { Grid, Tooltip, Typography } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import {
	ModelType,
	NotebookParameterToGenerate,
} from '../../../services/NotebookService/NotebookServiceModels';
import SourceIcon from '@mui/icons-material/Source';

function NotebookParametersToGenerateGrid(props: {
	notebookParametersToGenerate:
		| NotebookParameterToGenerate[]
		| undefined;
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
					rows={props.notebookParametersToGenerate}
					columns={[
						{ field: 'nameOfTheParameter', headerName: 'Name', flex: 1 },
						{
							field: 'descriptionOfTheParameter',
							headerName: 'Description',
							flex: 1,
						},
						{
							field: 'modelType',
							headerName: 'Model Type',
							flex: 1,
							renderCell: (params) => {
								const value = params.value as ModelType;
								return {
									[ModelType.OPEN_SOURCE]: (
										<Tooltip title='Open source'>
											<SourceIcon />
										</Tooltip>
									),
								}[value];
							},
						},
					]}
					getRowId={(row: NotebookParameterToGenerate) =>
						row.nameOfTheParameter + row.descriptionOfTheParameter
					}
					checkboxSelection
					disableRowSelectionOnClick
				/>
			</Grid>
			<Grid item xs={1}></Grid>
		</>
	);
}
export default NotebookParametersToGenerateGrid;
