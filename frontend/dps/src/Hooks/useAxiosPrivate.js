import axios, { axiosPrivate } from "../http/axios";
import { useEffect } from "react";
import useRefreshToken from "./useRefreshToken";
import useAuth from "./useAuth";
import { useContext } from 'react';
import AuthContext from "../providers/AuthProvider";

const useAxiosPrivate = () => {
    const refreshAsync = useRefreshToken();
    const { auth } = useContext(AuthContext)
    console.log("tokennnnn" +auth?.accessToken)

    useEffect(() => {
        const requestInteceptor = axiosPrivate.interceptors.request.use(
            config => {
                if (!config.headers['Authorization']) {
                    config.headers['Authorization'] = `Bearer ${auth?.accessToken}`
                }

                return config;
            }, (error) => Promise.reject(error)
        )

        const responseInterceptor = axiosPrivate.interceptors.response.use(
            response => response,
            async (error) => {
                const prevRequest = error?.config;
                if (error?.response?.status === 401 && !prevRequest?.sent ) {
                    prevRequest.sent = true //prevent endless loop of calling this multiple times 

                    const newAccessToken = await refreshAsync();
                    prevRequest.headers['Authorization'] = `Bearer ${newAccessToken}`;
                    return axiosPrivate(prevRequest)
                }

                return Promise.reject(error);
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