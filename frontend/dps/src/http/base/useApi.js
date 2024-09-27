const sendProtectedRequest = (
    method,
    path, 
    body,
    accessToken
) => {
    return sendRequest(method, path, body, accessToken);
};

const sendUnprotectedRequest = (
    method,
    path,
    body,
) => {
    return sendRequest(method, path, body, null);
};

const sendRequest = (
    method,
    path,
    body,
    authToken
) => {
    return fetch(path, {
        method,
        ...(body && { body: JSON.stringify(body) }),
        headers: {
            "Content-Type": "application/json",
            ...(authToken && { Authorization: `Bearer ${authToken}` }),
        },
    }).then((response) => {
        if (response.status >= 400) {
            throw response;
        }
        return response.json();
    });
}

export const useApi = () => {
    return { sendUnprotectedRequest, sendProtectedRequest };
};
