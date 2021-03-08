using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BLL.Models;

namespace Tests
{
    public class StoreTests
    {

        [Fact]
        public void TestStoreConstructor()
        {
            string name = "test";
            string city = "Annapolis";
            string state = "Maryland";

            Store s = new Store(name, city, state);

            Assert.Equal(name, s.Name);
            Assert.Equal(city, s.City);
            Assert.Equal(state, s.State);
            Assert.Equal(0, s.GrossProfit);
            Assert.Empty(s.Inventory);
        }

        [Fact]
        public void TestStoreInvalidCity()
        {
            string name = "test";
            string city = "Annapo123#lis";
            string state = "Maryland";

            Assert.Throws<ArgumentException>(() => new Store(name, city, state));
        }

        [Fact]
        public void TestStoreInvalidState()
        {
            string name = "test";
            string city = "Annapolis";
            string state = "Mar234%yland";

            Assert.Throws<ArgumentException>(() => new Store(name, city, state));
        }

        [Fact]
        public void TestStoreAddNewItem()
        {
            Product p = new Product("testproduct", 0.5M);
            Store s = new Store("testStore", "Annapolis", "Maryland");

            s.AddNewItemToInventory(p, 1);

            Assert.Equal(1, s.Inventory[p]);
        }

        [Fact]
        public void TestStoreAddDuplicateItem()
        {
            Product p = new Product("testproduct", 0.5M);
            Store s = new Store("testStore", "Annapolis", "Maryland");

            s.AddNewItemToInventory(p, 1);

            Assert.Throws<ArgumentException>(() => s.AddNewItemToInventory(p, 1));
        }

        [Fact]
        public void TestStoreAddItemWithNegativeQuantity()
        {
            Product p = new Product("testproduct", 0.5M);
            Store s = new Store("testStore", "Annapolis", "Maryland");

            Assert.Throws<ArgumentException>(() => s.AddNewItemToInventory(p, -1));
        }

        [Fact]
        public void TestStoreEditItemQuantity()
        {
            Product p = new Product("testproduct", 0.5M);
            Store s = new Store("testStore", "Annapolis", "Maryland");

            s.AddNewItemToInventory(p, 1);
            s.EditQuantityToItem(p, 5);

            Assert.Equal(5, s.Inventory[p]);
        }

        [Fact]
        public void TestStoreEditItemQuantityNegativeValue()
        {
            Product p = new Product("testproduct", 0.5M);
            Store s = new Store("testStore", "Annapolis", "Maryland");

            s.AddNewItemToInventory(p, 1);

            Assert.Throws<ArgumentException>(() => s.EditQuantityToItem(p, -1));
        }

        [Fact]
        public void TestStoreEditItemQuantityItemDoesntExist()
        {
            Product p = new Product("testproduct", 0.5M);
            Store s = new Store("testStore", "Annapolis", "Maryland");

            Product notInInventory = new Product("DNE", 0.5M);
            s.AddNewItemToInventory(p, 1);

            Assert.Throws<ArgumentException>(() => s.EditQuantityToItem(notInInventory, -1));
        }
    }
}
