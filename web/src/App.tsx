/** @format */

import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import MyNavbar from './header/MyNavbar';
import OpenSourceModelsPage from './pages/OpenSourceModels/OpenSourceModelsPage';
import JobsPage from './pages/Jobs/JobsPage';
import ScheduleNotebookPage from './pages/ScheduledNotebook/ScheduleNotebookPage';
function App() {
	return (
		<>
			<MyNavbar></MyNavbar>
			<BrowserRouter>
				<Routes>
					<Route path='/models' element={<OpenSourceModelsPage />} />
					<Route path='/jobs' element={<JobsPage />} />
					<Route
						path='/scheduledNotebooks'
						element={<ScheduleNotebookPage />}
					/>
				</Routes>
			</BrowserRouter>
		</>
	);
}

export default App;
