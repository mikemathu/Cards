
import { generateUrlWithUserId } from "../Shared/common.js";
import { makePostRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";

document.getElementById('createCard').addEventListener('submit', function (event) {
    event.preventDefault();


    const token = sessionStorage.getItem('token');

    const apiUrl = generateUrlWithUserId('', token);

    // Get form data
    const cardData = {
        Name: document.getElementById("cardName").value,
        Description: document.getElementById("cardDescription").value,
        Color: document.getElementById("cardColor").value
    };

    makePostRequest("POST", apiUrl, cardData, token)
        .then(data => {
            // Redirect to the newly created card page with all data
            window.location.href = `/Home/CardDetails/${data.cardId}`;
        })
        .catch(error => {
            showErrorToast(error.message);
        });
});

/*function generateUrlWithUserId(endpoint, token) {
    const decodedToken = parseJwt(token);

    if (decodedToken && decodedToken.AppUserId) {
        var url = `https://localhost:7265/api/appUsers/${decodedToken.AppUserId}/cards/${endpoint}`;
        return url;
    } else {
        throw new Error('User ID not found in token');
    }
}

function parseJwt(token) {
    try {
        return JSON.parse(atob(token.split('.')[1]));
    } catch (e) {
        return null;
    }
}


function makePostRequest(requestMethod, apiUrl, data, token) {

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


function showErrorToast(message) {
    var errorToast = document.getElementById('errorToast');
    var errorMessage = document.getElementById('errorMessage');

    errorMessage.innerText = message;

    var toast = new bootstrap.Toast(errorToast);
    toast.show();
}

*/