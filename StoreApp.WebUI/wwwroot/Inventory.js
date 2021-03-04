const inventoryTable = document.getElementById("inventory-table");
const editDropdown = document.getElementById("product-edit-select");
const editForm = document.getElementById("edit-item-form");
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
            const option = document.createElement("option");
            row.innerHTML = `<td>${item.id}</td>
                       <td>${item.name}</td>
                       <td>\$${item.price}</td>
                       <td>${item.quantity}</td>`;
            option.value = item.id;
            option.text = item.name;
            editDropdown.appendChild(option);
        }
        inventoryTable.hidden = false;
        editForm.hidden = false;
    })
    .catch(error => {
        errorMessage.textContent = error.toString();
        errorMessage.hidden = false;
    });