import { useState, useEffect } from "react";
import axios from "../http/axios";

const Users = () => {
    const [users, setUsers] = useState();

    useEffect(() => {
        let isMounted = true;
        const controller = new AbortController();

        const getUsers = async () => {
            try {
                const response = await axios.get("/users", {
                    signal: controller.signal
                });

                console.log(response.data);
                isMounted && setUsers(response.data)
            } catch (err) {
                console.error(err);
            }
        }

        getUsers();

        return () => {
            isMounted = false;
            controller.abort();
        }
    }, [])

    return (
        <article>
            <h2>Users list</h2>
            {users?.length
                ? (
                    <ul>
                        {users.map((users, i) =>
                            <li key={i}>user?.name</li>)}
                    </ul>)
                : <p>No users to display</p>
            }
        </article>
    )
}

export default Users;