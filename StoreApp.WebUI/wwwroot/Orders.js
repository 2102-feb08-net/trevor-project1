const ordersTable = document.getElementById("orders-table");
const ordersTableBody = document.getElementById("orders-table-body");
const storeID = sessionStorage.getItem("storeId");

function loadOrders(storeId) {
    return fetch(`api/ordersByStore/${storeId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

function formatDateTime(dateTime) {
    let date = new Date(Date.parse(dateTime));
    return date.toUTCString();
}

function populateOrdersTable(storeId) {
    loadOrders(storeId)
        .then(orders => {
            for (const item of orders) {
                const row = ordersTableBody.insertRow();
                let orderTime = formatDateTime(item.orderTime);
                row.innerHTML = `<td>${item.id}</td>
                       <td>${item.customerName}</td>
                       <td>\$${item.totalPrice}</td>
                       <td>${orderTime}</td>`;
                row.addEventListener('click', () => {
                    sessionStorage.setItem('orderId', item.id);
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

populateOrdersTable(storeID);