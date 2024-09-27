const accessTokenLocalStorageName = "accessToken"
const refreshTokenLocalStorageName = "refreshToken"
const userInformationLocalStorageName = "user"

function getAccessToken() {
    return localStorage.getItem(accessTokenLocalStorageName) || "";
}

function setAccessToken(accessToken) {
    localStorage.setItem(accessTokenLocalStorageName, accessToken);
}

function clearAccessToken() {
    localStorage.removeItem(accessTokenLocalStorageName);
}

function getRefreshToken() {
    return localStorage.getItem(refreshTokenLocalStorageName) || "";
}

function setRefreshToken(refreshToken) {
    localStorage.setItem(refreshTokenLocalStorageName, refreshToken);
}

function clearRefreshToken() {
    localStorage.removeItem(refreshTokenLocalStorageName);
}
function getUserInformation() {
    return JSON.parse(localStorage.getItem(userInformationLocalStorageName)) || null;
}

function setUserInformation(userInformation) {
    localStorage.setItem(userInformationLocalStorageName, userInformation);
}

function clearUserInformation() {
    localStorage.removeItem(userInformationLocalStorageName);
}

const AuthClientStore = {
    getAccessToken,
    setAccessToken,
    clearAccessToken,
    getRefreshToken,
    setRefreshToken,
    clearRefreshToken,
    getUserInformation,
    setUserInformation,
    clearUserInformation,
};

export default AuthClientStore;