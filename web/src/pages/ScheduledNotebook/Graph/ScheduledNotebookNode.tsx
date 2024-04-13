/** @format */

import { Handle, NodeProps, Position } from 'reactflow';
import {
	NodeScheduledData,
	Progress,
} from '../../../services/NotebookService/NotebookServiceModels';
import ProgressToolTip from '../Tooltips/ProgressToolTip';
import StatusToolTip from '../Tooltips/StatusToolTip';
import { Status } from '../../../services/GeneratorService/GeneratorServiceModels';

function ScheduledNotebookNode(props: NodeProps<NodeScheduledData>) {
	return (
		<>
			<Handle
				type='target'
				position={Position.Top}
				isConnectable={props.isConnectable}
			/>
			{props.data?.scheduledNotebook?.notebookNodeId ===
			props.data?.notebookNode?.id ? (
				<b>
					<span>{props.data?.scheduledNotebook?.notebookName}</span>
				</b>
			) : (
				<span>{props.data?.scheduledNotebook?.notebookName}</span>
			)}
			<ProgressToolTip
				progress={props.data?.scheduledNotebook?.progress as Progress}
			/>
			<StatusToolTip
				status={props.data?.scheduledNotebook?.status as Status}
			/>
			<Handle
				type='source'
				position={Position.Bottom}
				isConnectable={props.isConnectable}
			/>
		</>
	);
}

export default ScheduledNotebookNode;
