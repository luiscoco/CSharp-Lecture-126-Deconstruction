using DeconstructionPracticeLab.Models;

namespace DeconstructionPracticeLab.Services;

public static class DeconstructionSamples
{
    public static (string Name, decimal Price, bool IsAvailable) GetProduct()
        => ("Laptop", 1200m, true);

    public static (string Name, decimal Price, bool IsAvailable) GetKeyboard()
        => ("Keyboard", 79.99m, true);

    public static (string Name, decimal Price, bool IsAvailable) GetMouse()
        => ("Mouse", 29.99m, false);

    public static (decimal Subtotal, decimal Tax, decimal Total) CalculateInvoice(decimal amount, decimal taxRate)
    {
        var tax = amount * taxRate;
        return (amount, tax, amount + tax);
    }

    public static (string Name, string Category, decimal Price, bool IsAvailable) GetProductDetails()
        => ("Laptop", "Computers", 1200m, true);

    public static (decimal Min, decimal Max, decimal Average) AnalyzePrices(decimal[] prices)
        => (prices.Min(), prices.Max(), prices.Average());

    public static IReadOnlyList<(string Name, decimal FinalPrice)> GetAvailableProductSummaries(IEnumerable<Product> products)
        => products
            .Where(product => product.IsAvailable)
            .Select(product => (product.Name, FinalPrice: product.Price * 1.21m))
            .ToList();

    public static string GetShippingCategory(string country, decimal orderTotal)
        => (country, orderTotal) switch
        {
            ("Spain", >= 100m) => "Free domestic shipping",
            ("Spain", _) => "Standard domestic shipping",
            ("France", >= 150m) => "Free EU shipping",
            (_, >= 300m) => "Free international shipping",
            _ => "Standard international shipping"
        };

    public static string GetOrderCategory(ShippingOrder order)
        => order switch
        {
            ("Spain", >= 100m) => "Free domestic shipping",
            ("Spain", _) => "Standard domestic shipping",
            (_, >= 300m) => "Free international shipping",
            _ => "Standard international shipping"
        };

    public static ValidationResult ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return new ValidationResult(false, "Email is required.");
        }

        if (!email.Contains('@') || !email.Contains('.'))
        {
            return new ValidationResult(false, "Email must contain @ and a domain.");
        }

        return new ValidationResult(true, string.Empty);
    }

    public static (int A, int B) Swap(int a, int b)
    {
        (a, b) = (b, a);
        return (a, b);
    }
}
