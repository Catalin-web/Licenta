/** @format */

import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import {
	Avatar,
	Box,
	Container,
	Divider,
	Link,
	Menu,
	MenuItem,
	Tooltip,
} from '@mui/material';
import { useState } from 'react';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';
/** @format */
function MyNavbar() {
	const [isOpenBigMenu, setIsOpenBigMenu] = useState<boolean>(false);
	const [isOpenNotebookMenu, setIsOpenNotebookMenu] =
		useState<boolean>(false);
	const [anchorElBigMenu, setAnchorElBigMenu] = useState<Element>();
	const [anchorElNotebookMenu, setAnchorElNotebookMenu] =
		useState<Element>();

	const handleOpenBigMenu = (event: any) => {
		setIsOpenBigMenu(true);
		setAnchorElBigMenu(event.currentTarget);
	};
	const handleOpenNotebookMenu = (event: any) => {
		setIsOpenNotebookMenu(true);
		setAnchorElNotebookMenu(event.currentTarget);
	};

	const handleCloseBigMenu = () => {
		setIsOpenBigMenu(false);
		setAnchorElBigMenu(undefined);
	};
	const handleCloseNotebookMenu = () => {
		setIsOpenNotebookMenu(false);
		setAnchorElNotebookMenu(undefined);
	};

	return (
		<AppBar position='static'>
			<Container maxWidth={false}>
				<Toolbar>
					<Typography
						variant='h6'
						noWrap
						component={Link}
						href='/'
						sx={{
							mr: 2,
							color: 'inherit',
							textDecoration: 'none',
						}}>
						Large workflows models
					</Typography>
					<IconButton
						onClick={handleOpenBigMenu}
						size='large'
						edge='start'
						color='inherit'
						aria-label='menu'
						sx={{ mr: 2 }}>
						<MenuIcon />
					</IconButton>
					<Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
						<Button
							key='Home'
							onClick={handleCloseBigMenu}
							href='/'
							sx={{ my: 2, color: 'white', display: 'block' }}>
							Home
						</Button>
						<Button
							key='Models'
							onClick={handleCloseBigMenu}
							href='/models'
							sx={{ my: 2, color: 'white', display: 'block' }}>
							Models
						</Button>
						<Button
							key='Jobs'
							onClick={handleCloseBigMenu}
							href='/jobs'
							sx={{ my: 2, color: 'white', display: 'block' }}>
							Jobs
						</Button>
						<Button
							key='Notebooks'
							onClick={(e) => {
								handleOpenNotebookMenu(e);
								handleOpenNotebookMenu(e);
							}}
							sx={{ my: 2, color: 'white', display: 'block' }}>
							Notebooks
							<ArrowDropDownIcon />
						</Button>
					</Box>
					<Box sx={{ flexGrow: 0 }}>
						<Menu
							id='basic-navbar-nav'
							anchorEl={anchorElBigMenu}
							open={isOpenBigMenu}
							onClose={handleCloseBigMenu}
							MenuListProps={{
								'aria-labelledby': 'basic-navbar-nav',
							}}>
							<MenuItem onClick={handleCloseBigMenu}>Home</MenuItem>
							<Divider />
							<MenuItem
								component={Link}
								href='/models'
								onClick={handleCloseBigMenu}>
								Open source models
							</MenuItem>
							<Divider />
							<MenuItem
								component={Link}
								href='/jobs'
								onClick={handleCloseBigMenu}>
								Jobs
							</MenuItem>
							<Divider />
							<MenuItem component={Link} onClick={handleCloseBigMenu}>
								Create notebook
							</MenuItem>
							<MenuItem
								href='/scheduledNotebooks'
								component={Link}
								onClick={handleCloseBigMenu}>
								Scheduled notebooks
							</MenuItem>
							<MenuItem
								component={Link}
								href='/scheduledNotebooks'
								onClick={handleCloseBigMenu}>
								Scheduled notebooks
							</MenuItem>
							<MenuItem component={Link} onClick={handleCloseBigMenu}>
								Scheduled notebooks history
							</MenuItem>
							<MenuItem component={Link} onClick={handleCloseBigMenu}>
								Define notebook graph
							</MenuItem>
							<MenuItem component={Link} onClick={handleCloseBigMenu}>
								Trigger notebook graph
							</MenuItem>
						</Menu>
					</Box>
					<Box sx={{ flexGrow: 0 }}>
						<Menu
							id='notebook-menu'
							anchorEl={anchorElNotebookMenu}
							open={isOpenNotebookMenu}
							onClose={handleCloseNotebookMenu}>
							<MenuItem component={Link} onClick={handleCloseNotebookMenu}>
								Create notebook
							</MenuItem>
							<MenuItem
								href='/scheduledNotebooks'
								component={Link}
								onClick={handleCloseNotebookMenu}>
								Scheduled notebooks
							</MenuItem>
							<MenuItem component={Link} onClick={handleCloseNotebookMenu}>
								Scheduled notebooks history
							</MenuItem>
							<MenuItem component={Link} onClick={handleCloseNotebookMenu}>
								Define notebook graph
							</MenuItem>
							<MenuItem component={Link} onClick={handleCloseNotebookMenu}>
								Trigger notebook graph
							</MenuItem>
						</Menu>
						<Tooltip title='Open settings'>
							<IconButton onClick={handleOpenNotebookMenu} sx={{ p: 0 }}>
								<Avatar alt='Remy Sharp' src='/static/images/avatar/2.jpg' />
							</IconButton>
						</Tooltip>
					</Box>
					<Box sx={{ flexGrow: 0, display: { xs: 'none', md: 'flex' } }}>
						<Button color='inherit'>Login</Button>
						<Button color='inherit'>Sign up</Button>
					</Box>
				</Toolbar>
			</Container>
		</AppBar>
	);
}

export default MyNavbar;
