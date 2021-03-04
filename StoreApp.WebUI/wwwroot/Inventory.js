const inventoryTable = document.getElementById("inventory-table");
const editDropdown = document.getElementById("product-edit-select");
const addItemForm = document.getElementById("add-item-form");
const editItemForm = document.getElementById("edit-item-form");
const errorMessage = document.getElementById("error-message");
const successMessage = document.getElementById("success-message");
const storeID = sessionStorage.getItem("storeId");

function loadInventory(storeId) {
    return fetch(`api/storeInventory/${storeId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

function addItem(item, storeId) {
    return fetch(`api/storeInventoryAddItem?product=${item}&storeID=${storeId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
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
        editItemForm.hidden = false;
    })
    .catch(error => {
        errorMessage.textContent = error.toString();
        errorMessage.hidden = false;
    });

addItemForm.addEventListener('submit', event => {
    event.preventDefault();

    successMessage.hidden = true;
    errorMessage.hidden = true;

    const product = {
        name: addItemForm.elements['product-name'].value,
        price: addItemForm.elements['product-price'].value,
        quantity: addItemForm.elements['product-quantity'].value
    };

    addItem(product, storeID).then(() => {
        successMessage.textContent = 'Product added successfully';
        successMessage.hidden = false;
    })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
});