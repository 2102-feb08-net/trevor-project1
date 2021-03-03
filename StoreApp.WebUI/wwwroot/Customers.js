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
            const row = customerTable.insertRow();
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