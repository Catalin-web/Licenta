/** @format */

import axios from 'axios';
import { Settings } from '../Settings';
import {
	NotebookGraph,
	NotebookNode,
	NotebookScheduledGraph,
	ScheduleNotebookNodeRequest,
} from './NotebookServiceModels';

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
}
