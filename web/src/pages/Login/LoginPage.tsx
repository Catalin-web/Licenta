/** @format */

import { Button, Grid, TextField } from '@mui/material';
import { useState } from 'react';
import { UserService } from '../../services/UserService/UserService';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

function LoginPage() {
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');
	const navigate = useNavigate();

	const handleLogin = async () => {
		try {
			let userService = new UserService();
			let loginRequest = {
				email: email,
				password: password,
			};
			await userService.loginAsync(loginRequest);
			let user = userService.getLoggedUser();
			if (!user) {
				throw new Error();
			} else {
				toast.success('Login succeded');
				navigate('/models');
			}
		} catch (error: any) {
			toast.error('Login failed');
		}
	};

	return (
		<>
			<Grid container marginTop={5}>
				<Grid item xs={4} />
				<Grid item xs={4}>
					<Grid container>
						<Grid item xs={12}>
							<TextField
								fullWidth
								margin='normal'
								label='Email'
								onChange={(e) => {
									setEmail(e.target.value);
								}}
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField
								fullWidth
								margin='normal'
								label='Password'
								type='password'
								onChange={(e) => {
									setPassword(e.target.value);
								}}
							/>
						</Grid>
						<Grid item xs={12} marginTop={2}>
							<Button
								variant='contained'
								color='primary'
								fullWidth
								onClick={handleLogin}>
								Login
							</Button>
						</Grid>
					</Grid>
				</Grid>
				<Grid item xs={4} />
			</Grid>
		</>
	);
}

export default LoginPage;
