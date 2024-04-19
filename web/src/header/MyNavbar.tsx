/** @format */

import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import {
	Box,
	Container,
	Divider,
	Link,
	Menu,
	MenuItem,
} from '@mui/material';
import { useState } from 'react';
/** @format */
function MyNavbar() {
	const [isOpenBigMenu, setIsOpenBigMenu] = useState<boolean>(false);
	const [anchorElBigMenu, setAnchorElBigMenu] = useState<Element>();

	const handleOpenBigMenu = (event: any) => {
		setIsOpenBigMenu(true);
		setAnchorElBigMenu(event.currentTarget);
	};

	const handleCloseBigMenu = () => {
		setIsOpenBigMenu(false);
		setAnchorElBigMenu(undefined);
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
							onClick={handleCloseBigMenu}
							href='/scheduledNotebooks'
							sx={{ my: 2, color: 'white', display: 'block' }}>
							Notebooks
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
							<MenuItem
								href='/scheduledNotebooks'
								component={Link}
								onClick={handleCloseBigMenu}>
								Notebooks
							</MenuItem>
						</Menu>
					</Box>
					<Box sx={{ flexGrow: 0, display: { xs: 'none', md: 'flex' } }}>
						<Button color='inherit' href='/login'>
							Login
						</Button>
						<Button color='inherit' href='/register'>
							Sign up
						</Button>
					</Box>
				</Toolbar>
			</Container>
		</AppBar>
	);
}

export default MyNavbar;
