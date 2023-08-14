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
