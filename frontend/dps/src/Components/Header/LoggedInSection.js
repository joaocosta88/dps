import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Container from 'react-bootstrap/Container';

function LoggedInSection() {
    return (
            <Navbar.Collapse className="justify-content-end">
                <NavDropdown title="Mark Otto" id="basic-nav-dropdown">
                    <NavDropdown.Item href="#action/3.1">Log out</NavDropdown.Item>
                </NavDropdown>
            </Navbar.Collapse>
    )
}

export default LoggedInSection;