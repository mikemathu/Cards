import { setEndpointAndToken } from "../Shared/Home/common.js";
import { showErrorToast } from "../Shared/Home/common.js";
import { fetchCardDetails } from "./CardDetails.js";
import { fetchCardDetailsForEditing } from "./EditCard.js";
import { deleteCard } from "./DeleteCard.js";

const baseURL = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;


fetchData({});


/* ====================================================================

                             Fet API Get Request

======================================================================= */
function fetchData({ pagination = {}, sort = {}, filter = {} }) {
    document.getElementById('loader').style.display = 'block';

    var { token, apiUrl, } = setEndpointAndToken();

    apiUrl += constructQueryParams(pagination, sort, filter);

    if (apiUrl.endsWith('?')) {
        apiUrl = apiUrl.slice(0, -1);
    }

    fetch(apiUrl, {
        method: 'GET',
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
        // Get pagination data from response headers
        const paginationData = JSON.parse(response.headers.get('X-Pagination'));
  
        //Calculate 'from' and 'to' dynamically based on pagination data
        const { CurrentPage, PageSize, TotalCount } = paginationData;
        const from = (CurrentPage - 1) * PageSize + 1;
        const to = Math.min(CurrentPage * PageSize, TotalCount);
  
        // Update UI with pagination info
        const paginationInfoElement = document.getElementById('paginationInfo');
        paginationInfoElement.textContent = `Showing ${from}-${to} of ${TotalCount} cards`;

        // Dynamically generate dropdown options based on pagination data
        generatePaginationLinks(paginationData);
  
        return response.json();
    })
        .then(data => {
            if (data.length === 0) {
                document.getElementById('cardContainer').innerHTML = 'No data to show';

                document.getElementById('cardContainer').classList.add('text-alignCentre');
                document.getElementById('paginationInfo').classList.add('cs-hidden');
                document.getElementById('paginationNav').classList.add('cs-hidden');

                document.getElementById('loader').style.display = 'none';  
                return;
            }
        // Generate HTML for each card object in the data array
        const cardHtml = data.map(card => `
                            <div class="card mb-3" id="card" style="background-color: ${card.color}">
                                <div class="card-body">
                                    <h5 class="card-title">${card.name}</h5>
                                    <p class="card-text">Status: ${card.status}</p>
                                    <p class="card-text">Date of Creation: ${new Date(card.dateOfCreation).toLocaleString()}</p>

                                    <a class="btn btn-warning fas fa-pencil-alt"  data-id="${card.cardId}"></a>
                                    <a class="btn btn-info fa-solid fa-circle-info" data-id="${card.cardId}" ></a>
                                    <button data-id="${card.cardId}" data-name="${card.name}" class="btn btn-danger deleteBtn fa-solid fa-trash" type="button"></button>
                                </div>
                            </div>
                        `).join('');
  
            //Insert the generated HTML into the cardContainer element
            const cardContainer =  document.getElementById('cardContainer').innerHTML = cardHtml;

            document.getElementById('cardContainer').classList.remove('text-alignCentre');
            document.getElementById('paginationInfo').classList.remove('cs-hidden');
            document.getElementById('paginationNav').classList.remove('cs-hidden');

            document.getElementById('loader').style.display = 'none';  
    })
    .catch(error => {
        document.getElementById('loader').style.display = 'none';
        showErrorToast(error.message);
    }); 
}

function constructQueryParams(pagination, sort, filter) {
    let queryParams = '';

    // Pagination
    const { pageNumber, pageSize, isSelectedPageSizeDifferentFromDefault } = pagination;
    if (pageNumber) queryParams += `&pageNumber=${pageNumber}`;
    if (pageSize || isSelectedPageSizeDifferentFromDefault) queryParams += `&pageSize=${pageSize}`;

    // Sorting
    const { sortByString } = sort;
    if (sortByString) queryParams += `&orderBy=${sortByString}`;

    // Filtering
    const { name, color, status, dateOfCreation } = filter;
    if (name) queryParams += `&name=${name}`;
    if (color) {
        color = encodeURIComponent(color);
        queryParams += `&color=${color}`;
    }
    if (status) queryParams += `&status=${status}`;
    if (dateOfCreation) queryParams += `&dateOfCreation=${dateOfCreation}`;

   

    return queryParams;
}

/* ====================================================================

                             Pagination

======================================================================= */

function generatePaginationLinks(paginationData) {
    const paginationNav = document.getElementById('paginationNav');
    paginationNav.innerHTML = ''; // Clear existing pagination links

    const currentPage = document.getElementById('currentPage');
    currentPage.value = paginationData.CurrentPage;

    // Check if there is only one page
    if (paginationData.TotalPages === 1) {
        return;
    }

    // Add Previous page link if available
    if (paginationData.HasPrevious) {
        paginationNav.innerHTML += `
            <li class="page-item">
                <a class="page-link" href="#" onclick="handlePerPageSelectChangeListener(${paginationData.CurrentPage - 1})">Previous</a>
            </li>`;
    } else {
        paginationNav.innerHTML += `
            <li class="page-item disabled">
                <a class="page-link" href="#" tabindex="-1" aria-disabled="true">Previous</a>
            </li>`;
    }
    // Add first page link
    paginationNav.innerHTML += `
        <li class="page-item ${1 === paginationData.CurrentPage ? 'active' : ''}">
            <a class="page-link" href="#">1</a>
        </li>`;

    // Add ellipsis if needed
    if (paginationData.CurrentPage > 4) {
        paginationNav.innerHTML += `
            <li class="page-item disabled">
                <a class="page-link" href="#" tabindex="-1" aria-disabled="true">...</a>
            </li>`;
    }
    // Add sequential pages
    for (let i = Math.max(2, paginationData.CurrentPage - 2); i <= Math.min(paginationData.TotalPages - 1, paginationData.CurrentPage + 2); i++) {
        paginationNav.innerHTML += `
            <li class="page-item ${i === paginationData.CurrentPage ? 'active' : ''}">
                <a class="page-link" href="#" >${i}</a>
            </li>`;
    }
    // Add ellipsis on the right side if needed
    if (paginationData.TotalPages - paginationData.CurrentPage > 3) {
        paginationNav.innerHTML += `
            <li class="page-item disabled">
                <a class="page-link" href="#" tabindex="-1" aria-disabled="true">...</a>
            </li>`;
    }
    // Add last page link
    paginationNav.innerHTML += `
        <li class="page-item ${paginationData.TotalPages === paginationData.CurrentPage ? 'active' : ''}">
            <a class="page-link" href="#" >${paginationData.TotalPages}</a>
        </li>`;

    // Add Next page link if available
    if (paginationData.HasNext) {
        paginationNav.innerHTML += `
        <li class="page-item">
            <a class="page-link" href="#">Next</a>
        </li>`;
    } else {
        paginationNav.innerHTML += `
        <li class="page-item disabled">
            <a class="page-link" href="#" tabindex="-1" aria-disabled="true">Next</a>
        </li>`;
    }
}

const perPageSelect = document.getElementById('perPageSelect');

if (perPageSelect !== null) {
    perPageSelect.addEventListener('change', function (event) {
        handlePerPageSelectChangeListener();
    });
}


function handlePerPageSelectChangeListener() {
    const perPagePageSize = document.getElementById('perPageSelect').value;
    fetchDataCaller({ pagination: { pageSize: perPagePageSize } });
}

//  Select the paginationNav element
const paginationNav = document.getElementById('paginationNav');

if (paginationNav !== null) {
    paginationNav.addEventListener('click', function (event) {

        //  Check if the clicked element is an anchor tag
        if (event.target.tagName === 'A') {
            event.preventDefault(); 

            var pageNumber;
            const currentPage = document.getElementById('currentPage').value;
    
            if (event.target.textContent === "Next") {
                pageNumber = parseInt(currentPage) + 1;
            } else if (event.target.textContent === "Previous") {
                pageNumber = parseInt(currentPage) - 1;
            } else {
                pageNumber = parseInt(event.target.textContent);
            }
    
            const perPagePageSize = document.getElementById('perPageSelect').value;
            if (pageNumber === 0) {
                fetchDataCaller({ pagination: { pageSize: perPagePageSize } });
            } else {
                fetchDataCaller({ pagination: { pageNumber, pageSize: perPagePageSize } });
            }
        }
    });
}

// Function to handle dropdown change and return selected page size and comparison result
function handlePaginationOnPerPageSelectChange() {
    const perPageSelect = document.getElementById('perPageSelect');
    const selectedPageSize = perPageSelect.value;
    const defaultValue = perPageSelect.options[0].value;
    const isSelectedPageSizeDifferentFromDefault = selectedPageSize !== defaultValue;

    return {
        pagination: {
            pageSize: selectedPageSize,
            isSelectedPageSizeDifferentFromDefault: isSelectedPageSizeDifferentFromDefault
        }
    };
}

// Fetch initial data and generate dropdown options when the page loads
document.addEventListener('DOMContentLoaded', function () {

    document.getElementById('loader').style.display = 'block';

    var { token, apiUrl, } = setEndpointAndToken();

    // Remove leading '?' if there are query parameters
    if (apiUrl.endsWith('?')) {
        apiUrl = apiUrl.slice(0, -1);
    }

    fetch(apiUrl, {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not okk');
        }
        // Get pagination data from response headers
        const paginationData = JSON.parse(response.headers.get('X-Pagination'));

        // Dynamically generate dropdown options based on pagination data
        generateDropdownOptions(paginationData);

        document.getElementById('loader').style.display = 'none';;

    })
    .catch(error => {
        document.getElementById('loader').style.display = 'none';
        showErrorToast(error.message);
    });
});

// Function to generate dropdown options based on pagination data
function generateDropdownOptions(paginationData) {
    if (!paginationData || !paginationData.TotalCount || !paginationData.PageSize) return;
    const perPageSelect = document.getElementById('perPageSelect');
    const pageCount = Math.ceil(paginationData.TotalCount / paginationData.PageSize);

    if (perPageSelect !== null) {
        for (let i = 1; i <= pageCount; i++) {
            const option = document.createElement('option');
            option.value = paginationData.PageSize * i;
            option.textContent = `Show ${paginationData.PageSize * i}`;
            perPageSelect.appendChild(option);
        }
    }
   
}

  
/* ====================================================================

                             Sorting

======================================================================= */

// Attach event listener to checkboxes to update sorting values
document.querySelectorAll('input[name="sortOption"]').forEach(checkbox => {
    checkbox.addEventListener('change', function () {
        const sortString = updateSortString();
        fetchDataCaller({ sort: { sortByString: sortString } });
    });
});

// A function to update the sorting values based on the checked checkboxes
function updateSortString() {
    const options = {
        sort: {
            orderByString: []
        }
    };

    document.querySelectorAll('input[name="sortOption"]:checked').forEach(checkedCheckbox => {
        const value = checkedCheckbox.value;
        const oppositeSortOption = value.endsWith(' desc') ? value.slice(0, -5) : value + ' desc';
        const oppositeIndex = options.sort.orderByString.indexOf(oppositeSortOption);
        if (oppositeIndex !== -1) {
            options.sort.orderByString.splice(oppositeIndex, 1);
        }
        options.sort.orderByString.push(value);
    });

    return options.sort.orderByString.join(',');
}

/* ====================================================================

                         Filtering

======================================================================= */

// Add event listeners to filter input fields
document.addEventListener('DOMContentLoaded', () => {
    const filterByName = document.getElementById('filterByName');
    const colorPicker = document.getElementById('color-picker')
    const filterByStatus = document.getElementById('filterByStatus');
    const filterByDateOfCreation = document.getElementById('filterByDateOfCreation');

    if (filterByName !== null && colorPicker !== null &&
        filterByStatus !== null && filterByDateOfCreation !== null) {
        filterByName.addEventListener('input', filterDataOptions);
        colorPicker.addEventListener('input', filterDataOptions);
        filterByStatus.addEventListener('change', filterDataOptions);
        filterByDateOfCreation.addEventListener('change', filterDataOptions);
        }
});

export function filterDataOptions() {
    const filterOptions = updateFilterOptions();
    fetchDataCaller(filterOptions);
}
export function updateFilterOptions() {
    //color value
    const defaultColor = document.getElementById('color-picker').defaultValue;
    const colorValue = document.getElementById('color-picker').value !== defaultColor ? document.getElementById('color-picker').value : undefined

    //status calue
    const statusValue = document.getElementById('filterByStatus').value !== "Status" ? document.getElementById('filterByStatus').value : undefined;

    return {
        filter: {
            name: document.getElementById('filterByName').value,
            color: colorValue,
            status: statusValue,
            dateOfCreation: document.getElementById('filterByDateOfCreation').value
        }
    };
}

//color picker
// Get references to the input fields
const filterByColorInput = document.getElementById('filterByColor');
const colorPickerInput = document.getElementById('color-picker');

// Add event listener to color picker input field
if (colorPickerInput !== null) {
    colorPickerInput.addEventListener('input', function () {
        // Update the value of the filterByColor input field with the selected color
        filterByColorInput.value = colorPickerInput.value;
    });
}



/* ====================================================================

                             Toggle

======================================================================= */
const toggleButton = document.getElementById('toggleFilterSection');
const filterSection = document.getElementById('left-section');

if (toggleButton !== null) {
    toggleButton.addEventListener('click', function (event) {
        filterSection.classList.toggle('show');
    });
}

document.addEventListener('click', function (event) {
    const isClickInsideFilterSection = filterSection.contains(event.target) || event.target === toggleButton;

    if (!isClickInsideFilterSection) {
        filterSection.classList.remove('show');
    }
});

/* ====================================================================

                             Shared

======================================================================= */
function fetchDataCaller(options) {
    if (options.pagination === undefined) {
        options.pagination = handlePaginationOnPerPageSelectChange().pagination;
    }
    if (options.sort === undefined) {
        options.sort = updateSortString();
        //options.sort = updateSortString().sort;
    }
    if (options.filter === undefined) {
        options.filter = updateFilterOptions().filter;
    }
    fetchData(options);
}
/* ====================================================================

                Card Edit and Card Details Event Listeners

======================================================================= */

// Select the "Create Card" button
const createCardBtn = document.getElementById('createCardBtn');
createCardBtn.addEventListener('click', createCardBtnClick);


// Function to be called when the "Create Card" button is clicked
export function createCardBtnClick() {
    // Add cs-hidden class to all elements with the class name "dashboard-section"
    const dashboardSections = document.getElementsByClassName('dashboard-section');
    for (let i = 0; i < dashboardSections.length; i++) {
        dashboardSections[i].classList.add('cs-hidden');
    }
    document.getElementById('card-createform').classList.remove('cs-hidden');

    // Construct the create card URL
    const createCardUrl = `${baseURL}/Home/CreateCard`;


    // Update the URL in the address bar
    window.history.pushState({ path: createCardUrl }, '', createCardUrl);
}


//edit, view card and delete card event listeners
const cardContainer = document.getElementById('cardContainer');

if (cardContainer !== null) {
    cardContainer.addEventListener('click', function (event) {

        // Check if the clicked element is one of the buttons
        if (event.target.matches('.btn-warning')) {

            const cardId = event.target.getAttribute('data-id');
            fetchCardDetailsForEditing(cardId);

        } else if (event.target.matches('.btn-info')) {

            const cardId = event.target.getAttribute('data-id');
            fetchCardDetails(cardId);

        } else if (event.target.matches('.deleteBtn')) {

            const cardId = event.target.getAttribute('data-id');
            const cardName = event.target.getAttribute('data-name');
            deleteCard(cardId, cardName);

        }
    });
}

//logout
const logoutBtn = document.getElementById('logoutBtn');

if (logoutBtn !== null) {
    logoutBtn.addEventListener('click', logout);
}
function logout() {
    localStorage.removeItem('token');

    window.location.href = '/Auth/Login';
}