//document.addEventListener('DOMContentLoaded', loadCardDetails);
import { generateUrlWithUserId } from "../Shared/common.js";
import { makeGetRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";

function loadCardDetails(cardId) {

    const token = sessionStorage.getItem('token');

    const apiUrl = generateUrlWithUserId(cardId, token);

    makeGetRequest("GET", apiUrl, token)
        .then(cardData => {
            window.location.href = `/Home/CardDetails/${cardData.cardId}`;
        })
        .catch(error => {
            showErrorToast(error.message);
        });
} 
