const newOrderForm = document.getElementById("new-order-form");
const newProductButton = document.getElementById("new-product-button");
const customerSelector = document.getElementById("customer-select-dropdown");
const itemsContainer = document.getElementById("items-container");
const submitOrderButton = document.getElementById("submit-button");
const storeID = sessionStorage.getItem("storeId");
const errorMessage = document.getElementById("error-message");
const successMessage = document.getElementById("success-message");

let productLines = 1;

class Product {
	constructor(id, quantity) {
		this.id = id;
		this.quantity = quantity;
	}
}

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

function loadProducts(storeId) {
	return fetch(`api/storeInventory/${storeId}`).then(response => {
		if (!response.ok) {
			throw new Error(`Network response was not ok (${response.status})`);
		}
		return response.json();
	});
}

function populateProductDropdown(productLine, storeId) {
	loadProducts(storeId).then(items => {
		const line = document.getElementById(`product-select-dropdown-${productLine}`);
		for (const item of items) {
			const option = document.createElement("option");
			option.value = item.id;
			option.text = item.name;
			line.appendChild(option);
		}
	})
		.catch(error => {
			errorMessage.textContent = error.toString();
			errorMessage.hidden = false;
		});
}

function submitOrder(order) {
	return fetch(`api/orderAdd`, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(order)
	}).then(response => {
		if (!response.ok) {
			throw new Error(`Network response was not ok (${response.status})`);
		}
	});
}

	populateCustomersDropdown();
	populateProductDropdown(productLines, storeID);

	newProductButton.addEventListener("click", event => {
		event.preventDefault();
		productLines = productLines + 1;
		const productLabel = document.createElement("label");
		const quantityLabel = document.createElement("label");
		const productSelector = document.createElement("select");
		const newLine = document.createElement("br");

		productSelector.id = `product-select-dropdown-${productLines}`;
		productSelector.name = `product-${productLines}`;

		productLabel.innerHTML = `Product: `;
		quantityLabel.innerHTML = ` Quantity: <input type="number" min="1" name="product-quantity-${productLines}" placeholder="Quantity" />`;

		productLabel.appendChild(productSelector);

		itemsContainer.appendChild(productLabel);
		itemsContainer.appendChild(quantityLabel);
		itemsContainer.appendChild(newLine);
		populateProductDropdown(productLines, storeID);
	});

	newOrderForm.addEventListener("submit", event => {
		event.preventDefault();

		successMessage.hidden = true;
		errorMessage.hidden = true;

		let products = [];
		for (i = 1; i <= productLines; i++) {
			const productChoice = newOrderForm.elements[`product-${i}`].value;
			const productQuantity = newOrderForm.elements[`product-quantity-${i}`].value
			products.push(new Product(productChoice, productQuantity));
		}

		const customerID = newOrderForm.elements[`customer`].value;

		const order = {
			storeId: storeID,
			customerId: customerID,
			items: products
		}

		submitOrder(order).then(() => {
			successMessage.textContent = 'Order placed successfully';
			successMessage.hidden = false;
		})
			.catch(error => {
				errorMessage.textContent = error.toString();
				errorMessage.hidden = false;
			});
});