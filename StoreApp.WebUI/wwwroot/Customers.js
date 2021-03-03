const customerTable = document.getElementById("customer-table");

function loadCustomers() {
    return fetch('/api/customers').then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

loadCustomers()
    .then(customers => {
        for (const customer of customers) {
            // from | subject | received
            const row = customerTable.insertRow(); // returns a <tr>
            row.innerHTML = `<td>${customer.id}</td>
                       <td>${customer.firstName}</td>
                       <td>${customer.lastName}</td>
                       <td>${customer.email}</td>
                       <td>${customer.address}</td>`;
            row.addEventListener('click', () => {
                sessionStorage.setItem('customerId', customer.id);
                location = 'details.html';
            });
        }

        customerTable.hidden = false;
    })
    .catch(error => {
        errorMessage.textContent = error.toString();
        errorMessage.hidden = false;
    });