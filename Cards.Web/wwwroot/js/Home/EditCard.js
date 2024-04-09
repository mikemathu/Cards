import { setEndpointAndToken } from "../Shared/common.js";
import { makeRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";
import { handlePopState } from "../Shared/common.js";
import { handleDOMContentLoadedState } from "../Shared/common.js";
import { backButtonClick } from "../Shared/common.js";

const baseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;

// Add event listener for popstate event
window.addEventListener('popstate', handlePopState);

document.addEventListener('DOMContentLoaded', function () {
    handleDOMContentLoadedState();
});

const backButtons = document.querySelectorAll('.backtoCards');
// Loop through each back button and add click event listener
backButtons.forEach(button => {
    button.addEventListener('click', backButtonClick);
});

export function fetchCardDetailsForEditing(cardId) {

    var { token, apiUrl, } = setEndpointAndToken(cardId);

    // Remove leading '?' if there are query parameters
    if (apiUrl.endsWith('?')) {
        apiUrl = apiUrl.slice(0, -1);
    }      

    makeRequest("GET", apiUrl, '', token)
        .then(cardData => {
            // Populate form fields with fetched data
            document.getElementById('Name').value = cardData.name;
            document.getElementById('Description').value = cardData.description;
            document.getElementById('Color').value = cardData.color;

            // Set selected value for Status dropdown
            const statusDropdown = document.getElementById('Status');
            for (let i = 0; i < statusDropdown.options.length; i++) {
                if (statusDropdown.options[i].value === cardData.status) {
                    statusDropdown.selectedIndex = i;
                    break;
                }
            }

            // Add cs-hidden class to all elements with the class name "dashboard-section"
            const dashboardSections = document.getElementsByClassName('dashboard-section');
            for (let i = 0; i < dashboardSections.length; i++) {
                dashboardSections[i].classList.add('cs-hidden');
            }
            document.getElementById('cardEditTable').classList.remove('cs-hidden');

            // Construct the new URL
            const cardEditURL = `${baseURL}/Home/CardEdit/${cardData.cardId}/`;

            // Update the URL in the address bar
            window.history.pushState({ path: cardEditURL }, '', cardEditURL);
        })
        .catch(error => {
            showErrorToast(error.message);
        });
}

const createCardForm = document.getElementById('editCardForm');

if (createCardForm !== null) {
    createCardForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const cardId = window.location.pathname.split('/').pop();

        var { token, apiUrl, } = setEndpointAndToken(cardId);

        if (apiUrl.endsWith('?')) {
            apiUrl = apiUrl.slice(0, -1);
        }

        // Get form data
        const cardData = {
            Name: document.getElementById("Name").value,
            Description: document.getElementById("Description").value,
            Status: document.getElementById("Status").value,
            Color: document.getElementById("Color").value
        };

        makeRequest("PUT", apiUrl, cardData, token)
            .then(data => {
                showErrorToast("Card Updated successfully");
            })
            .catch(error => {
                showErrorToast(error.message);
            });
    });
}

