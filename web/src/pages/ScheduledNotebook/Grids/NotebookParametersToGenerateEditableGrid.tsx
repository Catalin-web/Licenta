/** @format */
import { Grid, IconButton, Tooltip, Typography } from '@mui/material';
import {
	DataGrid,
	GridRowId,
	GridRowSelectionModel,
} from '@mui/x-data-grid';
import {
	ModelType,
	NotebookParameterToGenerate,
} from '../../../services/NotebookService/NotebookServiceModels';
import { Dispatch, SetStateAction, useState } from 'react';
import AddIcon from '@mui/icons-material/Add';
import { toast } from 'react-toastify';
import DeleteIcon from '@mui/icons-material/Delete';
import SourceIcon from '@mui/icons-material/Source';
import EditDescriptionColumn from './EditDescriptionColumn';

function NotebookParameterToGenerateEditableGrid(props: {
	notebookParameters: NotebookParameterToGenerate[];
	setNotebookParameters: Dispatch<
		SetStateAction<NotebookParameterToGenerate[]>
	>;
	gridName: string;
}) {
	const [selectedRows, setSelectedRows] = useState<string[]>([]);
	const handleAdd = () => {
		let trials = 0;
		let nameOfTheParameter = 'defaultName';
		let descriptionOfTheParameter = 'description';
		while (
			props.notebookParameters.some(
				// eslint-disable-next-line no-loop-func
				(item) => item.nameOfTheParameter === nameOfTheParameter,
			)
		) {
			nameOfTheParameter = `defaultName${trials}`;
			descriptionOfTheParameter = `description${trials}`;
			trials++;
		}
		props.setNotebookParameters([
			...props.notebookParameters,
			{
				nameOfTheParameter: nameOfTheParameter,
				descriptionOfTheParameter: descriptionOfTheParameter,
				modelType: ModelType.OPEN_SOURCE,
			},
		]);
	};
	const handleRowEditStop = (params: any, event: any) => {
		event.defaultMuiPrevented = true;
	};
	const handleDelete = () => {
		let updatedList = [...props.notebookParameters];
		updatedList = updatedList.filter(
			(item) => !selectedRows.includes(item.nameOfTheParameter),
		);
		props.setNotebookParameters(updatedList);
	};
	return (
		<>
			<Grid item xs={1}></Grid>
			<Grid item xs={11}>
				<Typography>
					{props.gridName}{' '}
					<IconButton onClick={handleAdd}>
						<AddIcon />
					</IconButton>
					{selectedRows.length > 0 && (
						<IconButton onClick={handleDelete}>
							<DeleteIcon />
						</IconButton>
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
					rows={props.notebookParameters}
					columns={[
						{
							field: 'nameOfTheParameter',
							headerName: 'Name',
							flex: 1,
							editable: true,
						},
						{
							field: 'descriptionOfTheParameter',
							headerName: 'Description',
							flex: 1,
							renderCell: (params: { row: NotebookParameterToGenerate }) => {
								return (
									<EditDescriptionColumn
										notebookParameterToGenerate={params.row}
										setNotebookParametersToGenerate={
											props.setNotebookParameters
										}
									/>
								);
							},
						},
						{
							field: 'modelType',
							headerName: 'Model Type',
							flex: 1,
							sortable: false,
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
						row.nameOfTheParameter
					}
					checkboxSelection
					disableRowSelectionOnClick
					processRowUpdate={(
						updatedRow: NotebookParameterToGenerate,
						originalRow: NotebookParameterToGenerate,
					) => {
						let updatedList = [...props.notebookParameters];
						const index = updatedList.findIndex(
							(item) =>
								item.nameOfTheParameter === originalRow.nameOfTheParameter,
						);
						if (
							updatedRow.nameOfTheParameter !==
								originalRow.nameOfTheParameter &&
							updatedList.some(
								(item) =>
									item.nameOfTheParameter === updatedRow.nameOfTheParameter,
							)
						) {
							toast.error('Parameter name already exists');
							return originalRow;
						}
						updatedList[index] = updatedRow;
						props.setNotebookParameters(updatedList);
						return updatedRow;
					}}
					onRowEditStop={handleRowEditStop}
					onRowSelectionModelChange={(
						newSelection: GridRowSelectionModel,
					) => {
						let ids: string[] = [];
						newSelection.forEach((selectedRow: GridRowId) => {
							ids.push(selectedRow.toString());
						});
						setSelectedRows(ids);
					}}
				/>
			</Grid>
			<Grid item xs={1}></Grid>
		</>
	);
}
export default NotebookParameterToGenerateEditableGrid;
