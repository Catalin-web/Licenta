/** @format */

import Iframe from 'react-iframe';

/** @format */
function PlaygroundMiniPage() {
	return (
		<>
			<Iframe
				url='http://localhost:8080/user/user/lab'
				width='100%'
				height='800px'
				id='myId'
				className='myClassname'
				display='block'
			/>
		</>
	);
}

export default PlaygroundMiniPage;
