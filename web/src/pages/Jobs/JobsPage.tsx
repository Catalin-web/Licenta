/** @format */

import { useEffect, useState } from 'react';
import {
	Progress,
	PullJob,
	Status,
} from '../../services/GeneratorService/GeneratorServiceModels';
import { GeneratorService } from '../../services/GeneratorService/GeneratorService';
import {
	Button,
	Col,
	Container,
	Form,
	Row,
	Table,
} from 'react-bootstrap';

function JobsPage() {
	let [pullJobs, setPullJobs] = useState<PullJob[]>([]);
	let [refreshRateSeconds, setRefreshRateSeconds] =
		useState<number>(10);

	useEffect(() => {
		const fetchData = async () => {
			let generatorService = new GeneratorService();
			let data = await generatorService.getPullJobsAsync();
			setPullJobs(data);
		};
		fetchData();
	}, []);
	// Refresh rate
	useEffect(() => {
		const interval = setInterval(async () => {
			let generatorService = new GeneratorService();
			let data = await generatorService.getPullJobsAsync();
			setPullJobs(data);
		}, refreshRateSeconds * 1000);
		return () => clearInterval(interval);
	}, [refreshRateSeconds]);
	return (
		<>
			<Container fluid className='mt-4'>
				<Row>
					<Col className='col-1'></Col>
					<Col className='col-2'>
						<Button>Pull new model</Button>
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
									<th>Model name</th>
									<th>Created</th>
									<th>Finished</th>
									<th>Progress</th>
									<th>Status</th>
								</tr>
							</thead>
							<tbody>
								{pullJobs.map((pullJob, index) => (
									<tr key={pullJob.id}>
										<td>{index + 1}</td>
										<td>{pullJob.model}</td>
										<td>{pullJob.createdAt}</td>
										<td>{pullJob.finishedAt}</td>
										<td>{Progress[pullJob.progress]}</td>
										<td>{Status[pullJob.status]}</td>
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

export default JobsPage;
