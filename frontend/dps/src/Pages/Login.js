import { useContext, useState } from "react";
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import AuthContext from '../Providers/AuthContext';
import { notifications } from '@mantine/notifications';

const Login = () => {

  const auth = useContext(AuthContext)

  const [input, setInput] = useState({
    email: "",
    password: "",
  });

  const handleSubmitEvent = (e) => {
    e.preventDefault();
    if (input.email !== "" && input.password !== "") {
      auth.login(input);
      return;
    }
  };
  

  const handleInput = (e) => {
    notifications.show({
      title: 'Default notification',
      message: 'Do not forget to star Mantine on GitHub! 🌟',
    })
    const { name, value } = e.target;
    if (name === "email")
      validateEmail(e.target.value)
    
    setInput((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const validateEmail = (mail) => {
    if (/^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(mail))
      {
        return (true)
      }
        return (false)
  };

  return (
    <Container fluid>
      <Row className="justify-content-md-center" >
        <Col sm={4}>
          <Form onSubmit={handleSubmitEvent}>
            <Form.Group as={Row} className="m-3">
              <Form.Label column htmlFor="user-email">Email:</Form.Label>
              <Col >
                <Form.Control
                  type="email"
                  id="user-email"
                  name="email"
                  placeholder="example@yahoo.com"
                  aria-describedby="user-email"
                  aria-invalid="false"
                  onChange={handleInput}
                           required />
              </Col>
            </Form.Group>

            <Form.Group as={Row} className="m-3">
              <Form.Label column htmlFor="password">Password:</Form.Label>
              <Col >
                <Form.Control
                  type="password"
                  id="password"
                  name="password"
                  aria-describedby="user-password"
                  aria-invalid="false"
                  onChange={handleInput}
                  required
                />
              </Col>
            </Form.Group>
            <Button className="btn-submit" type="Submit" >Submit</Button>
          </Form>
        </Col>
      </Row>
    </Container>
  );
};

export default Login;