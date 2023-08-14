using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SupermarketCheckout.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        private List<PricingRule> _pricingRules;

        [SetUp]
        public void Setup()
        {
            // Arrange: Set up pricing rules for testing
            _pricingRules = new List<PricingRule>
            {
                new PricingRule('A', 50, (3, 130)),
                new PricingRule('B', 30, (2, 45)),
                new PricingRule('C', 20, (0, 0)),
                new PricingRule('D', 15, (0, 0))
            };
        }

        // Test: Calculating total price for an empty cart should return zero
        [Test]
        public void CalculateTotalPrice_EmptyCart_ReturnsZero()
        {
            // Arrange: Create a new checkout instance with pricing rules
            var checkout = new Checkout(_pricingRules);

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure the total price is zero for an empty cart
            Assert.AreEqual(0, total);
        }

        // Test: Calculating total price for a single item without special price should return correct total
        [Test]
        public void CalculateTotalPrice_SingleItem_NoSpecialPrice_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan an item
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure the total price is calculated correctly
            Assert.AreEqual(50, total);
        }

        // Test: Calculating total price for multiple items without special price should return correct total
        [Test]
        public void CalculateTotalPrice_MultipleItems_NoSpecialPrice_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan multiple items
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');
            checkout.Scan('B');
            checkout.Scan('C');
            checkout.Scan('D');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure the total price is calculated correctly
            Assert.AreEqual(115, total);
        }

        // Test: Calculating total price for a single item with special price applied should return correct total
        [Test]
        public void CalculateTotalPrice_SingleItem_SpecialPriceApplied_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan items for special price
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('A');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure the special price is applied correctly
            Assert.AreEqual(130, total);
        }

        // Test: Calculating total price for multiple items with mixed special prices should return correct total
        [Test]
        public void CalculateTotalPrice_MultipleItems_MixedSpecialPrices_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan items with mixed special prices
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('B');
            checkout.Scan('B');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure mixed special prices are calculated correctly
            Assert.AreEqual(175, total);
        }

        // Test: Calculating total price with an invalid item should throw an ArgumentException
        [Test]
        public void CalculateTotalPrice_InvalidItem_ThrowsArgumentException()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan an invalid item
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');
            checkout.Scan('E');

            // Assert: Ensure an ArgumentException is thrown when an invalid item is scanned
            Assert.Throws<ArgumentException>(() => checkout.CalculateTotalPrice());
        }

        // Test: Calculating total price when special price is not applicable should return correct total
        [Test]
        public void CalculateTotalPrice_SpecialPriceNotApplicable_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan items without reaching special price
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('B');
            checkout.Scan('B');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure regular pricing is applied correctly
            Assert.AreEqual(60, total);
        }

        // Test: Calculating total price for multiple special prices of the same item should return correct total
        [Test]
        public void CalculateTotalPrice_MultipleSpecialsForSameItem_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan multiple items for the same special price
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('A');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure multiple special prices for the same item are applied correctly
            Assert.AreEqual(180, total);
        }

        // Test: Calculating total price for multiple special prices of different items should return correct total
        [Test]
        public void CalculateTotalPrice_MultipleSpecialsForDifferentItems_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan items for different special prices
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('B');
            checkout.Scan('B');
            checkout.Scan('C');
            checkout.Scan('C');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure multiple special prices for different items are applied correctly
            Assert.AreEqual(165, total);
        }

        // Test: Calculating total price for a mix of single items and multiple special prices should return correct total
        [Test]
        public void CalculateTotalPrice_MultipleSpecialsAndSingleItems_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan a mix of items
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('B');
            checkout.Scan('B');
            checkout.Scan('B');
            checkout.Scan('C');
            checkout.Scan('D');
            checkout.Scan('D');
            checkout.Scan('D');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure a mix of special prices and regular prices are calculated correctly
            Assert.AreEqual(245, total);
        }

        // Test: Calculating total price for multiple items with various quantities and special prices
        [Test]
        public void CalculateTotalPrice_MultipleItems_MultipleQuantitiesAndSpecialPrices_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan items with mixed quantities and special prices
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A'); // Price: 50
            checkout.Scan('A'); // Price: 50
            checkout.Scan('A'); // Price: 130 (special price)
            checkout.Scan('B'); // Price: 30
            checkout.Scan('B'); // Price: 45 (special price)
            checkout.Scan('B'); // Price: 45 (special price)
            checkout.Scan('B'); // Price: 30
            checkout.Scan('C'); // Price: 20
            checkout.Scan('D'); // Price: 15
            checkout.Scan('D'); // Price: 30 (two for 45)
            checkout.Scan('D'); // Price: 30 (two for 45)

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure various quantities and special prices are applied correctly
            Assert.AreEqual(465, total);
        }

        // Test: Calculating total price for items with no special prices
        [Test]
        public void CalculateTotalPrice_NoSpecialPrices_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan items without special prices
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('C');
            checkout.Scan('C');
            checkout.Scan('C');
            checkout.Scan('D');
            checkout.Scan('D');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure regular prices are applied correctly
            Assert.AreEqual(85, total);
        }

        // Test: Calculating total price for items with only special prices
        [Test]
        public void CalculateTotalPrice_OnlySpecialPrices_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan items with only special prices
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('B');
            checkout.Scan('B');
            checkout.Scan('B');

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure only special prices are applied correctly
            Assert.AreEqual(175, total);
        }

        // Test: Calculating total price with large quantities and special prices
        [Test]
        public void CalculateTotalPrice_LargeQuantitiesAndSpecialPrices_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan large quantities of items with special prices
            var checkout = new Checkout(_pricingRules);
            for (int i = 0; i < 1000; i++)
            {
                checkout.Scan('A'); // Price: 130 (special price)
                checkout.Scan('B'); // Price: 45 (special price)
                checkout.Scan('C'); // Price: 20
            }

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure large quantities and special prices are calculated correctly
            Assert.AreEqual(195500, total);
        }

        // Test: Calculating total price with various items and no pricing rules
        [Test]
        public void CalculateTotalPrice_NoPricingRules_ThrowsArgumentException()
        {
            // Arrange: Create a new checkout instance with no pricing rules and scan items
            var checkout = new Checkout(new List<PricingRule>());
            checkout.Scan('A');
            checkout.Scan('B');

            // Assert: Ensure an ArgumentException is thrown when pricing rules are missing
            Assert.Throws<ArgumentException>(() => checkout.CalculateTotalPrice());
        }

        // Test: Calculating total price with multiple special price groups
        [Test]
        public void CalculateTotalPrice_MultipleSpecialPriceGroups_ReturnsCorrectTotal()
        {
            // Arrange: Create a new checkout instance with pricing rules and scan items for multiple special price groups
            var checkout = new Checkout(_pricingRules);
            checkout.Scan('A'); // Price: 50
            checkout.Scan('A'); // Price: 50
            checkout.Scan('A'); // Price: 130 (special price)
            checkout.Scan('A'); // Price: 50
            checkout.Scan('A'); // Price: 50
            checkout.Scan('A'); // Price: 130 (special price)

            // Act: Calculate the total price
            int total = checkout.CalculateTotalPrice();

            // Assert: Ensure multiple special price groups are applied correctly
            Assert.AreEqual(410, total);
        }
    }
}
