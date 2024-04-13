/** @format */

import { IconButton, Typography } from '@mui/material';
import { NotebookParameterToGenerate } from '../../../services/NotebookService/NotebookServiceModels';
import CreateIcon from '@mui/icons-material/Create';
import React, {
	Dispatch,
	SetStateAction,
	useEffect,
	useState,
} from 'react';
import EditDescriptionModal from './EditDescriptionModal';

function EditDescriptionColumn(props: {
	notebookParameterToGenerate: NotebookParameterToGenerate | undefined;
	setNotebookParametersToGenerate: Dispatch<
		SetStateAction<NotebookParameterToGenerate[]>
	>;
}) {
	const [show, setShow] = useState(false);
	const [description, setDescription] = useState<string | undefined>(
		props.notebookParameterToGenerate?.descriptionOfTheParameter,
	);
	const handleEdit = () => {
		setShow(true);
	};
	useEffect(() => {
		if (props.notebookParameterToGenerate !== undefined) {
			props.setNotebookParametersToGenerate(
				(prev: NotebookParameterToGenerate[]) => {
					const index = prev.findIndex(
						(item) =>
							item.nameOfTheParameter ===
							props.notebookParameterToGenerate?.nameOfTheParameter,
					);
					if (index === -1) {
						return prev;
					}
					prev[index].descriptionOfTheParameter = description ?? '';
					const updated = [...prev];
					return updated;
				},
			);
		}
	}, [description, props]);
	return (
		<>
			<EditDescriptionModal
				show={show}
				setShow={setShow}
				description={description}
				setDescription={setDescription}
			/>
			<Typography>
				<IconButton onClick={handleEdit}>
					<CreateIcon />
				</IconButton>
				{props.notebookParameterToGenerate?.descriptionOfTheParameter}
			</Typography>
		</>
	);
}

export default React.memo(EditDescriptionColumn);
