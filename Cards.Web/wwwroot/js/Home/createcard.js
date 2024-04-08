
//import { generateUrlWithUserId } from "../Shared/common.js";
import { makePostRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";
import { setEndpointAndToken } from "../Shared/common.js";
import { filterDataOptions } from "./Dashboard.js";
import { backButtonClick } from "../Shared/common.js";

const createCardForm = document.querySelector('.cs-createCard');
createCardForm.addEventListener('submit', function (event) {
    event.preventDefault();
    console.log("1. inside createCard event listener");

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
    console.log("2. inside createCard event listener");

    makePostRequest("POST", apiUrl, cardData, token)
        .then(data => {
            // Redirect to the newly created card page with all data
            //window.location.href = `/Home/CardDetails/${data.cardId}`;
            showErrorToast("Card created successfully");
            console.log("success");
            filterDataOptions();
            backButtonClick();
        })
        .catch(error => {
            showErrorToast(error.message);
        });
});

