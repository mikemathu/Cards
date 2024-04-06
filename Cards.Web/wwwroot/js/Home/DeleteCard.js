import { generateUrlWithUserId } from "../Shared/common.js";
import { makePostRequest } from "../Shared/common.js";
import { showErrorToast } from "../Shared/common.js";


document.addEventListener("DOMContentLoaded", function () {

    var deleteBtns = document.querySelectorAll('.deleteBtn');

    deleteBtns.forEach(function (deleteBtn) {
        deleteBtn.addEventListener('click', function (e) {
        // Get the name attribute value
        var cardName = deleteBtn.getAttribute("data-name");

        swal({
            title: "Are you sure?",
            text: `Are you sure you want to delete "${cardName}"?`, // Display the card name in the message
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((confirm) => {
            if (confirm) {
                var cardId = deleteBtn.getAttribute("data-id");
                document.getElementById('cardId').value = cardId;
                //document.getElementById('cardDeleteForm').submit();

                const token = sessionStorage.getItem('token');

                const apiUrl = generateUrlWithUserId(cardId, token);

                makePostRequest("DELETE", apiUrl, '', token)
                    .then(data => {
                        console.log('DELETE request successful');
                    })
                    .catch(error => {
                        showErrorToast(error.message);
                    });        
            }
        });
      });
  });
});