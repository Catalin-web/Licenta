/** @format */

import axios from 'axios';
import { Settings } from '../Settings';
import {
	NotebookGraph,
	NotebookGraphStatisticsResponse,
	NotebookNode,
	NotebookScheduledGraph,
	ScheduleNotebookNodeRequest,
	TriggerNotebookGraphJobHistoryModel,
	TriggerNotebookGraphJobModel,
} from './NotebookServiceModels';
import { UserService } from '../UserService/UserService';

export class GraphService {
	private readonly baseUrl: string;
	constructor() {
		this.baseUrl = Settings.NOTEBOOK_SERVICE_URL;
	}

	public async getNotebookNodeById(
		notebookNodeId: string,
	): Promise<NotebookNode> {
		let response = await axios.get<NotebookNode>(
			`${this.baseUrl}/notebookService/notebookGraph/node/id/${notebookNodeId}`,
		);
		return response.data;
	}

	public async getNotebookGraphById(
		startingNodeId: string,
	): Promise<NotebookGraph> {
		let response = await axios.get<NotebookGraph>(
			`${this.baseUrl}/notebookService/notebookGraph/node/graph/${startingNodeId}`,
		);
		return response.data;
	}

	public async getStartingNotebookNodes(): Promise<NotebookNode[]> {
		let response = await axios.get<NotebookNode[]>(
			`${this.baseUrl}/notebookService/notebookGraph/node/graph`,
		);
		return response.data;
	}

	public async getNotebookScheduledGraphById(
		graphUniqueId: string,
	): Promise<NotebookScheduledGraph> {
		let response = await axios.get<NotebookScheduledGraph>(
			`${this.baseUrl}/notebookService/notebookGraph/node/scheduled/${graphUniqueId}`,
		);
		return response.data;
	}

	public async scheduleNoteookGraph(
		scheduleNotebookGraphRequest: ScheduleNotebookNodeRequest,
	): Promise<void> {
		await axios.post(
			`${this.baseUrl}/notebookService/notebookGraph/schedule`,
			scheduleNotebookGraphRequest,
		);
	}

	public async deleteNotebookNodeById(
		startingNodeId: string,
	): Promise<void> {
		await axios.delete(
			`${this.baseUrl}/notebookService/notebookGraph/node/graph/${startingNodeId}`,
		);
	}

	public async getStatisticsAsync(): Promise<NotebookGraphStatisticsResponse> {
		let response = await axios.get<NotebookGraphStatisticsResponse>(
			`${this.baseUrl}/notebookService/notebookGraph/statistics`,
		);
		return response.data;
	}

	public async getNotebookGraphJobsCreatedByAuthenticatedUser(): Promise<
		TriggerNotebookGraphJobModel[]
	> {
		let userClient = new UserService();
		const user = await userClient.getLoggedUser();
		if (!user) {
			throw new Error('User not logged');
		}
		let response = await axios.get<TriggerNotebookGraphJobModel[]>(
			`${this.baseUrl}/notebookService/jobs/notebookGraph/user/${user.id}`,
		);
		return response.data;
	}

	public async getNotebookGraphHistoryJobsByJobId(
		triggerNotebookJobId: string,
	): Promise<TriggerNotebookGraphJobHistoryModel[]> {
		let response = await axios.get<
			TriggerNotebookGraphJobHistoryModel[]
		>(
			`${this.baseUrl}/notebookService/jobs/notebookGraph/history/${triggerNotebookJobId}`,
		);
		return response.data;
	}
}
