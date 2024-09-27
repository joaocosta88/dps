import { showNotification } from '@mantine/notifications';
import { refreshAccessToken } from "./BackendHttpService";
import { useContext } from "react";
import AuthContext from '../Providers/AuthProvider';


export async function makeGetRequest(url, accessToken) {
    return makeRequest(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + accessToken
        },
    });
}

export async function makeAnonymousPostRequest(url, data) {
    return makeRequest(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data)
    });
}

export async function makePostRequest(url, data, accessToken) {
    return makeRequest(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + accessToken
        },
        body: data
    });
}

async function makeRequest(url, options, auth) {

    try {
        let response = await fetch(url, options);

        //refresh token and retry request
        if (response.status === 401) {

            await auth.RefreshCurrentAccessToken();
            response = await fetch(url, options);
        }

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const res = await response.json();

        if (res.isSucceed === true) {
            return res.data;
        }

        throw new Error(res.message);
    } catch (error) {
        showNotification({
            title: 'Error',
            message: error.message,
            color: 'red',
        });

        console.log(error)
        throw error;
    }
}