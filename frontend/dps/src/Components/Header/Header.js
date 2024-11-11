import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import NavDropdown from 'react-bootstrap/NavDropdown'
import useAuth from "../../hooks/useAuth";

function Header() {
  const { auth } = useAuth();

  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container fluid>
        <Navbar.Brand href="/">DPS</Navbar.Brand>
        <Navbar.Toggle />
        <Navbar.Collapse>
          <Nav className="me-auto my-2 my-lg-0">
            <Nav.Link href="#action1">Home</Nav.Link>
          </Nav>

          {/* Right side: Register/Login or Username with Dropdown */}
          <Nav className="ml-auto">
            {/* <Nav.Item>
                  <Nav.Link href="/register" variant="outline-primary" className="me-2">Register</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                  <Nav.Link href="/login" variant="primary">Login</Nav.Link>
                </Nav.Item> */}
            {auth?.accessToken ? (
              <NavDropdown title='asdasd' id="user-nav-dropdown" align="end">
                <NavDropdown.Item>Logout</NavDropdown.Item>
              </NavDropdown>
            ) : (
              <>
                <Nav.Item>
                  <Nav.Link href="/register" variant="outline-primary" className="me-2">Register</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                  <Nav.Link href="/login" variant="primary">Login</Nav.Link>
                </Nav.Item>
              </>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default Header;