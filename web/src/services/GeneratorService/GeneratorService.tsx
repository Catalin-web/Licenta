/** @format */

import axios from 'axios';
import { Settings } from '../Settings';
import {
	OllamaPullModelRequest,
	OpenSourceModel,
	OpenSourceModelResponse,
	PullJob,
} from './GeneratorServiceModels';

/** @format */
export class GeneratorService {
	private readonly baseUrl: string;
	constructor() {
		this.baseUrl = Settings.GENERATOR_SERVICE_URL;
	}

	public async getOpenSourceModelsAsync(): Promise<OpenSourceModel[]> {
		let response = await axios.get<OpenSourceModelResponse>(
			`${this.baseUrl}/generatorService/models`,
		);
		return response.data.models;
	}

	public async pullModelAsync(
		request: OllamaPullModelRequest,
	): Promise<void> {
		await axios.post(
			`${this.baseUrl}/generatorService/jobs/models/pull`,
			request,
		);
	}

	public async getPullJobsAsync(): Promise<PullJob[]> {
		let response = await axios.get<PullJob[]>(
			`${this.baseUrl}/generatorService/jobs`,
		);
		return response.data;
	}
}
