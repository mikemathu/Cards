
/*document.addEventListener("DOMContentLoaded", function () {
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

//toggle
const toggleButton = document.getElementById('toggleFilterSection');
const body = document.body;
const filterSection = document.getElementById('left-section');

toggleButton.addEventListener('click', function (event) {
    filterSection.classList.toggle('show');
});

document.addEventListener('click', function (event) {
    const isClickInsideFilterSection = filterSection.contains(event.target) || event.target === toggleButton;

    if (!isClickInsideFilterSection) {
        filterSection.classList.remove('show');
    }
});


    function showLoader() {
        document.getElementById('loader').style.display = 'block';
    }

    function hideLoader() {
        document.getElementById('loader').style.display = 'none';
    }

    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhZG1pbkBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImlzcyI6IkNhcmRzQVBJIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzI2NSJ9.HrHxneJOGF_mjTJRaEpqXwDZEhADKurGtbbiGL2P0D0';


    //const userId = 'kev5f943-112f-4d49-888d-c671e210b8b8';
    const userId = 'admin46d-9e9f-44d3-8425-263ba67509aa';
    const endpoint = `https://localhost:7265/api/appUsers/${userId}/cards/all`;

        fetchData({});


        function fetchData({ pagination = {}, sort = {}, filter = {} }) {
        showLoader();

        let url = `${endpoint}?`;

        // Pagination
        const { pageNumber, pageSize, isSelectedPageSizeDifferentFromDefault } = pagination;
        if (pageNumber) url += `&pageNumber=${pageNumber}`;
        if (pageSize || isSelectedPageSizeDifferentFromDefault) url += `&pageSize=${pageSize}`;

    /*    //if (selectedPageSize) url += `&pageSize=${selectedPageSize}`;


        // Get the select element
        const perPageSelect = document.getElementById('perPageSelect');

        // Get the selected option's value
        const selectedPageSizeValue = perPageSelect.value;

        // Get the value of the default option
        const defaultValue = perPageSelect.options[0].value;

        // Append pageSize to the URL if a different option is selected
        if (selectedPageSizeValue !== defaultValue) {
            url += `&pageSize=${selectedPageSizeValue}`;
        }*/

       /* if (isDifferentFromDefault) {
            url += `&pageSize=${selectedPageSize}`;
        }*/

        // Sorting
        const { sortByString } = sort;
        if (sortByString) url += `&orderBy=${sortByString}`;

        // Filtering
        const { name, color, status, dateOfCreation } = filter;
        if (name) url += `&name=${name}`;
        //if (color) url += `&color=${color}`;
        if (status) url += `&status=${status}`;
        if (dateOfCreation) url += `&dateOfCreation=${dateOfCreation}`;

        // Remove leading '&' if there are query parameters
        if (url.endsWith('?')) {
            url = url.slice(0, -1);
        }

        fetch(url, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }

                // Get pagination data from response headers
                const paginationData = JSON.parse(response.headers.get('X-Pagination'));

                // Update UI with pagination info
                // Calculate 'from' and 'to' dynamically based on pagination data
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
                // Generate HTML for each card object in the data array
                const cardHtml = data.map(card => `
                                <div class="card mb-3" style="background-color: ${card.color}">
                                    <div class="card-body">
                                        <h5 class="card-title">${card.name}</h5>
                                        <p class="card-text">${card.description}</p>
                                        <p class="card-text">Created By: ${card.createdByAppUser}</p>
                                        <p class="card-text">Status: ${card.status}</p>
                                        <p class="card-text">Date of Creation: ${new Date(card.dateOfCreation).toLocaleString()}</p>
                                     <a class="btn btn-warning" href="/Home/EditCard/${card.cardId}" asp-action="EditServicePoint" asp-route-id="@item.Id"><i class="fas fa-pencil-alt"></i></a>
                <a class="btn btn-info" href="/Home/CardDetails/${card.cardId}"><i class="fa-solid fa-circle-info"></i></a>
                <button data-id="${card.cardId}" data-name="${card.name}" class="btn btn-danger deleteBtn" type="button"> <i class="fa-solid fa-trash"></i></button>
                                        </div>
                                </div>
                            `).join('');

                // Insert the generated HTML into the cardContainer element
                document.getElementById('cardContainer').innerHTML = cardHtml;
                hideLoader()

            })
            .catch(error => {
                console.error('There was a problem fetching the data:', error);
                hideLoader()
            });
    }

    // Function to generate pagination links dynamically
    /*function generatePaginationLinks(paginationData) {
        const paginationNav = document.getElementById('paginationNav');
        paginationNav.innerHTML = ''; // Clear existing pagination links


        // Check if there is only one page
        if (paginationData.TotalPages === 1) {
            return; // Exit the function early
        }

        // Add Previous page link if available
        if (paginationData.HasPrevious) {
            paginationNav.innerHTML += `
                    <li class="page-item">
                           <a class="page-link" href="#" onclick="fetchDataCaller({ pagination: { pageNumber: ${paginationData.CurrentPage - 1} } })">Previous</a>
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
                        <a class="page-link" href="#" onclick="fetchDataCaller({ pagination: { pageNumber: 1 } })">1</a>
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
                            <a class="page-link" href="#" onclick="fetchDataCaller({ pagination: { pageNumber: ${i} } })">${i}</a>
                    </li>`;

            paginationNav.innerHTML += `
            <li class="page-item ${pageNumber === paginationData.CurrentPage ? 'active' : ''}">
                <a class="page-link" href="#" onclick="handlePerPageSelectChangeListener(${pageNumber})">${pageNumber}</a>
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
                        <a class="page-link" href="#" onclick="fetchDataCaller({ pagination: { pageNumber: ${paginationData.TotalPages} } })">${paginationData.TotalPages}</a>
                </li>`;


        // Add Next page link if available
        if (paginationData.HasNext) {
            paginationNav.innerHTML += `
                <li class="page-item">
                        <a class="page-link" href="#" onclick="fetchDataCaller({ pagination: { pageNumber: ${paginationData.CurrentPage + 1} } })">Next</a>
                </li>`;
        } else {
            paginationNav.innerHTML += `
                <li class="page-item disabled">
                    <a class="page-link" href="#" tabindex="-1" aria-disabled="true">Next</a>
                </li>`;
        }
    }*/


    function generatePaginationLinks(paginationData) {
        const paginationNav = document.getElementById('paginationNav');
        paginationNav.innerHTML = ''; // Clear existing pagination links

        // Check if there is only one page
        if (paginationData.TotalPages === 1) {
            return; // Exit the function early
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
                <a class="page-link" href="#" onclick="handlePerPageSelectChangeListener(1)">1</a>
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
                    <a class="page-link" href="#" onclick="handlePerPageSelectChangeListener(${i})">${i}</a>
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
                <a class="page-link" href="#" onclick="handlePerPageSelectChangeListener(${paginationData.TotalPages})">${paginationData.TotalPages}</a>
            </li>`;

        // Add Next page link if available
        if (paginationData.HasNext) {
            paginationNav.innerHTML += `
                <li class="page-item">
                    <a class="page-link" href="#" onclick="handlePerPageSelectChangeListener(${paginationData.CurrentPage + 1})">Next</a>
                </li>`;
        } else {
            paginationNav.innerHTML += `
                <li class="page-item disabled">
                    <a class="page-link" href="#" tabindex="-1" aria-disabled="true">Next</a>
                </li>`;
        }
    }


    //pagination
    // Function to handle dropdown change
  /* function handlePerPageSelectChange() {
        const perPagePageSize = document.getElementById('perPageSelect').value;
        return {
            pagination: {
                pageSize: perPagePageSize
            }
        };
    }*/

    function handlePerPageSelectChangeListener(pageNumber) {
        const perPagePageSize = document.getElementById('perPageSelect').value;
        if (pageNumber === 0) {
            fetchDataCaller({ pagination: { pageSize: perPagePageSize } });
        }else{
            fetchDataCaller({ pagination: { pageNumber, pageSize: perPagePageSize } });
        }
    }



   /* function handlePerPageSelectChangeListener() {
        const perPagepageSize = document.getElementById('perPageSelect').value;

        fetchDataCaller({ pagination: { pageSize: perPagepageSize } });
    }*/

    // Function to handle dropdown change and return selected page size and comparison result
    function handlePerPageSelectChange() {
        const perPageSelect = document.getElementById('perPageSelect');
        const selectedPageSize = perPageSelect.value;
        const defaultValue = perPageSelect.options[0].value;
        const isSelectedPageSizeDifferentFromDefault = selectedPageSize !== defaultValue;
        //return { selectedPageSize, isDifferentFromDefault };

        return {
            pagination: {
                pageSize: selectedPageSize,
                isSelectedPageSizeDifferentFromDefault: isSelectedPageSizeDifferentFromDefault
            }
        };
    }

    // Event listener for dropdown change
    //document.getElementById('perPageSelect').addEventListener('change', handlePerPageSelectChangeListener);

    // Event listener for dropdown change
    /*document.getElementById('perPageSelect').addEventListener('change', function (event) {
        const selectedPageNumber = event.target.value; // Get the value of the selected option
        handlePerPageSelectChangeListener(selectedPageNumber); // Call the method with the selected page number
    });*/

    document.getElementById('perPageSelect').addEventListener('change', function (event) {
        const pageNumber = 0; // Set the desired value for pageNumber
        handlePerPageSelectChangeListener(pageNumber);
    });




    
    //sorting
    // Define a function to update the sorting values based on the checked checkboxes
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

    // Attach event listener to checkboxes to update sorting values
    document.querySelectorAll('input[name="sortOption"]').forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            const sortString = updateSortString();
            //fetchDataCaller(sortString);
            fetchDataCaller({ sort: { sortByString: sortString } });
        });
    });

    //Filtring
    function updateFilterOptions() {
        return {
            filter: {
                name: document.getElementById('filterByName').value,
                color: document.getElementById('color-picker').value,
                status: document.getElementById('filterByStatus').value,
                dateOfCreation: document.getElementById('filterByDateOfCreation').value
            }
        };
    }

    // Add event listeners to filter input fields
    document.addEventListener('DOMContentLoaded', () => {
        document.getElementById('filterByName').addEventListener('input', filterDataOptions);
        document.getElementById('color-picker').addEventListener('input', filterDataOptions);
        document.getElementById('filterByStatus').addEventListener('change', filterDataOptions);
        document.getElementById('filterByDateOfCreation').addEventListener('change', filterDataOptions);
    });

    function filterDataOptions() {
        const filterOptions = updateFilterOptions();
        fetchDataCaller(filterOptions);
    }

   /* function fetchDataCaller(options) {
        if (options.pagination === undefined){
            console.log("pagination is undefined");

            options = handlePerPageSelectChange();

            if (options.pagination === undefined) {
                console.log("=> pagination is undefined");
            }
        }
        if (options.sort === undefined) {
            console.log("sort is undefined")

            options = updateSortString();

            if (options.sort === undefined) {
                console.log("=> sort is undefined");
            }
        }
        if (options.filter == undefined) {
            console.log("fileter is undefined")

            options = updateFilterOptions()

            if (options.filter === undefined) {
                console.log("=> fileter is undefined");
            }
        }    
        fetchData(options);
    }*/


    function fetchDataCaller(options) {
        if (options.pagination === undefined) {
            options.pagination = handlePerPageSelectChange().pagination;
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






















    //color picker
    // Get references to the input fields
    const filterByColorInput = document.getElementById('filterByColor');
    const colorPickerInput = document.getElementById('color-picker');

    // Add event listener to color picker input field
    colorPickerInput.addEventListener('input', function () {
        // Update the value of the filterByColor input field with the selected color
        filterByColorInput.value = colorPickerInput.value;
    });




    // Event listener for dropdown change
    document.getElementById('perPageSelect').addEventListener('change', function (event) {
        const perPagepageSize = event.target.value;
        //fetchData({ pagination: { pageSize: pageSize } });
        //handlePerPageSelectChange(pageSize);
        handlePerPageSelectChange({ pagination: { pageSize: perPagepageSize } });
    });




    // Fetch initial data and generate dropdown options when the page loads
    document.addEventListener('DOMContentLoaded', function () {

        showLoader();

        fetch(endpoint, {
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

                hideLoader();

            })
            .catch(error => {
                console.error('There was a problem fetching the initial data:', error);
                hideLoader();
            });
    });

    // Function to generate dropdown options based on pagination data
    function generateDropdownOptions(paginationData) {
        if (!paginationData || !paginationData.TotalCount || !paginationData.PageSize) return;
        const perPageSelect = document.getElementById('perPageSelect');
        const pageCount = Math.ceil(paginationData.TotalCount / paginationData.PageSize);
        for (let i = 1; i <= pageCount; i++) {
            const option = document.createElement('option');
            option.value = paginationData.PageSize * i;
            option.textContent = `Show ${paginationData.PageSize * i}`;
            perPageSelect.appendChild(option);
        }
    }

