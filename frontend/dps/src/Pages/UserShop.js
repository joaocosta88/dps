import useAxiosPrivate from "../hooks/useAxiosPrivate";
import { routes } from '../http/routes';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import UsersListingPage from './UsersListingPage';

const UserShop = () => {
    const { userId } = useParams();
    const [user, setUser] = useState(null); // Use `null` instead of empty string for clarity
    const axiosPrivate = useAxiosPrivate();

    useEffect(() => {
        const controller = new AbortController();

        const fetchUserData = async () => {
            try {
                const response = await axiosPrivate.get(routes.users.byUsername + userId, {
                    signal: controller.signal,
                });
                
                setUser(response.data);
            } catch (error) {
                console.error(error);
            }
        };

        fetchUserData();

        return () => controller.abort();
    }, [userId]); // Add dependency array

    if (!user) {
        return <p>Loading...</p>;
    }

    return (
        <section>
            <h2>{user.username}'s listing</h2>
            <br />
            <UsersListingPage user={user} />
        </section>
    );
};

export default UserShop;
