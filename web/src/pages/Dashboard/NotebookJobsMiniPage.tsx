/** @format */

import { useEffect, useState } from 'react';
import { TriggerNotebookJobModel } from '../../services/NotebookService/NotebookServiceModels';
import { NotebookService } from '../../services/NotebookService/NotebookService';
import NotebookJobsGrid from './Grids/NotebookJobsGrid';

/** @format */
function NotebookJobsMiniPage() {
	const [notebookTriggerJobs, setNotebookTrigerJobs] = useState<
		TriggerNotebookJobModel[]
	>([]);
	const refreshTriggerNotebookJobs = async () => {
		const notebookService = new NotebookService();
		const triggerNotebookJobs =
			await notebookService.getNotebookJobsCreatedByAuthenticatedUser();
		setNotebookTrigerJobs(triggerNotebookJobs);
	};
	useEffect(() => {
		refreshTriggerNotebookJobs();
	}, []);
	return (
		<>
			<NotebookJobsGrid
				notebookTriggerJobs={notebookTriggerJobs}
				gridName='Schedule jobs defined'
			/>
		</>
	);
}

export default NotebookJobsMiniPage;
