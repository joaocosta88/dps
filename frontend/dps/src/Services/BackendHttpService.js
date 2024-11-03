import config from "../Config"
import { makeGetRequest, makeAnonymousPostRequestAsync, makePostRequest } from "./HttpRequestManager";

export async function loginUserAsync(email, password) {
    return await makeAnonymousPostRequestAsync("/users/login",
        {
            "email": email,
            "password": password
        });
}

export async function refreshAccessToken(accessToken, refreshToken) {
    console.log("refreshing token");
    return await makeAnonymousPostRequestAsync(config.API_URL+"/users/refreshtoken", {
        "accessToken": accessToken,
        "refreshToken": refreshToken
    });
}

export async function registerUserAsync(email, password) {
    return await makeAnonymousPostRequestAsync("/users/register",
        {
            "email": email,
            "password": password
        });
}

export async function getUserInfo(accessToken) {
    return await makeGetRequest(config.API_URL + "/users/profile", accessToken);
}

export async function createListing(accessToken, name, description, price, files) {
    const formData = new FormData();
    formData.append('name', name);
    formData.append('description', description);
    formData.append('price', price);
    files.forEach((file, index) => {
        formData.append(`file${index}`, file);
    });

    return await makePostRequest(config.API_URL + "/api/listing", formData, accessToken);
}