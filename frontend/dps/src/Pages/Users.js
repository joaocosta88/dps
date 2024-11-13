import React, { useState, useEffect } from "react";
import useAxiosPrivate from "../hooks/useAxiosPrivate";
import { routes } from '../http/routes';

const Users = () => {
    const [users, setUsers] = useState();
    const axiosPrivate = useAxiosPrivate();

    useEffect(() => {
        const controller = new AbortController();

        const getUsers = async () => {
            try {
                const response = await axiosPrivate.get(routes.users.all, 
                    { 
                        ...axiosPrivate.defaults,
                        signal: controller.signal 
                    });
                setUsers(response.data);
            } catch (err) {
                console.error(err);
            }
        }

        getUsers();

        return () => {
            controller.abort();
        };
    }, []);

    return (
        <article>
            <h2>Users List</h2>
            {users?.length ? (
                <ul>
                    {users.map((user, i) => (
                        <li key={i}>{user?.userName}</li>
                    ))}
                </ul>
            ) : (
                <p></p>
            )}
        </article>
    );
};

export default Users;