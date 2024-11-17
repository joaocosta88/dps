import { useForm } from '@mantine/form';
import { Group, Box, Textarea, TextInput, Button } from '@mantine/core';

import useAxiosPrivate from "../hooks/useAxiosPrivate";
import { routes } from '../http/routes';
import { axiosPrivate } from '../http/axios';

const AddListing = () => {
    const axiosPrivate = useAxiosPrivate();

    const form = useForm({
        mode: 'uncontrolled',
        validateInputOnChange: true,
        initialValues: {
            title: '',
            description: '',
            price: 0.0
        },

        validate: {
            
        },
    });


    const handleSubmit = async (values) => {
        var response = await axiosPrivate.post(routes.listings.add, {
            "title": values.title,
            "description": values.description,
            "price": values.price
        }, {})

        if (!response.data.success) {
            console.error(JSON.stringify(response.data))
        }
    };

    return (
        <Box maw={340} mx="auto">
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <TextInput
                    mt="sm"
                    label="Title"
                    placeholder=""
                    key={form.key('title')}
                    {...form.getInputProps('title')}
                />

                <Textarea
                    mt="sm"
                    label="Description"
                    placeholder=""
                    key={form.key('description')}
                    {...form.getInputProps('description')}
                />
                <TextInput
                    mt="sm"
                    label="Price"
                    placeholder=""
                    key={form.key('price')}
                    {...form.getInputProps('price')}
                />

                <Group justify="flex-end" mt="md">
                    <Button type="submit">Submit</Button>
                </Group>
            </form>
        </Box>
    )
}

export default AddListing;