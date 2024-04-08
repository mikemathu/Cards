import { setEndpointAndToken } from "../Shared/common.js";
import { makePostRequest } from "../Shared/common.js";
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

            makePostRequest("DELETE", apiUrl, cardId, token)
                .then(data => {
                    console.log('DELETE request successful');
                })
                .catch(error => {
                    showErrorToast(error.message);
                });
        }
    });
}