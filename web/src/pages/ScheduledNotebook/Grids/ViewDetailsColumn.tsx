/** @format */

import { IconButton } from '@mui/material';
import { ScheduledNotebook } from '../../../services/NotebookService/NotebookServiceModels';
import RemoveRedEyeIcon from '@mui/icons-material/RemoveRedEye';
import ScatterPlotIcon from '@mui/icons-material/ScatterPlot';
import React from 'react';

function ViewDetailsColumn(props: {
	scheduledNotebook: ScheduledNotebook;
	setCurentlyShowingScheduledNotebook: (
		scheduledNotebook: ScheduledNotebook,
	) => void;
	setShowGraphDetails: (showGraphDetails: boolean) => void;
	setShowNotebookDetails: (showNotebookDetails: boolean) => void;
}) {
	const handleOpenNotebookDetails = () => {
		props.setCurentlyShowingScheduledNotebook(props.scheduledNotebook);
		props.setShowNotebookDetails(true);
	};
	const handleOpenGraphDetails = () => {
		props.setCurentlyShowingScheduledNotebook(props.scheduledNotebook);
		props.setShowGraphDetails(true);
	};

	return (
		<>
			<IconButton onClick={handleOpenNotebookDetails}>
				<RemoveRedEyeIcon />
			</IconButton>
			{props.scheduledNotebook.notebookNodeId && (
				<IconButton onClick={handleOpenGraphDetails}>
					<ScatterPlotIcon />
				</IconButton>
			)}
		</>
	);
}

export default React.memo(ViewDetailsColumn);
