import { makeRequest } from "../Shared/Home/common.js";
import { showErrorToast } from "../Shared/Home/common.js";
import { setEndpointAndToken } from "../Shared/Home/common.js";
import { fetchCardDetails } from "./CardDetails.js";
import { handleDOMContentLoadedState } from "../Shared/Home/common.js";

document.addEventListener('DOMContentLoaded', function () {
    handleDOMContentLoadedState();
});

const createCardForm = document.getElementById('createCardForm');

if (createCardForm !== null) {
    createCardForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const cardId = 0;

        var { token, apiUrl, } = setEndpointAndToken(cardId);

        if (apiUrl.endsWith('?')) {
            apiUrl = apiUrl.slice(0, -1);
        }

        // Get form data
        const cardData = {
            Name: document.getElementById("cardName").value,
            Description: document.getElementById("cardDescription").value,
            Color: document.getElementById("cardColor").value
        };

        makeRequest("POST", apiUrl, cardData, token)
            .then(data => {
                ;
                showErrorToast("Card created successfully");

                const backButton = document.querySelector('.cardDetailsBackBtn');
                if (backButton !== null) {
                    backButton.id = 'backtoCardsAndFresh';
                }

                fetchCardDetails(data.cardId);
            })
            .catch(error => {
                showErrorToast(error.message);
            });
    });
}