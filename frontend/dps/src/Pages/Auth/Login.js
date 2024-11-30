import { useNavigate, useLocation } from 'react-router-dom';
import { Group, Box, PasswordInput, TextInput, Button, Checkbox } from '@mantine/core';
import { useForm } from '@mantine/form';
import { notifications } from '@mantine/notifications';
import axios from "../../http/axios";
import useAuth from '../../hooks/useAuth';
import { routes } from "../../http/routes"
import { Link } from 'react-router-dom';

const Login = () => {
    const { setAuth } = useAuth();

    const navigate = useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || "/ ";

    const form = useForm({
        mode: 'uncontrolled',
        validateInputOnChange: true,
        initialValues: {
            email: '',
            password: '',
            keepLoggedIn: false,
        },

        // functions will be used to validate values at corresponding key
        validate: {
            email: (value) => (/^\S+@\S+$/.test(value) ? null : 'Invalid email')
        },
    });

    const handleSubmit = async (values) => {
        console.log(JSON.stringify(values))
        try {
            const response = await axios.post(routes.auth.login, {
                "email": values.email,
                "password": values.password,
                "keepLoggedIn": values.keepLoggedIn
            }, {});

            setAuth({
                user: values.email,
                accessToken: response.data.accessToken,
                roles: response.data.roles
            })

            navigate(from, { replace: true })
        }
        catch (err) {
            notifications.show({
                color: "red",
                title: 'Error while registering user',
                message: 'Login failed',
            })
        }
    };

    return (
        <Box maw={340} mx="auto">
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <TextInput
                    mt="sm"
                    label="Email"
                    placeholder="Email"
                    key={form.key('email')}
                    {...form.getInputProps('email')}
                />

                <PasswordInput
                    label="Password"
                    placeholder="Password"
                    key={form.key('password')}
                    {...form.getInputProps('password')}
                />

                <Checkbox
                    key={form.key('keepLoggedIn')}
                    label="Keep logged in?"
                    {...form.getInputProps("keepLoggedIn", { type: "checkbox" })}
                />

                <Link to="/forgotPassword">Forgot password</Link>
                
                <Group justify="flex-end" mt="md">
                    <Button type="submit">Submit</Button>
                </Group>
            </form>
        </Box>
    );
};

export default Login;