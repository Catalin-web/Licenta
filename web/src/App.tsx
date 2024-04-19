/** @format */

import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import MyNavbar from './header/MyNavbar';
import OpenSourceModelsPage from './pages/OpenSourceModels/OpenSourceModelsPage';
import JobsPage from './pages/Jobs/JobsPage';
import ScheduleNotebookPage from './pages/ScheduledNotebook/ScheduleNotebookPage';
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from 'react-toastify';
import RegisterPage from './pages/Register/RegisterPage';
import LoginPage from './pages/Login/LoginPage';

function App() {
	return (
		<>
			<ToastContainer />
			<MyNavbar></MyNavbar>
			<BrowserRouter>
				<Routes>
					<Route path='/models' element={<OpenSourceModelsPage />} />
					<Route path='/jobs' element={<JobsPage />} />
					<Route
						path='/scheduledNotebooks'
						element={<ScheduleNotebookPage />}
					/>
					<Route path='/register' element={<RegisterPage />} />
					<Route path='/login' element={<LoginPage />} />
				</Routes>
			</BrowserRouter>
		</>
	);
}

export default App;
