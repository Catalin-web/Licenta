/** @format */
import { Box, Grid, Modal, TextField } from '@mui/material';
const style = {
	position: 'absolute',
	top: '10%',
	left: '10%',
	width: '80%',
	bgcolor: 'background.paper',
	border: '2px solid #000',
	overflow: 'scroll',
	height: '80%',
	display: 'block',
	boxShadow: 24,
	p: 4,
};
function EditDescriptionModal(props: {
	show: boolean;
	setShow: (show: boolean) => void;
	description: string | undefined;
	setDescription: (description: string) => void;
}) {
	const handleClose = () => props.setShow(false);

	return (
		<Modal
			open={props.show}
			onClose={handleClose}
			aria-labelledby='modal-modal-title'
			aria-describedby='modal-modal-description'>
			<Box sx={style}>
				<Grid container spacing={2}>
					<Grid item xs={12}>
						<TextField
							fullWidth
							multiline
							id='outlined-required'
							label='Description'
							onChange={(e) => props.setDescription(e.target.value)}
							value={props.description}
						/>
					</Grid>
				</Grid>
			</Box>
		</Modal>
	);
}

export default EditDescriptionModal;
