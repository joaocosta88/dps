import config from "../Config"
import { makeGetRequest, makeAnonymousPostRequest, makePostRequest } from "./HttpRequestManager";

export async function performLogin(email, password) {
    return await makeAnonymousPostRequest(config.API_URL + "/user/login",
        {
            "email": email,
            "password": password
        });
}

export async function refreshAccessToken(accessToken, refreshToken) {
    console.log("refreshing token");
    return await makeAnonymousPostRequest(config.API_URL+"/user/refreshtoken", {
        "accessToken": accessToken,
        "refreshToken": refreshToken
    });
}

export async function registerUser(email, password) {
    return await makeAnonymousPostRequest(config.API_URL + "/user/register",
        {
            "email": email,
            "password": password
        });
}

export async function getUserInfo(accessToken) {
    return await makeGetRequest(config.API_URL + "/user/profile", accessToken);
}