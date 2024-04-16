/** @format */
import { Tooltip } from '@mui/material';
import DoneIcon from '@mui/icons-material/Done';
import AccessAlarmIcon from '@mui/icons-material/AccessAlarm';
import PriorityHighIcon from '@mui/icons-material/PriorityHigh';
import { Status } from '../../../services/GeneratorService/GeneratorServiceModels';

function StatusToolTip(props: { status: Status }) {
	return (
		<>
			{props.status === Status.FAILED && (
				<Tooltip title='Failed'>
					<PriorityHighIcon color={'error'} />
				</Tooltip>
			)}
			{props.status === Status.NONE && (
				<Tooltip title='None'>
					<AccessAlarmIcon color={'info'} />
				</Tooltip>
			)}
			{props.status === Status.SUCCEDED && (
				<Tooltip title='Succeded'>
					<DoneIcon color='success' />
				</Tooltip>
			)}
		</>
	);
}
export default StatusToolTip;
