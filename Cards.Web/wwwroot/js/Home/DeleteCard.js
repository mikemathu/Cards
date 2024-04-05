/*document.addEventListener("DOMContentLoaded", function () {
  // Add debugging statement
  console.log("Form submission listener initialized");

  document.getElementById('cardDeleteForm').addEventListener('submit', function (event) {
    event.preventDefault();

    // Add debugging statement
    console.log("Form submitted");

    const cardId = document.getElementById("cardId").value;

    if (!cardId) { // Prevent form submission if cardId is empty or undefined
      console.error("Card ID is missing");
      return;
    }

    const token = sessionStorage.getItem('token');

    const apiUrl = generateUrlWithUserId(cardId, token);

    makePostRequest("DELETE", apiUrl, '', token)
      .then(data => {
        console.log('DELETE request successful');
      })
      .catch(error => {
        showErrorToast(error.message);
      });
  });
});

function generateUrlWithUserId(endpoint, token) {
  const decodedToken = parseJwt(token);

  if (decodedToken && decodedToken.AppUserId) {
    var url = `https://localhost:7265/api/appUsers/${decodedToken.AppUserId}/cards/${endpoint}`;
    return url;
  } else {
    throw new Error('User ID not found in token');
  }
}

function makePostRequest(requestMethod, apiUrl, data, token) {

  document.getElementById('loader').style.display = 'block';

  return fetch(apiUrl, {
    method: requestMethod,
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(data)
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




*/
/*
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
            function parseJwt(token) {
                try {
                    return JSON.parse(atob(token.split('.')[1]));
                } catch (e) {
                    return null;
                }
            }

            function generateUrlWithUserId(endpoint, token) {
                const decodedToken = parseJwt(token);

                if (decodedToken && decodedToken.AppUserId) {
                    var url = `https://localhost:7265/api/appUsers/${decodedToken.AppUserId}/cards/${endpoint}`;
                    return url;
                } else {
                    throw new Error('User ID not found in token');
                }
            }

            function makePostRequest(requestMethod, apiUrl, data, token) {

                document.getElementById('loader').style.display = 'block';

                return fetch(apiUrl, {
                    method: requestMethod,
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
                    },
                    body: JSON.stringify(data)
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
        }
    });
});
});
});*/