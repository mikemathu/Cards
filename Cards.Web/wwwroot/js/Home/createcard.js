import { makeRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";
import { setEndpointAndToken } from "../Shared/common.js";
import { filterDataOptions } from "./Dashboard.js";
import { backButtonClick } from "../Shared/common.js";

const createCardForm = document.querySelector('.cs-createCard');

if (createCardForm !== null) {
    createCardForm.addEventListener('submit', function () {

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
                filterDataOptions();
                backButtonClick();
            })
            .catch(error => {
                showErrorToast(error.message);
            });
    });
}