const ordersTable = document.getElementById("customer-orders-table");
const customerID = sessionStorage.getItem("customerId");

function loadOrders(customerId) {
    return fetch(`api/ordersByCustomer/${customerId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

function populateOrdersTable(customerId) {
    loadOrders(customerId)
        .then(orders => {
            for (const order of orders) {
                const row = ordersTable.insertRow();
                row.innerHTML = `<td>${order.id}</td>
                       <td>${order.storeName}</td>
                       <td>${order.storeLocation}</td>
                       <td>\$${order.totalPrice}</td>
                       <td>${order.orderTime}</td>`;
                row.addEventListener('click', () => {
                    sessionStorage.setItem('orderId', order.id);
                    location = 'OrderDetails.html';
                });
            }
            ordersTable.hidden = false;
        })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
}

populateOrdersTable(customerID);