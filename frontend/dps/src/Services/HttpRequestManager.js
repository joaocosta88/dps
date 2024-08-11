import { showNotification } from '@mantine/notifications';

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

async function makeRequest(url, options) {
    try {
    const response = await fetch(url, options);
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    
    const res = await response.json();

    if (res.isSucceed === true) {
        return res.data;
    }

    throw new Error(res.message);
} catch(error) {
    showNotification({
        title: 'Error',
        message: error.message,
        color: 'red',
      });

    console.log(error)  
      throw error;
}
}