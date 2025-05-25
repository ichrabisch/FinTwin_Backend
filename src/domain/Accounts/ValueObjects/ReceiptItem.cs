namespace Domain.Accounts.ValueObjects;

public sealed record ReceiptItem
{
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TaxRate { get; set; }
    public string Category { get; set; }
    public string GeneralCategory { get; set; }
    public ReceiptItem(string itemName, string itemDescription, decimal quantity, string unit, decimal unitPrice, decimal totalPrice, decimal taxRate, string category, string generalCategory)
    {
        ItemName = itemName;
        ItemDescription = itemDescription;
        Quantity = quantity;
        Unit = unit;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
        TaxRate = taxRate;
        Category = category;
        GeneralCategory = generalCategory;
    }

}
