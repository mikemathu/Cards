import { filterDataOptions } from "../../Home/Dashboard.js";
import { createCardBtnClick } from "../../Home/Dashboard.js";
import { fetchCardDetailsForEditing } from "../../Home/EditCard.js";
import { fetchCardDetails } from "../../Home/CardDetails.js";

const baseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;

export function setEndpointAndToken(cardId = null) {
    const token = localStorage.getItem('token');
    const decodedToken = token ? JSON.parse(atob(token.split('.')[1])) : null;

    let endpoint = '';

    if (cardId === null) {
        if (decodedToken && decodedToken.role === 'admin') {
            endpoint = 'all';
        } else {
            endpoint = 'forUser';
        }
    } else if (cardId === 0) {
        endpoint = '';
    } else {
        endpoint = cardId.toString();
    }

    let apiUrl = generateUrlWithUserId(endpoint, token);

    return {
        token: token,
        apiUrl: apiUrl
    };
}


function generateUrlWithUserId(endpoint, token) {
    const decodedToken = JSON.parse(atob(token.split('.')[1]));

    if (decodedToken && decodedToken.AppUserId) {

        const url = `${baseURL}/api/appUsers/${decodedToken.AppUserId}/cards/${endpoint}?`;

        return url;
    } else {
        throw new Error('Something went wrong');
    }
}


export function makeRequest(requestMethod, apiUrl, data, token) {

    document.getElementById('loader').style.display = 'block';

    
    return fetch(apiUrl, {
        method: requestMethod,
        headers: {
            'Content-Type': 'application/json',
            ...(token !== null ? { 'Authorization': `Bearer ${token}` } : {})
        },
        body: requestMethod !== "GET" ? JSON.stringify(data) : undefined
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

            document.getElementById('loader').style.display = 'none';

            if (requestMethod === "DELETE" || requestMethod === "PUT") {
                filterDataOptions();
                backButtonClick();
                return;
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
export function handleDOMContentLoadedState() {
    const cardId = window.location.pathname.split('/').pop();

    const isCreatePage = window.location.pathname.includes('Home/CreateCard');

    const isEditPage = window.location.pathname.includes('Home/CardEdit');

    const isDetailsPage = window.location.pathname.includes('Home/CardDetails');

    if (isCreatePage) {
        createCardBtnClick();
    }else if (isEditPage && cardId !== "") {
        fetchCardDetailsForEditing(cardId);
    } else if (isDetailsPage && cardId !== "") {
        fetchCardDetails(cardId);
    }
}

const backButtons = document.querySelectorAll('.cs-backtoCards');
// Loop through each back button and add click event listener
backButtons.forEach(button => {
    button.addEventListener('click', backButtonClick);
});

export function backButtonClick() {

    const dashboardSections = document.getElementsByClassName('dashboard-section');

    for (let i = 0; i < dashboardSections.length; i++) {
        dashboardSections[i].classList.remove('cs-hidden');
    }
    document.getElementById('cardEditTable').classList.add('cs-hidden');
    document.getElementById('cardTable').classList.add('cs-hidden');
    document.getElementById('card-createform').classList.add('cs-hidden');

    // Construct the new URL
    const homeURL = `${baseURL}`;

    // Update the URL in the address bar
    window.history.pushState({ path: homeURL }, '', homeURL);

    const backButtonwithRefresh = document.getElementById('backtoCardsAndFresh');
    if (backButtonwithRefresh !== null) {
        filterDataOptions();
    }
}





