const customerTable = document.getElementById("customer-table");
const customerTableBody = document.getElementById("customer-table-body");
const customerSearchTable = document.getElementById("customer-search-table");
const customerSearchTableBody = document.getElementById("customer-search-table-body");
const searchForm = document.getElementById("customer-search");
const addCustomerForm = document.getElementById("add-customer-form");
const errorMessage = document.getElementById("error-message");
const successMessage = document.getElementById("success-message");

function loadCustomers() {
    return fetch('/api/customers').then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

function addCustomer(customer) {
    return fetch(`api/customerAdd`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(customer)
    }).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
    });
}

function searchCustomers(firstName, lastName) {
    return fetch(`api/customers?firstName=${firstName}&lastName=${lastName}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

function resetTableBody(tableBody) {
    while (tableBody.firstChild) {
        tableBody.removeChild(tableBody.firstChild);
    }
}

function populateCustomersTable() {
    loadCustomers()
        .then(customers => {
            for (const customer of customers) {
                const row = customerTableBody.insertRow();
                row.innerHTML = `<td>${customer.id}</td>
                       <td>${customer.firstName}</td>
                       <td>${customer.lastName}</td>
                       <td>${customer.email}</td>
                       <td>${customer.address}</td>`;
                row.addEventListener('click', () => {
                    sessionStorage.setItem('customerId', customer.id);
                    location = 'CustomerDetails.html';
                });
            }

            customerTable.hidden = false;
        })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
}

populateCustomersTable();

searchForm.addEventListener("submit", event => {
    event.preventDefault();
    resetTableBody(customerSearchTableBody);
    const firstName = searchForm.elements["first-name"].value;
    const lastName = searchForm.elements["last-name"].value;

    searchCustomers(firstName, lastName).then(customers => {
        for (const customer of customers) {
            const row = customerSearchTableBody.insertRow();
            row.innerHTML = `<td>${customer.id}</td>
                       <td>${customer.firstName}</td>
                       <td>${customer.lastName}</td>
                       <td>${customer.email}</td>
                       <td>${customer.address}</td>`;
            row.addEventListener('click', () => {
                sessionStorage.setItem('customerId', customer.id);
                location = 'CustomerDetails.html';
            });
        }

        customerTable.hidden = true;
        customerSearchTable.hidden = false;
    })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
});

searchForm.addEventListener("reset", event => {
    event.preventDefault();

    searchForm.elements["first-name"].value = "";
    searchForm.elements["last-name"].value = "";
    resetTableBody(customerSearchTableBody);
    customerSearchTable.hidden = true;
    customerTable.hidden = false;
});

addCustomerForm.addEventListener("submit", event => {
    event.preventDefault();

    successMessage.hidden = true;
    errorMessage.hidden = true;

    const customer = {
        firstName: addCustomerForm.elements["first-name"].value,
        lastName: addCustomerForm.elements["last-name"].value,
        email: addCustomerForm.elements["email"].value,
        address: addCustomerForm.elements["address"].value,
    }

    addCustomer(customer).then(() => {
        successMessage.textContent = 'Customer added successfully';
        successMessage.hidden = false;
        resetTableBody(customerTableBody);
        populateCustomersTable();
    })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
});