/** @format */
import { Tooltip } from '@mui/material';
import { Progress } from '../../../services/NotebookService/NotebookServiceModels';
import DoneIcon from '@mui/icons-material/Done';
import CircularProgress from '@mui/material/CircularProgress';
import TimerIcon from '@mui/icons-material/Timer';
import AccessAlarmIcon from '@mui/icons-material/AccessAlarm';

function ProgressToolTip(props: { progress: Progress }) {
	return (
		<>
			{props.progress === Progress.CREATED && (
				<Tooltip title='Created'>
					<AccessAlarmIcon color={'info'} />
				</Tooltip>
			)}
			{props.progress === Progress.IN_PROGRESS && (
				<Tooltip title='In progress'>
					<CircularProgress color='info' />
				</Tooltip>
			)}
			{props.progress === Progress.COMPLETED && (
				<Tooltip title='Completed'>
					<DoneIcon color='success' />
				</Tooltip>
			)}
			{props.progress === Progress.QUEUED && (
				<Tooltip title='Queued' color='info'>
					<TimerIcon />
				</Tooltip>
			)}
		</>
	);
}
export default ProgressToolTip;
