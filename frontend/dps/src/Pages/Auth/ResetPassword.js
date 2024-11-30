import { useForm } from '@mantine/form';
import { notifications } from '@mantine/notifications';
import { Group, Box, PasswordInput, TextInput, Button } from '@mantine/core';
import { useNavigate, useSearchParams } from 'react-router-dom';

import axios from "../../http/axios";
import { routes } from "../../http/routes"

const ResetPassword = () => {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const token = searchParams.get("token");

    const form = useForm({
        mode: 'uncontrolled',
        validateInputOnChange: true,
        initialValues: {
            email: '',
            password: '',
            confirmPassword: ''
        },

        validate: {
            confirmPassword: (value, values) =>
                value !== values.password ? 'Passwords did not match' : null,
        },
    });

    const handleSubmit = async (values) => {
        var response = await axios.post(routes.auth.resetPassword, {
            "email": values.email,
            "password": values.password,
            token
        }, {})

        if (!response.data.success) {
            notifications.show({
                color: "red",
                title: 'Error while reseting the password',
                message: 'Reset password failed',
            })
        }
        else
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

export default ResetPassword;