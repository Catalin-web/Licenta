/** @format */
import { useState } from 'react';
import { Button, Form } from 'react-bootstrap';
import Modal from 'react-bootstrap/Modal';
import { GeneratorService } from '../../services/GeneratorService/GeneratorService';

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
		<Modal show={props.show} onHide={handleClose}>
			<Modal.Header closeButton>
				<Modal.Title>Pull new open source models model</Modal.Title>
			</Modal.Header>
			<Modal.Body>
				All available models:{' '}
				<a href='https://ollama.com/library'>link</a>
				<br></br>
				<Form className='mt-3'>
					<Form.Control
						size='sm'
						type='text'
						placeholder='Model name'
						value={modelName}
						onChange={(e) => setModelName(e.target.value)}
					/>
				</Form>
			</Modal.Body>
			<Modal.Footer>
				<Button variant='secondary' onClick={handleClose}>
					Close
				</Button>
				<Button variant='primary' onClick={handlePull}>
					Pull Model
				</Button>
			</Modal.Footer>
		</Modal>
	);
}

export default PullModelDialog;
