const storesTable = document.getElementById("stores-table");

function loadStores() {
    return fetch('/api/stores').then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

loadStores()
    .then(stores => {
        for (const store of stores) {
            const row = storesTable.insertRow();
            row.innerHTML = `<td>${store.id}</td>
                       <td>${store.name}</td>
                       <td>${store.city}</td>
                       <td>${store.state}</td>`;
            row.addEventListener('click', () => {
                sessionStorage.setItem('storeId', store.id);
                location = 'StoreDetails.html';
            });
        }

        storesTable.hidden = false;
    })
    .catch(error => {
        errorMessage.textContent = error.toString();
        errorMessage.hidden = false;
    });