export function makeRequest(requestMethod, apiUrl, data, requestType) {

    document.getElementById('loader').classList.remove('hide-loader');

    return fetch(apiUrl, {
        method: requestMethod,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (!response.ok) {
                if (response.status === 400) throw new Error("Enter strong password");
                if (response.status === 401) throw new Error("Invalid Username or Password.");
                if (response.status === 409) throw new Error("Email already exists.");
                if (response.status === 422) throw new Error("One or more mandatory fields are not submitted.");
                if (response.status === 500) throw new Error("Server error. Pleace try again later.");
            }

            document.getElementById('loader').classList.add('hide-loader');

            if (requestType === "Login") {
                return response.json();
            }

        })
        .catch(error => {
            document.getElementById('loader').classList.add('hide-loader');
            throw error;
        });
}

export function showErrorToast(message) {
    var errorToast = document.getElementById('errorToast');
    var errorMessage = document.getElementById('errorMessage');

    errorMessage.innerText = message;

    var toast = new bootstrap.Toast(errorToast);
    toast.show();
}