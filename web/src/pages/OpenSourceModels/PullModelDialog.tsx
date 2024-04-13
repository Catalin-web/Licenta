/** @format */
import { useState } from 'react';
import { GeneratorService } from '../../services/GeneratorService/GeneratorService';
import {
	Box,
	Button,
	Grid,
	Modal,
	TextField,
	Typography,
} from '@mui/material';
import SystemUpdateAltIcon from '@mui/icons-material/SystemUpdateAlt';
const style = {
	position: 'absolute',
	top: '50%',
	left: '50%',
	transform: 'translate(-50%, -50%)',
	width: 400,
	bgcolor: 'background.paper',
	border: '2px solid #000',
	boxShadow: 24,
	p: 4,
};
function PullModelDialog(props: {
	show: boolean;
	setShow: (show: boolean) => void;
}) {
	const [modelName, setModelName] = useState('');
	const handleClose = () => props.setShow(false);
	const handlePull = async () => {
		let generatorService = new GeneratorService();
		await generatorService.pullModelAsync({ name: modelName });
		props.setShow(false);
	};

	return (
		<Modal
			open={props.show}
			onClose={handleClose}
			aria-labelledby='modal-modal-title'
			aria-describedby='modal-modal-description'>
			<Box sx={style}>
				<Grid container spacing={2}>
					<Grid item xs={12}>
						<Typography id='modal-modal-title' variant='h6' component='h2'>
							Pull an open source model
						</Typography>
					</Grid>
					<Grid item xs={8}>
						<TextField
							required
							id='outlined-required'
							label='Model name'
							onChange={(e) => setModelName(e.target.value)}
						/>
					</Grid>
					<Grid
						item
						xs={4}
						sx={{ display: 'flex', justifyContent: 'center' }}>
						<Button variant='contained' onClick={handlePull}>
							<SystemUpdateAltIcon />
						</Button>
					</Grid>
				</Grid>
			</Box>
		</Modal>
	);
}

export default PullModelDialog;
