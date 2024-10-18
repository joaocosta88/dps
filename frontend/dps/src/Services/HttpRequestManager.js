import { showNotification } from '@mantine/notifications';
import { refreshAccessToken } from "./BackendHttpService";
import { useContext } from "react";
import AuthContext from '../Providers/AuthProvider.bkp.';
import axios from '../http/axios'
import GeneralHttpError from '../Components/Errors/GeneralHttpError'

export async function makeGetRequest(url, accessToken) {
    return makeRequestAsync(url, "get",
        {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + accessToken
        }
    );
}

export async function makeAnonymousPostRequestAsync(url, data) {
    return await makeRequestAsync(url, "post",
        {
            "Content-Type": "application/json",
            "testing":"asdasd"
        },
        data
    );
}

export async function makePostRequest(url, data, accessToken) {
    return makeRequestAsync(url, "post", 
        {
            ContentType: "application/json",
            "Authorization": "Bearer " + accessToken
        },
        data
    );
}

async function makeRequestAsync(url, method, headers, data) {

    try {
        console.log(headers)
        let response = await axios({
            method: method,
            url: url,
            headers: headers,
            data: data,
            withCredentials: true
        });

        //refresh token and retry request
        // if (response.status === 401) {

        //     await auth.RefreshCurrentAccessToken();
        //     response = await fetch(url, options);
        // }
        // if (!response)

        // if (!response.ok) {
        //     throw new Error(`HTTP error! status: ${response.status}`);
        // }

        return response;
    } catch (error) {
        console.log(error)
        throw error;
    }
}