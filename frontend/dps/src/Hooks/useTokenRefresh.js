import { useEffect } from "react";
import axios from "../http/axios";
import { routes } from "../http/routes";
import useAuth from "./useAuth";

const useTokenRefresh = () => {
    const { auth, setAuth } = useAuth();

    // This function handles the process of refreshing the token and updating the user info
    const refreshTokenAndUserInfo = async () => {
        try {
            // Step 1: Refresh token to get the new access token
            const refreshTokenResponse = await axios.post(routes.auth.refreshToken, {}, {
                withCredentials: true,
            });
            const newAccessToken = refreshTokenResponse.data.accessToken;

            // Step 2: Fetch user info using the new access token
            const userInfoResponse = await axios.get(routes.users.me, {
                headers: { Authorization: `Bearer ${newAccessToken}` },
                withCredentials: true,
            });

            // Step 3: Update the auth state with the new access token and user info
            setAuth((prev) => ({
                ...prev,
                email: userInfoResponse.data.data.email,
                roles: refreshTokenResponse.data.roles,
                accessToken: newAccessToken,
            }));


            return newAccessToken;
        } catch (err) {
            console.error("Error during token refresh or user info fetch:", err);
            throw err;
        }
    };

    return refreshTokenAndUserInfo;
};

export default useTokenRefresh;
