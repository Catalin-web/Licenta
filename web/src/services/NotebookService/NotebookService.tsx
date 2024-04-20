/** @format */

import axios from 'axios';
import { Settings } from '../Settings';
import {
	ScheduleNotebookRequest,
	ScheduledNotebook,
	ScheduledNotebookStatisticsResponse,
	TriggerNotebookJobHistoryModel,
	TriggerNotebookJobModel,
} from './NotebookServiceModels';
import { UserService } from '../UserService/UserService';

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

	public async getNotebookJobsCreatedByAuthenticatedUser(): Promise<
		TriggerNotebookJobModel[]
	> {
		let userClient = new UserService();
		const user = await userClient.getLoggedUser();
		if (!user) {
			throw new Error('User not logged');
		}
		let response = await axios.get<TriggerNotebookJobModel[]>(
			`${this.baseUrl}/notebookService/jobs/notebook/user/${user.id}`,
		);
		return response.data;
	}

	public async getNotebookHistoryJobsByJobId(
		triggerNotebookJobId: string,
	): Promise<TriggerNotebookJobHistoryModel[]> {
		let response = await axios.get<TriggerNotebookJobHistoryModel[]>(
			`${this.baseUrl}/notebookService/jobs/notebook/history/${triggerNotebookJobId}`,
		);
		return response.data;
	}
}
