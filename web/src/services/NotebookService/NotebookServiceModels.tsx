/** @format */

import { Status } from '../GeneratorService/GeneratorServiceModels';

export enum Progress {
	CREATED,
	QUEUED,
	IN_PROGRESS,
	COMPLETED,
}

export interface ScheduledNotebook {
	id: string;
	notebookName: string;
	createdAt: string;
	finishedAt: string;
	progress: Progress;
	status: Status;
	errorMessage: string;
	inputParameters: NotebookParameter[];
	inputParametersToGenerate: NotebookParameterToGenerate[];
	outputParameters: NotebookParameter[];
	outputParametersNames: string[];
	notebookNodeId: string;
	graphUniqueId: string;
}

export interface NotebookParameter {
	name: string;
	value: string;
}

export interface NotebookParameterToGenerate {
	nameOfTheParameter: string;
	descriptionOfTheParameter: string;
	modelType: ModelType;
}

export enum ModelType {
	OPEN_SOURCE,
}

export interface NotebookNode {
	id: string;
	notebookName: string;
	inputParameters: NotebookParameter[];
	inputParametersToGenerate: NotebookParameterToGenerate[];
	outputParametersNames: string[];
	childNodeIds: string[];
	parentNodeId: string;
	startingNodeId: string;
}

export interface NotebookGraph {
	notebookNode: NotebookNode;
	childGraphs: NotebookGraph[];
}

export interface NotebookScheduledGraph {
	scheduleNotebook: ScheduledNotebook;
	childGraphs: NotebookScheduledGraph[];
}

export interface NodeUi {
	id: string;
	position: { x: number; y: number };
	data: { label: string };
	style: { background: string };
}

export interface EdgeUi {
	id: string;
	source: string;
	target: string;
	animated: boolean;
}

export interface NodeScheduledUi {
	id: string;
	position: { x: number; y: number };
	data: NodeScheduledData;
	type: string;
}

export interface EdgeScheduledUi {
	id: string;
	source: string;
	target: string;
	animated: boolean;
}

export interface NodeScheduledData {
	scheduledNotebook: ScheduledNotebook | undefined;
	notebookNode: NotebookNode | undefined;
}
