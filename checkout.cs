using System;
using System.Collections.Generic;

// Represents the pricing information for an item
public class PricingRule
{
    public char Item { get; } // Item identifier (SKU)
    public int UnitPrice { get; } // Individual unit price
    public (int Quantity, int SpecialPrice) SpecialPrice { get; } // Special pricing rule (quantity and price)

    public PricingRule(char item, int unitPrice, (int quantity, int specialPrice) specialPrice)
    {
        Item = item;
        UnitPrice = unitPrice;
        SpecialPrice = specialPrice;
    }
}

// Represents the supermarket checkout system
public class Checkout
{
    private readonly Dictionary<char, int> _itemCounts; // Keep track of scanned items
    private readonly List<PricingRule> _pricingRules; // Pricing rules for items

    public Checkout(List<PricingRule> pricingRules)
    {
        _pricingRules = pricingRules ?? throw new ArgumentNullException(nameof(pricingRules));
        _itemCounts = new Dictionary<char, int>();
    }

    // Scan an item and update the item count
    public void Scan(char item)
    {
        if (!_itemCounts.ContainsKey(item))
        {
            _itemCounts[item] = 0;
        }

        _itemCounts[item]++;
    }

    // Calculate the total price based on scanned items and pricing rules
    public int CalculateTotalPrice()
    {
        int total = 0;

        foreach (var (item, count) in _itemCounts)
        {
            var pricingRule = _pricingRules.Find(rule => rule.Item == item);

            if (pricingRule == null)
            {
                throw new ArgumentException($"Item '{item}' is not valid.");
            }

            if (pricingRule.SpecialPrice.Quantity > 0 && count >= pricingRule.SpecialPrice.Quantity)
            {
                int specialPriceGroups = count / pricingRule.SpecialPrice.Quantity;
                int remainingItems = count % pricingRule.SpecialPrice.Quantity;

                total += specialPriceGroups * pricingRule.SpecialPrice.SpecialPrice + remainingItems * pricingRule.UnitPrice;
            }
            else
            {
                total += count * pricingRule.UnitPrice;
            }
        }

        return total;
    }
}
