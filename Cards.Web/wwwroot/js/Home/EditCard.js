//document.addEventListener('DOMContentLoaded', loadCardDetails);
import { setEndpointAndToken } from "../Shared/common.js";
import { makePostRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";
import { handlePopState } from "../Shared/common.js";
import { handleDOMContentLoadedState } from "../Shared/common.js";
import { backButtonClick } from "../Shared/common.js";

// Add event listener for popstate event
window.addEventListener('popstate', handlePopState);

document.addEventListener('DOMContentLoaded', function () {

    handleDOMContentLoadedState();
});

// Select the button with the class "backtoCards"
// Select all elements with the class "backtoCards"
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


    makePostRequest("GET", apiUrl, '', token)
        .then(cardData => {
            // Populate form fields with fetched data
            document.getElementById('Name').value = cardData.name;
            document.getElementById('Description').value = cardData.description;
            document.getElementById('Status').value = cardData.status;
            document.getElementById('Color').value = cardData.color;

            // Add cs-hidden class to all elements with the class name "dashboard-section"
            const dashboardSections = document.getElementsByClassName('dashboard-section');
            for (let i = 0; i < dashboardSections.length; i++) {
                dashboardSections[i].classList.add('cs-hidden');
            }
            document.getElementById('cardEditTable').classList.remove('cs-hidden');

            // Construct the new URL
            const newUrl = `https://localhost:7265/Home/CardEdit/${cardData.cardId}`;

            // Update the URL in the address bar
            window.history.pushState({ path: newUrl }, '', newUrl);
        })
        .catch(error => {
            showErrorToast(error.message);
        });
}

const createCardForm = document.getElementById('editCardForm');
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

    makePostRequest("PUT", apiUrl, cardData, token)
        .then(data => {
            showErrorToast("Card Updated successfully");
        })
        .catch(error => {
            showErrorToast(error.message);
        });
});
