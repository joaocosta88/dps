const accessTokenLocalStorageName = "accessToken"
const refreshTokenLocalStorageName = "refreshToken"
const accessTokenTimeoutTaskIdLocalStorageName = "timeoutTaskId"
const userInformationLocalStorageName = "user"


export function getAccessToken() {
    return localStorage.getItem(accessTokenLocalStorageName) || "";
}

export function setAccessToken(accessToken) {
    localStorage.setItem(accessTokenLocalStorageName, accessToken);
}

export function clearAccessToken() {
    localStorage.clear(accessTokenLocalStorageName);
}

export function getRefreshToken() {
    return localStorage.getItem(refreshTokenLocalStorageName) || "";
}

export function setRefreshToken(refreshToken) {
    localStorage.setItem(refreshTokenLocalStorageName, refreshToken);
}

export function clearRefreshToken() {
    localStorage.clear(refreshTokenLocalStorageName);
}

export function getAccessTokenTimeoutTaskId() {
    return localStorage.getItem(accessTokenTimeoutTaskIdLocalStorageName) || "";
}

export function setAccessTokenTimeoutTaskId(accessTokenTimeoutTaskId) {
    localStorage.setItem(accessTokenTimeoutTaskIdLocalStorageName, accessTokenTimeoutTaskId);
}

export function clearAccessTokenTimeoutTaskId() {
    localStorage.clear(accessTokenTimeoutTaskIdLocalStorageName);
}

export function getUserInformation() {
    return JSON.parse(localStorage.getItem(userInformationLocalStorageName)) || null;
}

export function setUserInformation(userInformation) {
    localStorage.setItem(userInformationLocalStorageName, userInformation);
}

export function clearUserInformation() {
    localStorage.clear(userInformationLocalStorageName);
}