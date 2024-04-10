import { filterDataOptions } from "./Dashboard.js";
import { setEndpointAndToken } from "../Shared/common.js";
import { makeRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";
import { handleDOMContentLoadedState } from "../Shared/common.js";

const baseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;
document.addEventListener('DOMContentLoaded', function () {
    handleDOMContentLoadedState();
});

export function fetchCardDetails(cardId) {

    var { token, apiUrl, } = setEndpointAndToken(cardId);

    // Remove leading '?' if there are query parameters
    if (apiUrl.endsWith('?')) {
        apiUrl = apiUrl.slice(0, -1);
    }    

    makeRequest("GET", apiUrl,'' ,token)
        .then(cardData => {
            const tableBody = document.getElementById('cardTableBody');

            tableBody.innerHTML = `
            <tr>
                <td>Card ID:</td>
                <td>${cardData.cardId}</td>
            </tr>
            <tr>
                <td><b>Name:</b></td>
                <td>${cardData.name}</td>
            </tr>
            <tr>
                <td><b>Description:</b></td>
                <td>${cardData.description}</td>
            </tr>
            <tr>
                <td><b>Date of Creation:</b></td>
                <td>${new Date(cardData.dateOfCreation).toLocaleString()}</td>
            </tr>
            <tr>
                <td><b>Status:</b></td>
                <td>${cardData.status}</td>
            </tr>
            <tr>
                <td><b>Created By:</b></td>
                <td>${cardData.createdByAppUser}</td>
            </tr>
            <tr>
                <td><b>Color:</b></td>
                <td style="background-color: ${cardData.color};"></td>
            </tr>
        `;          

            // Add cs-hidden class to all elements with the class name "dashboard-section"
            const dashboardSections = document.getElementsByClassName('dashboard-section');
            for (let i = 0; i < dashboardSections.length; i++) {
                dashboardSections[i].classList.add('cs-hidden');
            }
            document.getElementById('card-createform').classList.add('cs-hidden');
            document.getElementById('cardTable').classList.remove('cs-hidden');

            // Construct the Card Details URL
            const cardDetailsURL = `${baseURL}/Home/CardDetails/${cardData.cardId}`;

            // Update the URL in the address bar
            window.history.pushState({ path: cardDetailsURL }, '', cardDetailsURL);

        })
        .catch(error => {
            showErrorToast(error.message);
        });
}
