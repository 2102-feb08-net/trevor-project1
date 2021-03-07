const inventoryTable = document.getElementById("inventory-table");
const inventoryTableBody = document.getElementById("inventory-table-body");
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

function clearTableBody(tableBody) {
    while (tableBody.firstChild) {
        tableBody.removeChild(tableBody.firstChild);
    }
}

function addItem(item, storeId) {
    return fetch(`api/storeInventoryAddItem/${storeId}?productName=${item.name}&productPrice=${item.price}&quantity=${item.quantity}`, {
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

function editQuantity(item, storeId) {
    return fetch(`api/storeInventoryEditItem/${storeId}?productID=${item.id}&newQuantity=${item.quantity}`, {
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

function populateTable(storeId) {
    loadInventory(storeId)
        .then(inventory => {
            for (const item of inventory) {
                const row = inventoryTableBody.insertRow();
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
}

populateTable(storeID);

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
        clearTableBody(inventoryTableBody);
        clearTableBody(editDropdown);
        populateTable(storeID);
    })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
});

editItemForm.addEventListener('submit', event => {
    event.preventDefault();

    successMessage.hidden = true;
    errorMessage.hidden = true;

    const product = {
        id: editItemForm.elements['product-edit-select'].value,
        quantity: editItemForm.elements['product-new-quantity'].value
    };

    editQuantity(product, storeID).then(() => {
        successMessage.textContent = 'Product quantity updated successfully';
        successMessage.hidden = false;
        clearTableBody(inventoryTableBody);
        clearTableBody(editDropdown);
        populateTable(storeID);
    })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
});