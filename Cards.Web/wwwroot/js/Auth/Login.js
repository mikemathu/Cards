// Fetch API code
const apiUrl = 'https://localhost:7265/api/authentication/login';

// Function to handle form submission
async function handleFormSubmit(event) {
    event.preventDefault(); // Prevent the default form submission

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // Form data to be sent in the request body
    const formData = {
        email: email,
        password: password,
    };

    try {
        // Make a POST request using the Fetch API
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(formData),
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        // Process the response data
        const userData = await response.json();

        localStorage.setItem('token', userData.token);
        sessionStorage.setItem('token', userData.token);

        // Redirect the user to a logged-in area or perform any other necessary actions
        window.location.href = "/Home/Dashboard"; // Redirect to the dashboard

    } catch (error) {
        console.error('Error:', error);
    }
}

// Add event listener to the form submission event
document.getElementById('loginForm').addEventListener('submit', handleFormSubmit);

