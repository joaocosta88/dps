import { useState, useEffect } from "react";
import useAxiosPrivate from "../hooks/useAxiosPrivate";
import useRefreshToken from "../hooks/useRefreshToken";
import { routes } from "../http/routes"

const Users = () => {
    const [users, setUsers] = useState();
    const refresh = useRefreshToken();
    const axiosPrivate = useAxiosPrivate();

    useEffect(() => {
        let isMounted = true;
        const controller = new AbortController();

        const getUsers = async () => {
            try {
                const response = await axiosPrivate.get(routes.auth.all, {
                    signal: controller.signal,
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
                        {users.map((user, i) =>
                            <li key={i}>{user?.email}</li>)}
                    </ul>)
                : <p>No users to display</p>
            }
            <button onClick={() => {
                try {
                    refresh()
                }
                catch (err) {
                    console.log(err);
                }
            }
            }> Refresh</button>
            <br />
        </article >
    )
}

export default Users;