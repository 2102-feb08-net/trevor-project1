const orderDetailsTable = document.getElementById("order-details-table");
const errorMessage = document.getElementById("error-message");
const successMessage = document.getElementById("success-message");
const welcomeHeader = document.getElementById("order-details-header");
const orderTotal = document.getElementById("order-total");
const orderID = sessionStorage.getItem("orderId");

function loadOrderDetails(orderId) {
    return fetch(`api/orders/${orderId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

function populateOrderDetailsTable(orderId) {
    loadOrderDetails(orderId)
        .then(order => {
            for (const item of order) {
                const row = orderDetailsTable.insertRow();
                row.innerHTML = `<td>${item.id}</td>
                       <td>${item.name}</td>
                       <td>\$${item.price}</td>
                       <td>${item.quantity}</td>`;
            }
            orderDetailsTable.hidden = false;
        })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
}

populateOrderDetailsTable(orderID);