using System;
using System.Collections.Generic;

namespace SupermarketCheckout
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define pricing rules for items
            var pricingRules = new List<PricingRule>
            {
                new PricingRule('A', 50, (3, 130)),
                new PricingRule('B', 30, (2, 45)),
                new PricingRule('C', 20, (0, 0)),
                new PricingRule('D', 15, (0, 0))
            };

            // Create a new checkout instance with pricing rules
            var checkout = new Checkout(pricingRules);

            // Scan items
            checkout.Scan('A');
            checkout.Scan('B');
            checkout.Scan('A');
            checkout.Scan('C');
            checkout.Scan('D');
            checkout.Scan('B');
            checkout.Scan('A');

            // Calculate and display the total price
            int totalPrice = checkout.CalculateTotalPrice();
            Console.WriteLine($"Total Price: {totalPrice} pence");
        }
    }
}

//A simple console application with a Main function is constructed in this Program.cs file.
// It establishes item pricing rules, creates a Checkout instance, scans many products, 
//calculates the total price using the CalculateTotalPrice function, and displays the total price.
