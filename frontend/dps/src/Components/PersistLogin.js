import { Outlet } from "react-router-dom";
import { useState, useEffect } from "react";
import useRefreshToken from "../hooks/useRefreshToken";
import useAuth from "../hooks/useAuth";

const PersistLogin = () => {
    const [isLoading, setIsLoading] = useState(true)
    const refresh = useRefreshToken();
    const { auth, setAuth } = useAuth();

    useEffect(() => {
        const verifyRefreshToken = async () => {
            try {
                var response = await refresh();

                setAuth(prev => {
                    console.log("previous state " +JSON.stringify(prev))
                    return { ...prev, 
                        roles: response.data.roles,
                        accessToken: response.data.accessToken
                    };
                });
                 
            } catch (err) {
                console.log(err);
            }
            finally {
                setIsLoading(false)
            }

        }

        !auth?.accessToken ? verifyRefreshToken() : setIsLoading(false)

    }, [])

    useEffect(() => {
        console.log("is loading "+isLoading)
        console.log("at "+JSON.stringify(auth?.accessToken))

    }, [isLoading])

    return (
        <>
        {isLoading ? <p>is loading...</p> : <Outlet />}
        </>
    )
}

export default PersistLogin;