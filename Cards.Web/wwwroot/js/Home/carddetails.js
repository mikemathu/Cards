//document.addEventListener('DOMContentLoaded', loadCardDetails);
import { setEndpointAndToken } from "../Shared/common.js";
import { makeRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";
import { handlePopState } from "../Shared/common.js";
import { handleDOMContentLoadedState } from "../Shared/common.js";
import { backButtonClick } from "../Shared/common.js";


window.addEventListener('popstate', handlePopState);
document.addEventListener('DOMContentLoaded', function () {
    handleDOMContentLoadedState();
});

const backButtons = document.querySelectorAll('.backtoCards');
backButtons.forEach(button => {
    button.addEventListener('click', backButtonClick);
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
            <th>Card ID:</th>
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
            // Redirect to a new page
           


            // Add cs-hidden class to all elements with the class name "dashboard-section"
            const dashboardSections = document.getElementsByClassName('dashboard-section');
            for (let i = 0; i < dashboardSections.length; i++) {
                dashboardSections[i].classList.add('cs-hidden');
            }
            document.getElementById('cardTable').classList.remove('cs-hidden');

            // Construct the new URL
            const newUrl = `https://localhost:7265/Home/CardDetails/${cardData.cardId}`;

            // Update the URL in the address bar
            window.history.pushState({ path: newUrl }, '', newUrl);

        })
        .catch(error => {
            showErrorToast(error.message);
        });






}
