using DeconstructionPracticeLab.Models;
using DeconstructionPracticeLab.Services;

namespace DeconstructionPracticeLab.Exercises;

public class DeconstructionStudentChallenges
{
    public void Run()
    {
        string[] challenges =
        [
            "1. Basic tuple deconstruction.",
            "2. Deconstruction with explicit types.",
            "3. Deconstruction into existing variables.",
            "4. Deconstruction using discards.",
            "5. A method returning a tuple and deconstructing the result.",
            "6. Deconstruction inside a foreach loop.",
            "7. Deconstruction of a positional record.",
            "8. A custom Deconstruct method for a Product class.",
            "9. An extension Deconstruct method for a type.",
            "10. A LINQ projection into tuples consumed with deconstruction.",
            "11. Dictionary entry deconstruction.",
            "12. A tuple assignment variable swap.",
            "13. A switch expression using tuple patterns.",
            "14. A bad deconstruction example refactored for readability."
        ];

        Console.WriteLine("Student challenges");
        Console.WriteLine("------------------");

        foreach (var challenge in challenges)
        {
            Console.WriteLine(challenge);
        }
    }

    public void ShowSolutions()
    {
        Console.WriteLine("Student challenge solutions");
        Console.WriteLine("---------------------------");

        // 1. Basic tuple deconstruction.
        var productTuple = (Name: "Laptop", Price: 1200m);
        var (tupleName, tuplePrice) = productTuple;
        Console.WriteLine($"1. {tupleName}: {tuplePrice:C}");

        // 2. Explicit types show exactly what the tuple contains.
        (string explicitName, decimal explicitPrice, bool explicitAvailability) = DeconstructionSamples.GetProduct();
        Console.WriteLine($"2. {explicitName}: {explicitPrice:C}, available: {explicitAvailability}");

        // 3. Existing variables can receive values from a tuple.
        string name;
        decimal price;
        bool isAvailable;
        (name, price, isAvailable) = DeconstructionSamples.GetKeyboard();
        Console.WriteLine($"3. {name}: {price:C}, available: {isAvailable}");

        // 4. Discards ignore values that are not needed.
        var (_, _, detailsPrice, detailsAvailability) = DeconstructionSamples.GetProductDetails();
        Console.WriteLine($"4. Price: {detailsPrice:C}, available: {detailsAvailability}");

        // 5. Tuple-returning methods are concise for small multi-value results.
        var (subtotal, tax, total) = DeconstructionSamples.CalculateInvoice(100m, 0.21m);
        Console.WriteLine($"5. {subtotal:C} + {tax:C} = {total:C}");

        // 6. foreach deconstruction keeps tuple iteration readable.
        List<(string Name, int Score)> scores = [("Anna", 92), ("Luis", 85)];
        foreach (var (studentName, score) in scores)
        {
            Console.WriteLine($"6. {studentName}: {score}");
        }

        // 7. Positional records include a generated Deconstruct method.
        var customer = new Customer("Marta", "Garcia", "marta@example.com");
        var (firstName, lastName, email) = customer;
        Console.WriteLine($"7. {firstName} {lastName}: {email}");

        // 8. Product exposes custom Deconstruct overloads.
        var product = new Product { Name = "Mouse", Category = "Accessories", Price = 29.99m };
        var (productName, productPrice) = product;
        Console.WriteLine($"8. {productName}: {productPrice:C}");

        // 9. Order receives deconstruction through an extension method.
        var order = new Order { Id = 7, Total = 99.50m, Status = "Paid" };
        var (orderId, orderTotal) = order;
        Console.WriteLine($"9. Order {orderId}: {orderTotal:C}");

        // 10. LINQ tuple projections are useful for local, short-lived results.
        Product[] products =
        [
            new() { Name = "Laptop", Price = 1200m, IsAvailable = true },
            new() { Name = "Monitor", Price = 250m, IsAvailable = false }
        ];

        foreach (var (summaryName, finalPrice) in DeconstructionSamples.GetAvailableProductSummaries(products))
        {
            Console.WriteLine($"10. {summaryName}: {finalPrice:0.00}");
        }

        // 11. Dictionary entries can be deconstructed into key and value.
        Dictionary<int, string> productNames = new() { [1] = "Laptop" };
        foreach (var (productId, dictionaryName) in productNames)
        {
            Console.WriteLine($"11. {productId}: {dictionaryName}");
        }

        // 12. Tuple assignment can swap values without a temporary variable.
        var (swappedA, swappedB) = DeconstructionSamples.Swap(10, 20);
        Console.WriteLine($"12. a = {swappedA}, b = {swappedB}");

        // 13. Tuple patterns match multiple values at once.
        Console.WriteLine($"13. {DeconstructionSamples.GetShippingCategory("Spain", 120m)}");

        // 14. Prefer property access when the names are important for clarity.
        Console.WriteLine($"14. Cleaner property access: {product.Name}, {product.Category}, {product.Price:C}");
    }
}
