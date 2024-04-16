/** @format */

import axios from 'axios';
import { Settings } from '../Settings';
import {
	ScheduleNotebookRequest,
	ScheduledNotebook,
	ScheduledNotebookStatisticsResponse,
} from './NotebookServiceModels';

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

	public async scheduleNotebookAsync(
		scheduleNotebookRequest: ScheduleNotebookRequest,
	): Promise<void> {
		await axios.post(
			`${this.baseUrl}/notebookService/scheduleNotebook/schedule`,
			scheduleNotebookRequest,
		);
	}

	public async getStatisticsAsync(): Promise<ScheduledNotebookStatisticsResponse> {
		let response = await axios.get<ScheduledNotebookStatisticsResponse>(
			`${this.baseUrl}/notebookService/scheduleNotebook/statistics`,
		);
		return response.data;
	}
}
