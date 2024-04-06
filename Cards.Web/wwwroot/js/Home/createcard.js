
import { generateUrlWithUserId } from "../Shared/common.js";
import { makePostRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";

document.getElementById('createCard').addEventListener('submit', function (event) {
    event.preventDefault();


    const token = sessionStorage.getItem('token');

    const apiUrl = generateUrlWithUserId('', token);

    // Get form data
    const cardData = {
        Name: document.getElementById("cardName").value,
        Description: document.getElementById("cardDescription").value,
        Color: document.getElementById("cardColor").value
    };

    makePostRequest("POST", apiUrl, cardData, token)
        .then(data => {
            // Redirect to the newly created card page with all data
            window.location.href = `/Home/CardDetails/${data.cardId}`;
        })
        .catch(error => {
            showErrorToast(error.message);
        });
});
