/** @format */
export interface OpenSourceModelResponse {
	models: OpenSourceModel[];
}
export interface OpenSourceModel {
	name: string;
	model: string;
	size: number;
	digest: string;
}

export interface OllamaPullModelRequest {
	name: string;
}

export enum Status {
	NONE,
	SUCCEDED,
	FAILED,
}

export enum Progress {
	QUEUED,
	IN_PROGRESS,
	COMPLETED,
}

export interface PullJob {
	id: string;
	model: string;
	createdAt: string;
	finishedAt: string;
	progress: Progress;
	status: Status;
}
