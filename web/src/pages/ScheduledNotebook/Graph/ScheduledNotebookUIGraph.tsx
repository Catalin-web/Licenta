/** @format */
import { Grid } from '@mui/material';
import {
	EdgeScheduledUi,
	EdgeUi,
	NodeScheduledUi,
	NotebookNode,
	NotebookScheduledGraph,
} from '../../../services/NotebookService/NotebookServiceModels';
import ReactFlow from 'reactflow';
import { useEffect, useState } from 'react';
import ScheduledNotebookNode from './ScheduledNotebookNode';

const scheduledNotebookNodeType = {
	scheduledNotebookNode: ScheduledNotebookNode,
};
function ScheduledNotebookUIGraph(props: {
	notebookNode: NotebookNode | undefined;
	notebookScheduledGraph: NotebookScheduledGraph | undefined;
}) {
	const [nodes, setNodes] = useState<NodeScheduledUi[]>([]);
	const [edges, setEdges] = useState<EdgeUi[]>([]);
	useEffect(() => {
		if (
			props.notebookScheduledGraph !== undefined &&
			props.notebookNode !== undefined
		) {
			let nodes: NodeScheduledUi[] = [];
			let edges: EdgeScheduledUi[] = [];
			const addGraphToNodes = (
				notebookScheduledGraph: NotebookScheduledGraph,
				y_position = 0,
				x_position = 0,
			) => {
				let node: NodeScheduledUi = {
					id: notebookScheduledGraph.scheduleNotebook.id,
					position: { x: x_position, y: y_position },
					data: {
						scheduledNotebook: notebookScheduledGraph.scheduleNotebook,
						notebookNode: props.notebookNode,
					},
					type: 'scheduledNotebookNode',
				};
				nodes.push(node);
				let child_x_position = x_position;
				notebookScheduledGraph.childGraphs.forEach((childGraph) => {
					let edge = {
						id: `e${notebookScheduledGraph.scheduleNotebook.id}-${childGraph.scheduleNotebook.id}`,
						source: notebookScheduledGraph.scheduleNotebook.id,
						target: childGraph.scheduleNotebook.id,
						animated:
							childGraph.scheduleNotebook.notebookNodeId ===
							props.notebookNode?.id,
					};
					edges.push(edge);
					addGraphToNodes(childGraph, y_position + 100, child_x_position);
					child_x_position += 200;
				});
			};
			addGraphToNodes(props.notebookScheduledGraph);
			setNodes(nodes);
			setEdges(edges);
		}
	}, [props.notebookScheduledGraph, props.notebookNode]);
	return (
		<>
			<Grid item xs={1}></Grid>
			<Grid item xs={10}>
				<div style={{ width: '50vw', height: '50vh' }}>
					<ReactFlow
						nodes={nodes}
						edges={edges}
						nodeTypes={scheduledNotebookNodeType}
					/>
				</div>
			</Grid>
			<Grid item xs={1}></Grid>
		</>
	);
}
export default ScheduledNotebookUIGraph;
