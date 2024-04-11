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