/** @format */
import { Grid } from '@mui/material';
import {
	EdgeUi,
	NodeUi,
	NotebookGraph,
	NotebookNode,
} from '../../../services/NotebookService/NotebookServiceModels';
import ReactFlow from 'reactflow';
import { useEffect, useState } from 'react';

function NotebookUiGraph(props: {
	notebookNode: NotebookNode | undefined;
	notebookGraph: NotebookGraph | undefined;
}) {
	const [nodes, setNodes] = useState<NodeUi[]>([]);
	const [edges, setEdges] = useState<EdgeUi[]>([]);
	useEffect(() => {
		if (props.notebookGraph !== undefined) {
			let nodes: NodeUi[] = [];
			let edges: EdgeUi[] = [];
			const addGraphToNodes = (
				notebookGraph: NotebookGraph,
				y_position = 0,
				x_position = 0,
			) => {
				let node: NodeUi = {
					id: notebookGraph.notebookNode.id,
					position: { x: x_position, y: y_position },
					data: { label: notebookGraph.notebookNode.notebookName },
					style: {
						background:
							notebookGraph.notebookNode.id === props.notebookNode?.id
								? 'white'
								: 'gray',
					},
				};
				nodes.push(node);
				let child_x_position = x_position;
				notebookGraph.childGraphs.forEach((childGraph) => {
					let edge = {
						id: `e${notebookGraph.notebookNode.id}-${childGraph.notebookNode.id}`,
						source: notebookGraph.notebookNode.id,
						target: childGraph.notebookNode.id,
						animated: childGraph.notebookNode.id === props.notebookNode?.id,
					};
					edges.push(edge);
					addGraphToNodes(childGraph, y_position + 100, child_x_position);
					child_x_position += 200;
				});
			};
			addGraphToNodes(props.notebookGraph);
			setNodes(nodes);
			setEdges(edges);
		}
	}, [props.notebookGraph, props.notebookNode]);
	return (
		<>
			<Grid item xs={1}></Grid>
			<Grid item xs={10}>
				<div style={{ width: '50vw', height: '50vh' }}>
					<ReactFlow nodes={nodes} edges={edges} />
				</div>
			</Grid>
			<Grid item xs={1}></Grid>
		</>
	);
}
export default NotebookUiGraph;
