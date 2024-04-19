/** @format */

import { Button, Grid, TextField } from '@mui/material';
import { useState } from 'react';
import { UserService } from '../../services/UserService/UserService';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

function RegisterPage() {
	const [firstName, setFirstName] = useState('');
	const [lastName, setLastName] = useState('');
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');
	const navigate = useNavigate();

	const handleRegister = async () => {
		try {
			let userService = new UserService();
			let requsterRequest = {
				firstName: firstName,
				lastName: lastName,
				email: email,
				password: password,
			};
			await userService.registerAsync(requsterRequest);
			let user = userService.getLoggedUser();
			if (!user) {
				throw new Error();
			} else {
				toast.success('Register succeded');
				navigate('/models');
			}
		} catch (error) {
			toast.error('Register failed');
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
								label='First Name'
								onChange={(e) => {
									setFirstName(e.target.value);
								}}
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField
								fullWidth
								margin='normal'
								label='Last Name'
								onChange={(e) => {
									setLastName(e.target.value);
								}}
							/>
						</Grid>
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
								onClick={handleRegister}>
								Register
							</Button>
						</Grid>
					</Grid>
				</Grid>
				<Grid item xs={4} />
			</Grid>
		</>
	);
}

export default RegisterPage;
