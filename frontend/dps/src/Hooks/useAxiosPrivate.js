import { axiosPrivate } from "../http/axios";
import { useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import useAuth from "./useAuth";
import useTokenRefresh from "./useTokenRefresh";

const useAxiosPrivate = () => {
    const { auth, setAuth } = useAuth();
    const refreshTokenAndUserInfo = useTokenRefresh();
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        const requestInterceptor = axiosPrivate.interceptors.request.use(
            (config) => {
                const token = auth?.accessToken;
                if (token && !config.headers['Authorization']) {
                    config.headers['Authorization'] = `Bearer ${token}`;
                }
                return config;
            },
            (error) => Promise.reject(error)
        );

        const responseInterceptor = axiosPrivate.interceptors.response.use(
            (response) => response,
            async (error) => {
                const prevRequest = error?.config;

                if (error?.response?.status === 401 && !prevRequest?.sent) {
                    prevRequest.sent = true; // Prevent endless loop

                    try {
                        // Step 1: Refresh token and get user info
                        const newAccessToken = await refreshTokenAndUserInfo();

                        // Step 2: Update token for the retried request
                        prevRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
                        return axiosPrivate(prevRequest);
                    } catch (err) {
                        console.error("Could not refresh token or fetch user info", err);
                        navigate("/login", { state: { from: location }, replace: true });
                        return Promise.reject(err);
                    }
                }

                return Promise.reject(error);
            }
        );

        return () => {
            axiosPrivate.interceptors.request.eject(requestInterceptor);
            axiosPrivate.interceptors.response.eject(responseInterceptor);
        };
    }, [auth?.accessToken, setAuth, navigate, location, refreshTokenAndUserInfo]);

    return axiosPrivate;
};

export default useAxiosPrivate;
