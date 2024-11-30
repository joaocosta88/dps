
import { useNavigate } from 'react-router-dom';
import { Group, Box, TextInput, Button } from '@mantine/core';
import { useForm } from '@mantine/form';
import { notifications } from '@mantine/notifications';
import axios from "../../http/axios";
import { routes } from "../../http/routes"

const ForgotPassword = () => {
    const navigate = useNavigate();

    const form = useForm({

        mode: 'uncontrolled',
        validateInputOnChange: true,
        initialValues: {
            email: ''
        },

        // functions will be used to validate values at corresponding key
        validate: {
            email: (value) => (/^\S+@\S+$/.test(value) ? null : 'Invalid email')
        },
    });

    const handleSubmit = async (values) => {
        console.log(JSON.stringify(values))
        try {
            await axios.post(routes.auth.forgotPassword, {
                "email": values.email,
            }, {});
        }
        catch (err) {
            
        }
        finally {
            navigate("/", { replace: true })

            notifications.show({
                color: "gree",
                title: 'Please check your email to reset your password',
                message: 'Email sent',
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

                <Group justify="flex-end" mt="md">
                    <Button type="submit">Submit</Button>
                </Group>
            </form>
        </Box>
    );
};

export default ForgotPassword;