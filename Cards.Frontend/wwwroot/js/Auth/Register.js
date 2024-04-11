import { makeRequest } from "../Shared/Auth/shared.js";
import { showErrorToast } from "../Shared/Auth/shared.js";

const baseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;
const apiUrl = `${baseURL}/api/authentication`;

if (apiUrl.endsWith('?')) {
    apiUrl = apiUrl.slice(0, -1);
}

document.getElementById('registerForm').addEventListener('submit', handleRegisterFormSubmit);

function handleRegisterFormSubmit(event) {
    event.preventDefault();


    const userData = {
        email: document.getElementById('registerPassword').value,
        password: document.getElementById('registerEmail').value,
    };

    makeRequest("POST", apiUrl, userData, 'Register')
        .then(response => {
            showErrorToast("User registered successfully.");
            //window.location.href = '/Auth/Login';
        })
        .catch(error => {
            showErrorToast(error.message);
        });
}