//document.addEventListener('DOMContentLoaded', loadCardDetails);
import { setEndpointAndToken } from "../Shared/common.js";
import { makeGetRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";

// Event listener for popstate event
// Function to handle popstate event
function handlePopState(event) {
    console.log("popstate");
    if (event.state && event.state.path) {
        console.log("inside if statement popstate");
        // Extract cardId from the URL
        const cardId = event.state.path.split('/').pop();
        // Fetch and display card details
        fetchCardDetailsForEditing(cardId);
    }
}

// Add event listener for popstate event
window.addEventListener('popstate', handlePopState);

// Check URL and trigger actions on DOMContentLoaded
document.addEventListener('DOMContentLoaded', function () {
    // Extract cardId from the current URL
    const cardId = window.location.pathname.split('/').pop();
    // Fetch and display card details
    fetchCardDetailsForEditing(cardId);
});

export function fetchCardDetailsForEditing(cardId) {

    var { token, apiUrl, } = setEndpointAndToken(cardId);

    // Remove leading '?' if there are query parameters
    if (apiUrl.endsWith('?')) {
        apiUrl = apiUrl.slice(0, -1);
    }       


    makeGetRequest("GET", apiUrl, token)
        .then(cardData => {
            // Populate form fields with fetched data
            document.getElementById('Name').value = cardData.name;
            document.getElementById('Description').value = cardData.description;
            document.getElementById('Color').value = cardData.color;
        })
        .catch(error => {
            showErrorToast(error.message);
        });
}
