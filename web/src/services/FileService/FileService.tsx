/** @format */

import axios from 'axios';
import { Settings } from '../Settings';
import { NotebookFile } from './FileServiceModels';

export class FileService {
	private readonly baseUrl: string;
	constructor() {
		this.baseUrl = Settings.FILE_SERVICE_URL;
	}

	public async getAllFiles(): Promise<NotebookFile[]> {
		let response = await axios.get<NotebookFile[]>(
			`${this.baseUrl}/fileService/notebook`,
		);
		return response.data;
	}
}
