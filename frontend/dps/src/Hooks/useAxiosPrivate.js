import { axiosPrivate } from "../http/axios";
import { useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";

import useRefreshToken from "./useRefreshToken";
import useAuth from "./useAuth";

const useAxiosPrivate = () => {
    const refreshAsync = useRefreshToken();
    const { auth, setAuth } = useAuth();

    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        const requestInteceptor = axiosPrivate.interceptors.request.use(
            config => {
                if (!config.headers['Authorization']) {
                    config.headers['Authorization'] = `Bearer ${auth?.accessToken}`
                }

                return config;
            }, (error) => {
                Promise.reject(error)
            }
        )

        const responseInterceptor = axiosPrivate.interceptors.response.use(
            response => response,
            async (error) => {
                const prevRequest = error?.config;
                if (error?.response?.status === 401 && !prevRequest?.sent) {
                    prevRequest.sent = true //prevent endless loop of calling this multiple times 

                    try {
                        const response = await refreshAsync();

                        setAuth(prev => {
                            return { ...prev, 
                                accessToken: response.data.accessToken,
                            };
                        });

                        prevRequest.headers['Authorization'] = `Bearer ${response.data.accessToken}`;
                    }
                    catch (err) {
                        console.log(JSON.stringify(err))
                        console.log("could not refresh refresh token")
                        navigate("/login", { state: { from: location }, replace: true });

                    }
                    return axiosPrivate(prevRequest)
                }
            }
        )

        return () => {
            axiosPrivate.interceptors.request.eject(requestInteceptor)
            axiosPrivate.interceptors.response.eject(responseInterceptor)
        }
    }, [auth, refreshAsync]);

    return axiosPrivate;
}

export default useAxiosPrivate;