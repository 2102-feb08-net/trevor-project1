const newOrderForm = document.getElementById("new-order-form");
const newProductButton = document.getElementById("new-product-button");
const customerSelector = document.getElementById("customer-select-dropdown");

function loadCustomers() {
	return fetch('/api/customers').then(response => {
		if (!response.ok) {
			throw new Error(`Network response was not ok (${response.status})`);
		}
		return response.json();
	});
}

function populateCustomersDropdown() {
	loadCustomers().then(customers => {
		for (const customer of customers) {
			const option = document.createElement("option");
			option.value = customer.id;
			option.text = customer.firstName + " " + customer.lastName;
			customerSelector.appendChild(option);
		}
	})
        .catch(error => {
            errorMessage.textContent = error.toString();
            errorMessage.hidden = false;
        });
}

populateCustomersDropdown();


newProductButton.addEventListener("click", event => {
	event.preventDefault();

	const productLabel = document.createElement("label");
	const quantityLabel = document.createElement("label");
	const newLine = document.createElement("br");

	productLabel.innerHTML = `Product:
            <select id="product-select-dropdown">
            </select>`;
	quantityLabel.innerHTML = ` Quantity:
	<input type="number" min="1" name="product-quantity" placeholder="Quantity" />`;

	newOrderForm.insertBefore(productLabel, newProductButton);
	newOrderForm.insertBefore(quantityLabel, newProductButton);
	newOrderForm.insertBefore(newLine, newProductButton);
});