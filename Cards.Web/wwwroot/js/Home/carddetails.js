//document.addEventListener('DOMContentLoaded', loadCardDetails);
import { generateUrlWithUserId } from "../Shared/common.js";

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
    /*function generateUrlWithUserId(endpoint, token) {
        const decodedToken = parseJwt(token);

        if (decodedToken && decodedToken.AppUserId) {
            var url = `https://localhost:7265/api/appUsers/${decodedToken.AppUserId}/cards/${endpoint}`;
            return url;
        } else {
            throw new Error('ID not found in token');
        }
    }*/

    function makeGetRequest(requestMethod, apiUrl, token) {

        document.getElementById('loader').style.display = 'block';

        return fetch(apiUrl, {
            method: requestMethod,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
            .then(response => {
                if (!response.ok) {
                    if (response.status === 400) throw new Error("Invalid color code submitted.");
                    if (response.status === 401) throw new Error("User is not authenticated.");
                    if (response.status === 403) throw new Error("Access Forbidden. You don't have permission to access this resource.");
                    if (response.status === 404) throw new Error("User not found in the database.");
                    if (response.status === 422) throw new Error("One or more mandatory fields are not submitted.");
                    if (response.status === 500) throw new Error("Server error. Pleace try again later.");
                }
                return response.json();
            })
            .catch(error => {
                document.getElementById('loader').style.display = 'none';
                throw error;
            });
    }

