/** @format */

import axios from 'axios';
import { Settings } from '../Settings';
import { ScheduledNotebook } from './NotebookServiceModels';

export class NotebookService {
	private readonly baseUrl: string;
	constructor() {
		this.baseUrl = Settings.NOTEBOOK_SERVICE_URL;
	}
	public async getScheduledNotebooksAsync(): Promise<
		ScheduledNotebook[]
	> {
		let response = await axios.get<ScheduledNotebook[]>(
			`${this.baseUrl}/notebookService/scheduleNotebook`,
		);
		return response.data;
	}

	public async getScheduledNotebooksHistoryAsync(): Promise<
		ScheduledNotebook[]
	> {
		let response = await axios.get<ScheduledNotebook[]>(
			`${this.baseUrl}/notebookService/scheduleNotebook/history`,
		);
		return response.data;
	}
}
