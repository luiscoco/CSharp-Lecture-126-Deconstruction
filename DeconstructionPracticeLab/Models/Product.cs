namespace DeconstructionPracticeLab.Models;

public class Product
{
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public bool IsAvailable { get; init; }

    public void Deconstruct(out string name, out decimal price)
    {
        name = Name;
        price = Price;
    }

    public void Deconstruct(out string name, out string category, out decimal price)
    {
        name = Name;
        category = Category;
        price = Price;
    }
}
