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

export interface OutputParameterName {
	name: string;
}

export interface ScheduleNotebookRequest {
	notebookName: string;
	inputParameters: NotebookParameter[];
	inputParametersToGenerate: NotebookParameterToGenerate[];
	outputParametersNames: string[];
}

export interface ScheduleNotebookNodeRequest {
	notebookNodeId: string;
}

export interface ScheduledNotebookStatisticsResponse {
	numberOfCreatedNotebooks: number;
	numberOfQueuedNotebooks: number;
	numberOfInProgressNotebooks: number;
	numberOfCompletedNotebooks: number;
	numberOfFailedNotebooks: number;
	numberOfSuccedeNotebooks: number;
}

export interface NotebookGraphStatisticsResponse {
	numberOfNotebookGraphs: number;
	numberOfInprogressGraphs: number;
	numberOfSuccededGraphs: number;
	numberOfFailedGraphs: number;
	numberOfCreatedNotebooks: number;
	numberOfQueuedNotebooks: number;
	numberOfInProgressNotebooks: number;
	numberOfCompletedNotebooks: number;
	numberOfFailedNotebooks: number;
	numberOfSuccedeNotebooks: number;
}

export interface TriggerNotebookJobModel {
	id: string;
	jobName: string;
	notebookName: string;
	inputParameters: NotebookParameter[];
	inputParametersToGenerate: NotebookParameterToGenerate[];
	outputParametersNames: string[];
	jobId: string;
	triggerId: string;
	triggerJobInterval: number;
	userId: string;
}

export interface TriggerNotebookJobHistoryModel {
	id: string;
	triggerNotebookJobId: string;
	scheduledNotebookId: string;
	triggerTime: string;
}

export interface TriggerNotebookGraphJobModel {
	id: string;
	jobName: string;
	notebookNodeId: string;
	jobId: string;
	triggerId: string;
	triggerJobInterval: number;
	userId: string;
}

export interface TriggerNotebookGraphJobHistoryModel {
	id: string;
	triggerNotebookGraphJobId: string;
	graphUniqueId: string;
	triggerTime: string;
}

export enum DashboardPageSelected {
	ScheduleNotebookJobs,
	ScheduleNotebookHistoryJobs,
	ScheduleGraphJobs,
	ScheduleGraphHistoryJobs,
	PlaygroundPage,
}

export interface ScheduleNotebookJobRequest
	extends ScheduleNotebookRequest {
	jobName: string;
	intervalInSeconds: number;
	triggerNow: boolean;
	userId: string;
}

export interface ScheduleNotebookGraphJobRequest
	extends ScheduleNotebookNodeRequest {
	jobName: string;
	intervalInSeconds: number;
	triggerNow: boolean;
	userId: string;
}
