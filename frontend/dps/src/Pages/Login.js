import { useNavigate, useLocation } from 'react-router-dom';

import useAuth from '../Hooks/useAuth';
import { loginUserAsync } from '../Services/BackendHttpService';

import { Group, Box, PasswordInput, TextInput, Button } from '@mantine/core';
import { useForm } from '@mantine/form';
import { notifications } from '@mantine/notifications';


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
        },

        // functions will be used to validate values at corresponding key
        validate: {
            email: (value) => (/^\S+@\S+$/.test(value) ? null : 'Invalid email')
        },
    });

    const handleSubmit = async (values) => {
        try {
            var response = await loginUserAsync(values.email, values.password)
            if (response.data.success)
                alert("hereeee")
                setAuth({
                    user: values.email,
                    accessToken: response.data.data.accessToken, 
                    refreshToken: response.data.data.refreshToken, 
                    roles: response.data.roles
                })
                
            if (!response.data.success) {
                notifications.show({
                    color: "red",
                    title: 'Error while registering user',
                    message: 'Login failed',
                })
            }

            navigate(from, { replace: true })
        }
        catch (err) {
            alert(err)
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

                <Group justify="flex-end" mt="md">
                    <Button type="submit">Submit</Button>
                </Group>
            </form>
        </Box>
    );
};

export default Login;