import { makeRequest } from "../Shared/Auth/shared.js";
import { showErrorToast } from "../Shared/Auth/shared.js";

const baseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;
const apiUrl = `${baseURL}/api/authentication/login`;

if (apiUrl.endsWith('?')) {
    apiUrl = apiUrl.slice(0, -1);
}

document.getElementById('loginForm').addEventListener('submit', handleFormSubmit);

function handleFormSubmit(event) {
    event.preventDefault(); 


    const userData = {
        email: document.getElementById('email').value,
        password: document.getElementById('password').value,
    };

    makeRequest("POST", apiUrl, userData, 'Login')
        .then(response => {
            const token = response.token; 
            if (token) {
                localStorage.setItem('token', token);
                //window.location.href = baseURL;
                window.location.href = '/Home/Dashboard';
            }
        })
        .catch(error => {
            showErrorToast(error.message);
        });
}
/*function makeRequest(requestMethod, apiUrl, data, token) {

    document.getElementById('loader').classList.remove('hide-loader');

    return fetch(apiUrl, {
        method: requestMethod,
        headers: {
            'Content-Type': 'application/json',
            ...(token !== null ? { 'Authorization': `Bearer ${token}` } : {})
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (!response.ok) {
                if (response.status === 401) throw new Error("Invalid Username or Password.");
                if (response.status === 422) throw new Error("One or more mandatory fields are not submitted.");
                if (response.status === 500) throw new Error("Server error. Pleace try again later.");
            }

            document.getElementById('loader').classList.add('hide-loader');

            return response.json();
        })      
        .catch(error => {
            document.getElementById('loader').classList.add('hide-loader');
            throw error;
        });
}

function showErrorToast(message) {
    var errorToast = document.getElementById('errorToast');
    var errorMessage = document.getElementById('errorMessage');

    errorMessage.innerText = message;

    var toast = new bootstrap.Toast(errorToast);
    toast.show();
}*/