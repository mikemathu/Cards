export function generateUrlWithUserId(endpoint, token) {
    const decodedToken = JSON.parse(atob(token.split('.')[1]));

    if (decodedToken && decodedToken.AppUserId) {

        const baseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;
        const url = `${baseURL}/api/appUsers/${decodedToken.AppUserId}/cards/${endpoint}?`;
        //const url2 = `${baseURL}/api/appUsers/${decodedToken.AppUserId}/cards/${endpoint}`;


        //var url = `https:// localhost:7265/api/appUsers/${decodedToken.AppUserId}/cards/${endpoint}`;
        return url;
    } else {
        throw new Error('ID not found in token');
    }
}
/*
function parseJwt(token) {
    try {
        return JSON.parse(atob(token.split('.')[1]));
    } catch (e) {
        return null;
    }
}*/


export function makePostRequest(requestMethod, apiUrl, data, token) {

    document.getElementById('loader').style.display = 'block';

    return fetch(apiUrl, {
        method: requestMethod,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (!response.ok) {
                if (response.status === 400) throw new Error("Invalid color code submitted.");
                if (response.status === 401) throw new Error("User is not authenticated.");
                if (response.status === 403) throw new Error("Access Forbidden. You don't have permission to access this resource.");
                if (response.status === 404) throw new Error("User not found in the database.");
                if (response.status === 422) throw new Error("One or more mandatory fields are not submitted.");
                if (response.status === 500) throw new Error("Server error. Pleace try again later.");
            }
            return response.json();
        })
        .catch(error => {
            document.getElementById('loader').style.display = 'none';
            throw error;
        });
}

export function showErrorToast(message) {
    var errorToast = document.getElementById('errorToast');
    var errorMessage = document.getElementById('errorMessage');

    errorMessage.innerText = message;

    var toast = new bootstrap.Toast(errorToast);
    toast.show();
}

export function makeGetRequest(requestMethod, apiUrl, token) {

    document.getElementById('loader').style.display = 'block';

    return fetch(apiUrl, {
        method: requestMethod,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    })
        .then(response => {
            if (!response.ok) {
                if (response.status === 400) throw new Error("Invalid color code submitted.");
                if (response.status === 401) throw new Error("User is not authenticated.");
                if (response.status === 403) throw new Error("Access Forbidden. You don't have permission to access this resource.");
                if (response.status === 404) throw new Error("User not found in the database.");
                if (response.status === 422) throw new Error("One or more mandatory fields are not submitted.");
                if (response.status === 500) throw new Error("Server error. Pleace try again later.");
            }
            return response.json();
        })
        .catch(error => {
            document.getElementById('loader').style.display = 'none';
            throw error;
        });
}