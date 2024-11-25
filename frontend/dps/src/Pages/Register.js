import { useForm } from '@mantine/form';
import { notifications } from '@mantine/notifications';
import { Group, Box, PasswordInput, TextInput, Button } from '@mantine/core';
import { useNavigate } from 'react-router-dom';

import axios from "../http/axios";
import { routes } from "../http/routes"

const Register = () => {
    const navigate = useNavigate();

    const form = useForm({
        mode: 'uncontrolled',
        validateInputOnChange: true,
        initialValues: {
            email: '',
            password: '',
            confirmPassword: ''
        },

        validate: {
            email: (value) => (/^\S+@\S+$/.test(value) ? null : 'Invalid email'),
            confirmPassword: (value, values) =>
                value !== values.password ? 'Passwords did not match' : null,
        },
    });

    const handleSubmit = async (values) => {
        var response = await axios.post(routes.auth.register, {
            "email":values.email,
            "password":values.password
        }, {})

        if (!response.data.success)
        {
            if (response.data.error.errorCode === "duplicate_email") {
                notifications.show({
                    color: "red",
                    title: 'Error while registering user',
                    message: 'This email is already registered',
                })
            }
            else {
                notifications.show({
                    color: "red",
                    title: 'Error while registering user',
                    message: 'Registration failed' ,
                })
            }
        }

        navigate("/", { replace: true })
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

                <PasswordInput
                    mt="sm"
                    label="Confirm password"
                    placeholder="Confirm password"
                    key={form.key('confirmPassword')}
                    {...form.getInputProps('confirmPassword')}
                />

                <Group justify="flex-end" mt="md">
                    <Button type="submit">Submit</Button>
                </Group>
            </form>
        </Box>
    );
};

export default Register;