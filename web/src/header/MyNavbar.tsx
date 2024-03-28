/** @format */

import { Container, Nav, NavDropdown, Navbar } from 'react-bootstrap';

/** @format */
function MyNavbar() {
	return (
		<Navbar expand='lg' className='bg-body-tertiary'>
			<Container>
				<Navbar.Brand href='#'>Title</Navbar.Brand>
				<Navbar.Toggle aria-controls='basic-navbar-nav' />
				<Navbar.Collapse id='basic-navbar-nav'>
					<Nav className='me-auto'>
						<Nav.Link href='#'>Home</Nav.Link>
						<Nav.Link href='/models'>Open source models</Nav.Link>
						<Nav.Link href='/jobs'>Jobs</Nav.Link>
						<NavDropdown title='Notebook' id='basic-nav-dropdown'>
							<NavDropdown.Item href='#'>Create notebook</NavDropdown.Item>
							<NavDropdown.Item href='#'>Upload notebook</NavDropdown.Item>
							<NavDropdown.Item href='#'>Schedule notebook</NavDropdown.Item>
							<NavDropdown.Item href='#'>
								Scheduled notebooks
							</NavDropdown.Item>
							<NavDropdown.Item href='#'>
								Scheduled notebooks history
							</NavDropdown.Item>
							<NavDropdown.Divider />
							<NavDropdown.Item href='#'>
								Define notebook graph
							</NavDropdown.Item>
							<NavDropdown.Item href='#'>
								Trigger notebook graph
							</NavDropdown.Item>
						</NavDropdown>
					</Nav>
					<Nav className='ml-auto'>
						<Nav.Link href='#'>Login</Nav.Link>
						<Nav.Link href='#'>Sign up</Nav.Link>
					</Nav>
				</Navbar.Collapse>
			</Container>
		</Navbar>
	);
}

export default MyNavbar;
