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

function formatDate(date) {
    let hours = date.getHours();
    let minutes = date.getMinutes();
    let amOrPm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; 
    minutes = minutes < 10 ? '0' + minutes : minutes;
    let strTime = hours + ':' + minutes + ' ' + amOrpm;
    return date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear() + " " + strTime;
}
// formatDate(item.orderTime)

function populateOrdersTable(storeId) {
    loadOrders(storeId)
        .then(orders => {
            for (const item of orders) {

                

                const row = ordersTableBody.insertRow();
                row.innerHTML = `<td>${item.id}</td>
                       <td>${item.customerName}</td>
                       <td>\$${item.totalPrice}</td>
                       <td>${item.orderTime}</td>`;
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