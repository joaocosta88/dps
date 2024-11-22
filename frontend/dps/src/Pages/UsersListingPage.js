import ListingsPage from "./ListingsPage"
import useAxiosPrivate from "../hooks/useAxiosPrivate";
import { routes } from '../http/routes';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import userEvent from "@testing-library/user-event";

const UsersListingPage = () => {
    const { userId } = useParams();
    const [username, setUsername] = useState('');

    const axiosPrivate = useAxiosPrivate();
    useEffect(() => {

        const controller = new AbortController();

        const fetchUserData = async () => {
            const response = await axiosPrivate.get(routes.users.byUsername + userId,
                {
                    signal: controller.signal
                });

            setUsername(response.data.username);
        }

        fetchUserData();

        return () => controller.abort();
    }, [userId]);

    return (
        <section>
            <h1>Home</h1>
            <br />
            <br />
            <h2>{username}'s listing</h2>
            <br />
            <ListingsPage userId={userId} />
        </section>
    );
};

export default UsersListingPage;