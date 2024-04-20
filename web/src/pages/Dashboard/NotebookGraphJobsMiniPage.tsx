/** @format */

import { useEffect, useState } from 'react';
import { TriggerNotebookGraphJobModel } from '../../services/NotebookService/NotebookServiceModels';
import NotebookGraphJobsGrid from './Grids/NotebookGraphJobsGrid';
import { GraphService } from '../../services/NotebookService/GraphService';

/** @format */
function NotebookGraphJobsMiniPage() {
	const [notebookGraphTriggerJobs, setNotebookGraphTriggerJobs] =
		useState<TriggerNotebookGraphJobModel[]>([]);
	const refreshTriggerNotebookGraphJobs = async () => {
		const notebookService = new GraphService();
		const triggerNotebookGraphJobs =
			await notebookService.getNotebookGraphJobsCreatedByAuthenticatedUser();
		setNotebookGraphTriggerJobs(triggerNotebookGraphJobs);
	};
	useEffect(() => {
		refreshTriggerNotebookGraphJobs();
	}, []);
	return (
		<>
			<NotebookGraphJobsGrid
				notebookGraphTriggerJobs={notebookGraphTriggerJobs}
				gridName='Schedule graph jobs defined'
			/>
		</>
	);
}

export default NotebookGraphJobsMiniPage;
