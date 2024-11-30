import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import NavDropdown from 'react-bootstrap/NavDropdown'
import useAuth from "../../hooks/useAuth";
import useLogout from "../../hooks/useLogout";
import { Link } from "react-router-dom";

function Header() {
  const { auth } = useAuth();
  const logout = useLogout();

  return (
    <Navbar expand="lg" className="bg-body-tertiary">
      <Container fluid>
        <Navbar.Brand><Link to="/">DPS</Link></Navbar.Brand>
        <Navbar.Toggle />
        <Navbar.Collapse>
          <Nav className="me-auto my-2 my-lg-0">
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
                <NavDropdown.Item onClick={async () => await logout()}>Logout</NavDropdown.Item>
              </NavDropdown>
            ) : (
              <>
                <Nav.Item>
                  <Nav.Link variant="outline-primary" className="me-2"><Link to="/register">Register</Link></Nav.Link>
                  {/* <Nav.Link href="/register" variant="outline-primary" className="me-2">Register</Nav.Link> */}
                </Nav.Item>
                <Nav.Item>

                  <Nav.Link variant="primary" className="me-2">
                    <Link to="/login">Login</Link>
                  </Nav.Link>
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