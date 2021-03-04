const welcomeHeader = document.getElementById("welcome-header");
const storeProfit = document.getElementById("store-profit");
const errorMessage = document.getElementById("error-message");
const storeID = sessionStorage.getItem('storeId');

function loadStore(storeId) {
    return fetch(`api/stores/${storeId}`).then(response => {
        if (!response.ok) {
            throw new Error(`Network response was not ok (${response.status})`);
        }
        return response.json();
    });
}

loadStore(storeID)
    .then(store => {
        welcomeHeader.innerHTML = `Welcome to ${store.name} located in ${store.city}, ${store.state}`;
        welcomeHeader.hidden = false;
        storeProfit.innerHTML = `Total store profit: \$${store.grossProfit}`;
        storeProfit.hidden = false;
    })
    .catch(error => {
        errorMessage.textContent = error.toString();
        errorMessage.hidden = false;
    });