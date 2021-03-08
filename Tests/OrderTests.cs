using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BLL.Models;

namespace Tests
{
    public class OrderTests
    {
        [Fact]
        public void TestOrderConstructor()
        {
            Customer c = new Customer();
            Store s = new Store();
            Order o = new Order(c, s);

            Assert.Equal(c, o.Customer);
            Assert.Equal(s, o.Store);
            Assert.Equal(0, o.TotalPrice);
            Assert.Empty(o.Cart);
        }

        [Fact]
        public void TestOrderNegativePrice()
        {
            Customer c = new Customer();
            Store s = new Store();
            Order o = new Order(c, s);

            Assert.Throws<ArgumentException>(() => o.TotalPrice = -1);
        }

        [Fact]
        public void TestOrderAddItemToCart()
        {
            Customer c = new Customer();
            Store s = new Store("TestStore", "Annapolis", "MD");
            Order o = new Order(c, s);
            Product p = new Product("Test", 0.5M);
            s.AddNewItemToInventory(p, 1);
            o.AddNewItemToCart(p, 1);

            Assert.Equal(1, o.Cart[p]);
            Assert.Equal(0, o.Store.Inventory[p]);
        }

        [Fact]
        public void TestOrderAddItemToCartItemNotInStore()
        {
            Customer c = new Customer();
            Store s = new Store("TestStore", "Annapolis", "MD");
            Order o = new Order(c, s);
            Product p = new Product("Test", 0.5M);
            
            Assert.Throws<ArgumentException>(() => o.AddNewItemToCart(p, 1));
        }

        [Fact]
        public void TestOrderAddItemToCartItemAlreadyInCart()
        {
            Customer c = new Customer();
            Store s = new Store("TestStore", "Annapolis", "MD");
            Order o = new Order(c, s);
            Product p = new Product("Test", 0.5M);

            s.AddNewItemToInventory(p, 2);
            o.AddNewItemToCart(p, 1);

            Assert.Throws<ArgumentException>(() => o.AddNewItemToCart(p, 1));
        }

        [Fact]
        public void TestOrderAddItemToCartQuantityTooHigh()
        {
            Customer c = new Customer();
            Store s = new Store("TestStore", "Annapolis", "MD");
            Order o = new Order(c, s);
            Product p = new Product("Test", 0.5M);

            s.AddNewItemToInventory(p, 1);

            Assert.Throws<ArgumentException>(() => o.AddNewItemToCart(p, 2));
        }

        [Fact]
        public void TestOrderAddItemToCartQuantityNegative()
        {
            Customer c = new Customer();
            Store s = new Store("TestStore", "Annapolis", "MD");
            Order o = new Order(c, s);
            Product p = new Product("Test", 0.5M);

            s.AddNewItemToInventory(p, 1);

            Assert.Throws<ArgumentException>(() => o.AddNewItemToCart(p, -1));
        }

        [Fact]
        public void TestOrderSubmitOrder()
        {
            Customer c = new Customer();
            Store s = new Store("TestStore", "Annapolis", "MD");
            Order o = new Order(c, s);
            Product p = new Product("Test", 1);

            s.AddNewItemToInventory(p, 1);
            o.AddNewItemToCart(p, 1);
            o.SubmitOrder();

            Assert.Equal(1, o.TotalPrice);
            Assert.Equal(1, o.Store.GrossProfit);
        }
    }
}
