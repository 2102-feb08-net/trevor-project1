const storesTable = document.getElementById("stores-table");
const storesTableBody = document.getElementById("stores-table-body");
const addStoreForm = document.getElementById("add-store-form");
const errorMessage = document.getElementById("error-message");
const successMessage = document.getElementById("success-message");

function loadStores() {
    return fetch('/api/stores').then(response => {
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

function addStore(store) {
    return fetch(`api/storeAdd`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(store)
    }).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
    });
}

function populateStoresTable() {
    loadStores()
        .then(stores => {
            for (const store of stores) {
                const row = storesTableBody.insertRow();
                row.innerHTML = `<td>${store.id}</td>
                       <td>${store.name}</td>
                       <td>${store.city}</td>
                       <td>${store.state}</td>`;
                row.addEventListener('click', () => {
                    sessionStorage.setItem('storeId', store.id);
                    location = 'StoreHome.html';
                });
            }

            storesTable.hidden = false;
        })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
}

populateStoresTable();

addStoreForm.addEventListener("submit", event => {
    event.preventDefault();

    errorMessage.hidden = true;
    successMessage.hidden = true;

    const store = {
        name: addStoreForm.elements["name"].value,
        city: addStoreForm.elements["city"].value,
        state: addStoreForm.elements["state"].value
    }

    addStore(store).then(() => {
        successMessage.textContent = 'Store added successfully';
        successMessage.hidden = false;
        clearTableBody(storesTableBody);
        populateStoresTable();
    })
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
});