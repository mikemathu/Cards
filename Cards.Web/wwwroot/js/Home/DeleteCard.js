import { setEndpointAndToken } from "../Shared/common.js";
import { makeRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";
import { filterDataOptions } from "./Dashboard.js";


export function deleteCard(cardId, cardName) {

    var { token, apiUrl, } = setEndpointAndToken(cardId);

    if (apiUrl.endsWith('?')) {
        apiUrl = apiUrl.slice(0, -1);
    }

    swal({
        title: "Are you sure?",
        text: `Are you sure you want to delete "${cardName}" card?`,
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((confirm) => {
        if (confirm) {

            makeRequest("DELETE", apiUrl, cardId, token)
                .then(data => {
                    showErrorToast("Card Deleted successfully");
                })
                .catch(error => {
                    showErrorToast(error.message);
                });
        }
    });
}