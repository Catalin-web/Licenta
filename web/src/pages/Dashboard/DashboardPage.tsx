/** @format */

import {
	Box,
	Collapse,
	Grid,
	List,
	ListItem,
	ListItemButton,
	ListItemIcon,
	ListItemText,
} from '@mui/material';
import { useState } from 'react';
import WorkIcon from '@mui/icons-material/Work';
import ScatterPlotIcon from '@mui/icons-material/ScatterPlot';
import HistoryIcon from '@mui/icons-material/History';
import { ExpandLess, ExpandMore } from '@mui/icons-material';
import { DashboardPageSelected } from '../../services/NotebookService/NotebookServiceModels';
import RemoveRedEyeIcon from '@mui/icons-material/RemoveRedEye';
import DefinedJobsMiniPage from './NotebookJobsHistoryMiniPage';
import NotebookJobsMiniPage from './NotebookJobsMiniPage';
import NotebookGraphJobsMiniPage from './NotebookGraphJobsMiniPage';
import NotebookGraphJobsHistoryMiniPage from './NotebookGraphJobsHistoryMiniPage';
import PlaygroundMiniPage from './PlaygroundMiniPage';
import InventoryIcon from '@mui/icons-material/Inventory';

function DashboardPage() {
	const [currentPageSelected, setCurrentPageSelected] =
		useState<DashboardPageSelected>(
			DashboardPageSelected.ScheduleNotebookJobs,
		);
	const [openGraph, setOpenGraph] = useState(true);
	const [openNotebook, setOpenNotebook] = useState(true);
	const handleClickGraph = () => {
		setOpenGraph(!openGraph);
	};
	const handleClickNotebook = () => {
		setOpenNotebook(!openNotebook);
	};
	const handleClickPlayground = () => {
		setCurrentPageSelected(DashboardPageSelected.PlaygroundPage);
	};

	return (
		<>
			<Grid container>
				<Grid item xs={3}>
					<Box
						sx={{
							width: '100%',
							height: '100%',
							maxWidth: 360,
							minHeight: '100vh',
							bgcolor: '#E5E4E2',
						}}>
						<List>
							<ListItem disablePadding>
								<ListItemButton onClick={handleClickNotebook}>
									<ListItemIcon>
										<WorkIcon />
									</ListItemIcon>
									<ListItemText primary='Simple automation jobs' />
									{openNotebook ? <ExpandLess /> : <ExpandMore />}
								</ListItemButton>
							</ListItem>
							<Collapse in={openNotebook} timeout='auto' unmountOnExit>
								<List>
									<ListItem disablePadding>
										<ListItemButton
											sx={{ pl: 4 }}
											onClick={() =>
												setCurrentPageSelected(
													DashboardPageSelected.ScheduleNotebookJobs,
												)
											}>
											<ListItemIcon>
												<RemoveRedEyeIcon />
											</ListItemIcon>
											<ListItemText primary='Defined jobs' />
										</ListItemButton>
									</ListItem>
									<ListItem disablePadding>
										<ListItemButton
											sx={{ pl: 4 }}
											onClick={() =>
												setCurrentPageSelected(
													DashboardPageSelected.ScheduleNotebookHistoryJobs,
												)
											}>
											<ListItemIcon>
												<HistoryIcon />
											</ListItemIcon>
											<ListItemText primary='Jobs outputs' />
										</ListItemButton>
									</ListItem>
								</List>
							</Collapse>
							<ListItem disablePadding>
								<ListItemButton onClick={handleClickGraph}>
									<ListItemIcon>
										<ScatterPlotIcon />
									</ListItemIcon>
									<ListItemText primary='Complex automation jobs' />
									{openGraph ? <ExpandLess /> : <ExpandMore />}
								</ListItemButton>
							</ListItem>
							<Collapse in={openGraph} timeout='auto' unmountOnExit>
								<List>
									<ListItem disablePadding>
										<ListItemButton
											sx={{ pl: 4 }}
											onClick={() =>
												setCurrentPageSelected(
													DashboardPageSelected.ScheduleGraphJobs,
												)
											}>
											<ListItemIcon>
												<RemoveRedEyeIcon />
											</ListItemIcon>
											<ListItemText
												primary='Defined jobs'
												onClick={() =>
													setCurrentPageSelected(
														DashboardPageSelected.ScheduleGraphHistoryJobs,
													)
												}
											/>
										</ListItemButton>
									</ListItem>
									<ListItem disablePadding>
										<ListItemButton
											sx={{ pl: 4 }}
											onClick={() =>
												setCurrentPageSelected(
													DashboardPageSelected.ScheduleGraphHistoryJobs,
												)
											}>
											<ListItemIcon>
												<HistoryIcon />
											</ListItemIcon>
											<ListItemText primary='Jobs outputs' />
										</ListItemButton>
									</ListItem>
								</List>
							</Collapse>
							<ListItem disablePadding>
								<ListItemButton onClick={handleClickPlayground}>
									<ListItemIcon>
										<InventoryIcon />
									</ListItemIcon>
									<ListItemText primary='Playground' />
								</ListItemButton>
							</ListItem>
						</List>
					</Box>
				</Grid>
				<Grid item xs={8}>
					<Grid container sx={{ pt: 5 }}>
						{currentPageSelected ===
							DashboardPageSelected.ScheduleNotebookJobs && (
							<NotebookJobsMiniPage />
						)}
						{currentPageSelected ===
							DashboardPageSelected.ScheduleNotebookHistoryJobs && (
							<DefinedJobsMiniPage />
						)}
						{currentPageSelected ===
							DashboardPageSelected.ScheduleGraphJobs && (
							<NotebookGraphJobsMiniPage />
						)}
						{currentPageSelected ===
							DashboardPageSelected.ScheduleGraphHistoryJobs && (
							<NotebookGraphJobsHistoryMiniPage />
						)}
						{currentPageSelected ===
							DashboardPageSelected.PlaygroundPage && <PlaygroundMiniPage />}
					</Grid>
				</Grid>
			</Grid>
		</>
	);
}

export default DashboardPage;
