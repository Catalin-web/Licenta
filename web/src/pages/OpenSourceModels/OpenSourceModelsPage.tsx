/** @format */

import { useEffect, useState } from 'react';
import { OpenSourceModel } from '../../services/GeneratorService/GeneratorServiceModels';
import { GeneratorService } from '../../services/GeneratorService/GeneratorService';
import {
	Button,
	Col,
	Container,
	Form,
	Row,
	Table,
} from 'react-bootstrap';
import PullModelDialog from './PullModelDialog';

function OpenSourceModelsPage() {
	const [showPullDialog, setShowPullDialog] = useState(false);
	let [models, setModels] = useState<OpenSourceModel[]>([]);
	let [refreshRateSeconds, setRefreshRateSeconds] =
		useState<number>(10);

	useEffect(() => {
		const fetchData = async () => {
			let generatorService = new GeneratorService();
			let data = await generatorService.getOpenSourceModelsAsync();
			setModels(data);
		};
		fetchData();
	}, []);
	// Refresh rate
	useEffect(() => {
		const interval = setInterval(async () => {
			let generatorService = new GeneratorService();
			let data = await generatorService.getOpenSourceModelsAsync();
			setModels(data);
		}, refreshRateSeconds * 1000);
		return () => clearInterval(interval);
	}, [refreshRateSeconds]);
	const handlePull = () => {
		setShowPullDialog(true);
	};
	return (
		<>
			<PullModelDialog
				show={showPullDialog}
				setShow={setShowPullDialog}
			/>
			<Container fluid className='mt-4'>
				<Row>
					<Col className='col-1'></Col>
					<Col className='col-2'>
						<Button onClick={handlePull}>Pull new model</Button>
					</Col>
					<Col className='col-5'></Col>
					<Col className='col-2'>
						<Form>
							<Form.Label>Refresh:</Form.Label>
							<Form.Select
								onChange={(e) =>
									setRefreshRateSeconds(Number(e.target.value))
								}>
								<option value='10'>10</option>
								<option value='5'>5</option>
								<option value='3'>3</option>
							</Form.Select>
						</Form>
					</Col>
					<Col className='col-1'></Col>
				</Row>
				<Row>
					<Col className='col-1'></Col>
					<Col className='col-9'>
						<Table>
							<thead>
								<tr>
									<th>#</th>
									<th>Name</th>
									<th>Model</th>
									<th>Size</th>
									<th>Digest</th>
								</tr>
							</thead>
							<tbody>
								{models.map((model, index) => (
									<tr key={model.name}>
										<td>{index + 1}</td>
										<td>{model.name}</td>
										<td>{model.model}</td>
										<td>{model.size}</td>
										<td>{model.digest}</td>
									</tr>
								))}
							</tbody>
						</Table>
					</Col>
				</Row>
			</Container>
		</>
	);
}

export default OpenSourceModelsPage;
