import  { useContext } from 'react';
import { useForm } from '@mantine/form';
import { TextInput, Box, NumberInput, Textarea, Button, FileInput, Group } from '@mantine/core';
import { AuthContext } from '../Providers/AuthProvider';
import { createListing } from "../Services/BackendHttpService";

const CreateComponent = () => {

    const auth = useContext(AuthContext)
    
    const handleSubmit = async (values) => {

        createListing(auth.token, values.name, values.description, values.price, values.files)
        // const formData = new FormData();
        // formData.append('name', values.name);
        // formData.append('description', values.description);
        // values.file.forEach((file, index) => {
        //     formData.append(`file${index}`, file);
        // });

        // try {
        //     const response = await fetch('/api/upload', {
        //         method: 'POST',
        //         body: formData,
        //     });

        //     if (!response.ok) {
        //         throw new Error(`HTTP error! status: ${response.status}`);
        //     }

        //     const result = await response.json();
        //     console.log('Upload successful:', result);
        // } catch (error) {
        //     console.error('Error uploading data:', error);
        // }
    };

    const form = useForm({
        mode: 'uncontrolled',
        validateInputOnChange: true,
        initialValues: {
            name: '',
            description: '',
            price: '',
            files: []
        },

        // functions will be used to validate values at corresponding key
        validate: {
        files: (value) => {
            if (value.length > 4)
                return 'Please upload up to 4 files' 
            return null
        },

        },
    });


    return (
        <Box maw={340} mx="auto">

            <form onSubmit={form.onSubmit(handleSubmit)}>
                <TextInput
                    label="Name"
                    placeholder="Name"
                    key={form.key('name')}
                    {...form.getInputProps('name')}
                    required
                />
                <Textarea
                    label="Description"
                    placeholder="Description"
                    key={form.key('description')}
                    {...form.getInputProps('description')}
                    required
                />
                <NumberInput
                    label="Price"
                    thousandSeparator=" "
                    decimalScale={2}
                    key={form.key('price')}
                    {...form.getInputProps('price')}
                    required
                />
                <FileInput
                    label="Upload up to 4 images"
                    placeholder="Select images"
                    multiple
                    accept="image/*"
                    key={form.key('files')}
                    {...form.getInputProps('files')}
                />
                <Group justify="flex-end" mt="md">
                    <Button type="submit">Submit</Button>
                </Group>
            </form>
        </Box>
    );
};

export default CreateComponent;