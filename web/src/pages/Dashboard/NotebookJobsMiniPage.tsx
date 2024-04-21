/** @format */

import { useEffect, useState } from 'react';
import { TriggerNotebookJobModel } from '../../services/NotebookService/NotebookServiceModels';
import { NotebookService } from '../../services/NotebookService/NotebookService';
import NotebookJobsGrid from './Grids/NotebookJobsGrid';
import { Button, Grid } from '@mui/material';
import DefineANewNotebookJobModal from './Modals/DefineANewNotebookJobModal';

/** @format */
function NotebookJobsMiniPage() {
	const [notebookTriggerJobs, setNotebookTrigerJobs] = useState<
		TriggerNotebookJobModel[]
	>([]);
	const [showDefineJobModal, setShowDefineJobModal] =
		useState<boolean>(false);
	const refreshTriggerNotebookJobs = async () => {
		const notebookService = new NotebookService();
		const triggerNotebookJobs =
			await notebookService.getNotebookJobsCreatedByAuthenticatedUser();
		setNotebookTrigerJobs(triggerNotebookJobs);
	};
	useEffect(() => {
		refreshTriggerNotebookJobs();
	}, []);
	const handleDefineJob = () => {
		setShowDefineJobModal(true);
	};
	return (
		<>
			<DefineANewNotebookJobModal
				show={showDefineJobModal}
				setShow={setShowDefineJobModal}
			/>
			<Grid xs={2}>
				<Button variant='contained' onClick={handleDefineJob}>
					Define a new job
				</Button>
			</Grid>
			<Grid xs={10} />
			<NotebookJobsGrid
				notebookTriggerJobs={notebookTriggerJobs}
				gridName='Schedule jobs defined'
			/>
		</>
	);
}

export default NotebookJobsMiniPage;
