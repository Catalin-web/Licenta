/** @format */
import { Grid, IconButton, Typography } from '@mui/material';
import {
	DataGrid,
	GridRowId,
	GridRowSelectionModel,
} from '@mui/x-data-grid';
import { OutputParameterName } from '../../../services/NotebookService/NotebookServiceModels';
import { Dispatch, SetStateAction, useState } from 'react';
import AddIcon from '@mui/icons-material/Add';
import { toast } from 'react-toastify';
import DeleteIcon from '@mui/icons-material/Delete';

function OutputParameterNamesEditableGrid(props: {
	outputParameterNames: OutputParameterName[];
	setOutputParameterNames: Dispatch<
		SetStateAction<OutputParameterName[]>
	>;
	gridName: string;
}) {
	const [selectedRows, setSelectedRows] = useState<string[]>([]);
	const handleAdd = () => {
		let trials = 0;
		let name = 'defaultName';
		while (
			// eslint-disable-next-line no-loop-func
			props.outputParameterNames.some((item) => item.name === name)
		) {
			name = `defaultName${trials}`;
			trials++;
		}
		props.setOutputParameterNames([
			...props.outputParameterNames,
			{ name: name },
		]);
	};
	const handleRowEditStop = (params: any, event: any) => {
		event.defaultMuiPrevented = true;
	};
	const handleDelete = () => {
		let updatedList = [...props.outputParameterNames];
		updatedList = updatedList.filter(
			(item) => !selectedRows.includes(item.name),
		);
		props.setOutputParameterNames(updatedList);
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
					rows={props.outputParameterNames}
					columns={[
						{ field: 'name', headerName: 'Name', flex: 1, editable: true },
					]}
					getRowId={(row: OutputParameterName) => row.name}
					checkboxSelection
					disableRowSelectionOnClick
					processRowUpdate={(
						updatedRow: OutputParameterName,
						originalRow: OutputParameterName,
					) => {
						let updatedList = [...props.outputParameterNames];
						const index = updatedList.findIndex(
							(item) => item.name === originalRow.name,
						);
						if (
							updatedRow.name !== originalRow.name &&
							updatedList.some((item) => item.name === updatedRow.name)
						) {
							toast.error('Parameter name already exists');
							return originalRow;
						}
						updatedList[index] = updatedRow;
						props.setOutputParameterNames(updatedList);
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
export default OutputParameterNamesEditableGrid;
