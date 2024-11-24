import { Outlet } from "react-router-dom";
import { useState, useEffect } from "react";
import useAuth from "../hooks/useAuth";
import useTokenRefresh from "../hooks/useTokenRefresh";

const PersistLogin = () => {
    const [isLoading, setIsLoading] = useState(true);
    const { auth } = useAuth();
    const refreshTokenAndUserInfo = useTokenRefresh();

    useEffect(() => {

        const verifyRefreshToken = async () => {
            try {
                if (!auth?.accessToken) {
                    await refreshTokenAndUserInfo();
                }
            } catch (err) {
                console.error("Failed to refresh token:", err);
            } finally {
                setIsLoading(false);
            }
        };

        // Only try refreshing the token if we don't have an access token
        if (!auth?.accessToken) {
            verifyRefreshToken();
        } else {
            setIsLoading(false); // Already have the token, no need to refresh
        }
    }, [auth, refreshTokenAndUserInfo]);

    return (
        <>
            {isLoading ? <p>Loading...</p> : <Outlet />}
        </>
    );
};

export default PersistLogin;
