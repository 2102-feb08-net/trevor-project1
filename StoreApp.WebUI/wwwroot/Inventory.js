const inventoryTable = document.getElementById("inventory-table");
const errorMessage = document.getElementById("error-message");
const storeID = sessionStorage.getItem("storeId");

function loadInventory(storeId) {
    return fetch(`api/storeInventory/${storeId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

loadInventory(storeID)
    .then(inventory => {
        for (const item of inventory) {
            const row = inventoryTable.insertRow();
            row.innerHTML = `<td>${item.id}</td>
                       <td>${item.name}</td>
                       <td>${item.price}</td>
                       <td>${item.quantity}</td>`;
        }
        inventoryTable.hidden = false;
    })
    .catch(error => {
        errorMessage.textContent = error.toString();
        errorMessage.hidden = false;
    });