using DeconstructionPracticeLab.Models;
using DeconstructionPracticeLab.Services;

namespace DeconstructionPracticeLab.Demos;

public class BasicTupleDeconstructionDemo
{
    public void Run()
    {
        var product = (Name: "Laptop", Price: 1200m);

        // Deconstruction extracts tuple elements into separate variables.
        // The variable names on the left side are chosen by the caller.
        var (name, price) = product;

        // Named tuple fields make the original tuple readable.
        Console.WriteLine($"product.Name: {product.Name}");
        Console.WriteLine($"product.Price: {product.Price}");
        Console.WriteLine($"name: {name}");
        Console.WriteLine($"price: {price}");
    }
}

public class VarAndExplicitTypeDeconstructionDemo
{
    public void Run()
    {
        // var lets the compiler infer all deconstructed variable types.
        var (name, price, isAvailable) = DeconstructionSamples.GetProduct();

        // Explicit types can make teaching examples clearer.
        // The number of variables must match the number of tuple elements.
        (string explicitName, decimal explicitPrice, bool explicitAvailability) = DeconstructionSamples.GetProduct();

        Console.WriteLine($"var: {name}, {price:C}, available: {isAvailable}");
        Console.WriteLine($"explicit: {explicitName}, {explicitPrice:C}, available: {explicitAvailability}");
        Console.WriteLine("Use the style that makes the code easiest to read.");
    }
}

public class ExistingVariableDeconstructionDemo
{
    public void Run()
    {
        string name;
        decimal price;
        bool isAvailable;

        var product = DeconstructionSamples.GetKeyboard();

        // Deconstruction can assign into existing variables.
        (name, price, isAvailable) = product;
        Console.WriteLine($"{name}: {price:C}, available: {isAvailable}");

        // This is useful when updating several related local variables at once.
        // Do not overuse it when separate assignments would be clearer.
        (name, price, isAvailable) = DeconstructionSamples.GetMouse();
        Console.WriteLine($"{name}: {price:C}, available: {isAvailable}");
    }
}

public class MethodReturnDeconstructionDemo
{
    public void Run()
    {
        // Deconstruction is often used with methods returning tuples.
        // It removes the need for a temporary tuple variable.
        var (subtotal, tax, total) = DeconstructionSamples.CalculateInvoice(100m, 0.21m);

        Console.WriteLine($"Subtotal: {subtotal:C}");
        Console.WriteLine($"Tax: {tax:C}");
        Console.WriteLine($"Total: {total:C}");
        Console.WriteLine("For important public domain results, a named record may be better than a tuple.");
    }
}

public class DiscardsDeconstructionDemo
{
    public void Run()
    {
        // _ is a discard: it means "I intentionally do not need this value."
        var (name, _, price, _) = DeconstructionSamples.GetProductDetails();
        Console.WriteLine($"{name}: {price:C}");

        decimal[] prices = [10m, 25m, 40m];
        var (_, max, average) = DeconstructionSamples.AnalyzePrices(prices);
        Console.WriteLine($"Max: {max:C}");
        Console.WriteLine($"Average: {average:C}");
        Console.WriteLine("Too many discards can signal that the tuple is too large or poorly shaped.");
    }
}

public class ForeachDeconstructionDemo
{
    public void Run()
    {
        List<(string Name, int Score)> results =
        [
            ("Anna", 92),
            ("Luis", 85),
            ("Marta", 97)
        ];

        // Deconstruction can be used directly in foreach.
        foreach (var (name, score) in results)
        {
            Console.WriteLine($"{name}: {score}");
        }

        Dictionary<string, int> scoresByName = new()
        {
            ["Anna"] = 92,
            ["Luis"] = 85,
            ["Marta"] = 97
        };

        // This is useful when iterating tuples or key-value pairs.
        // Clear variable names matter more than short names.
        foreach (var (studentName, score) in scoresByName)
        {
            Console.WriteLine($"{studentName}: {score}");
        }
    }
}

public class RecordDeconstructionDemo
{
    public void Run()
    {
        var customer = new Customer("Anna", "Smith", "anna@example.com");

        // Positional records automatically support deconstruction.
        // The deconstruction order follows the positional record declaration.
        var (firstName, lastName, email) = customer;

        Console.WriteLine($"{firstName} {lastName}");
        Console.WriteLine(email);
        Console.WriteLine("For large records, deconstructing many values can reduce readability.");
    }
}

public class CustomDeconstructMethodDemo
{
    public void Run()
    {
        var product = new Product
        {
            Name = "Laptop",
            Category = "Computers",
            Price = 1200m,
            IsAvailable = true
        };

        // Any type can support deconstruction with a suitable Deconstruct method.
        // The Deconstruct method uses out parameters.
        var (name, price) = product;

        Console.WriteLine($"{name}: {price:C}");
        Console.WriteLine("Only expose Deconstruct when the positional meaning is obvious.");
    }
}

public class MultipleDeconstructOverloadsDemo
{
    public void Run()
    {
        var product = new Product
        {
            Name = "Laptop",
            Category = "Computers",
            Price = 1200m,
            IsAvailable = true
        };

        // The compiler selects the overload based on the number of target variables.
        var (name, price) = product;
        var (name2, category, price2) = product;

        Console.WriteLine($"{name}: {price:C}");
        Console.WriteLine($"{name2} / {category}: {price2:C}");
        Console.WriteLine("Prefer one or two obvious deconstruction shapes.");
    }
}

public class ExtensionDeconstructDemo
{
    public void Run()
    {
        var order = new Order { Id = 42, Total = 199.99m, Status = "Paid" };

        // Extension Deconstruct methods can add support to types you do not own.
        // Use this carefully because it can surprise other developers.
        var (id, total) = order;

        Console.WriteLine($"Order {id}: {total:C}");
        Console.WriteLine("Prefer explicit property access when the shape is not obvious.");
    }
}

public class LinqDeconstructionDemo
{
    public void Run()
    {
        List<Product> products =
        [
            new() { Name = "Laptop", Category = "Computers", Price = 1200m, IsAvailable = true },
            new() { Name = "Keyboard", Category = "Accessories", Price = 79.99m, IsAvailable = true },
            new() { Name = "Monitor", Category = "Displays", Price = 250m, IsAvailable = false }
        ];

        // LINQ can project into tuples.
        var summaries = DeconstructionSamples.GetAvailableProductSummaries(products);

        // foreach deconstruction makes tuple projection easy to consume.
        foreach (var (name, finalPrice) in summaries)
        {
            Console.WriteLine($"{name}: {finalPrice:0.00}");
        }

        Console.WriteLine("For reusable reports, prefer a named DTO or record.");
    }
}

public class DictionaryEntryDeconstructionDemo
{
    public void Run()
    {
        Dictionary<int, string> productNames = new()
        {
            [1] = "Laptop",
            [2] = "Keyboard",
            [3] = "Mouse"
        };

        // KeyValuePair supports deconstruction in foreach.
        foreach (var (productId, productName) in productNames)
        {
            Console.WriteLine($"{productId}: {productName}");
        }

        Dictionary<(int CustomerId, int ProductId), int> quantities = new()
        {
            [(1, 10)] = 3,
            [(1, 11)] = 2,
            [(2, 10)] = 5
        };

        // Tuple keys can also be deconstructed.
        // Nested deconstruction is powerful but can become hard to read.
        foreach (var ((customerId, productId), quantity) in quantities)
        {
            Console.WriteLine($"Customer {customerId}, Product {productId}: {quantity}");
        }
    }
}

public class SwitchExpressionDeconstructionDemo
{
    public void Run()
    {
        Console.WriteLine(DeconstructionSamples.GetShippingCategory("Spain", 120m));
        Console.WriteLine(DeconstructionSamples.GetShippingCategory("Spain", 50m));
        Console.WriteLine(DeconstructionSamples.GetShippingCategory("France", 180m));
        Console.WriteLine(DeconstructionSamples.GetShippingCategory("Brazil", 350m));
        Console.WriteLine(DeconstructionSamples.GetShippingCategory("Brazil", 80m));

        var order = new ShippingOrder("Spain", 150m);

        // Tuple patterns match multiple values at once.
        // Positional patterns can use a record's generated Deconstruct method.
        Console.WriteLine(DeconstructionSamples.GetOrderCategory(order));
        Console.WriteLine("The order of values matters, so pattern matching should remain readable.");
    }
}

public class DeconstructionVsManualAccessDemo
{
    public void Run()
    {
        var product = new Product { Name = "Laptop", Category = "Computers", Price = 1200m };

        // Manual property access is explicit and clear.
        var productName = product.Name;
        var category = product.Category;
        var price = product.Price;

        // Deconstruction is concise, but it depends on positional meaning.
        var (name, category2, price2) = product;

        Console.WriteLine($"Manual: {productName}, {category}, {price:C}");
        Console.WriteLine($"Deconstructed: {name}, {category2}, {price2:C}");
        Console.WriteLine("Do not deconstruct just to reduce lines if property names are important.");
    }
}

public class DeconstructionVsTupleAccessDemo
{
    public void Run()
    {
        var invoice = (Subtotal: 100m, Tax: 21m, Total: 121m);

        // Named tuple access keeps the original field names visible.
        Console.WriteLine(invoice.Subtotal);
        Console.WriteLine(invoice.Tax);
        Console.WriteLine(invoice.Total);

        // Deconstruction creates independent local variables.
        var (subtotal, tax, total) = invoice;
        Console.WriteLine($"{subtotal}, {tax}, {total}");
        Console.WriteLine("Use named access when it improves readability.");
    }
}

public class VariableSwappingDemo
{
    public void Run()
    {
        int a = 10;
        int b = 20;

        Console.WriteLine($"Before: a = {a}, b = {b}");

        // Tuple assignment can swap variables without a temporary variable.
        (a, b) = (b, a);

        Console.WriteLine($"After: a = {a}, b = {b}");
        Console.WriteLine("Use it only when it remains easy to understand.");
    }
}

public class ValidationResultDeconstructionDemo
{
    public void Run()
    {
        // A named record is clearer than a raw tuple when the concept is reused.
        var (isValid, errorMessage) = DeconstructionSamples.ValidateEmail("test@example.com");

        Console.WriteLine($"Is valid: {isValid}");
        Console.WriteLine($"Error: {errorMessage}");
        Console.WriteLine("Avoid deconstructing if the property names matter more than brevity.");
    }
}

public class InheritanceDeconstructionDemo
{
    public void Run()
    {
        var employee = new Employee
        {
            FirstName = "Anna",
            LastName = "Smith",
            Department = "Engineering"
        };

        // Derived types can offer additional Deconstruct overloads.
        var (first, last) = employee;
        var (first2, last2, department) = employee;

        Console.WriteLine($"{first} {last}");
        Console.WriteLine($"{first2} {last2}, {department}");
        Console.WriteLine("Too many positional shapes can confuse API users.");
    }
}

public class DeconstructionAntiPatternsDemo
{
    public void Run()
    {
        Console.WriteLine("1. Too many values");
        var fullProduct = ("Laptop", "Computers", 1200m, true, "A-100", 2.4m);
        var (name, _, price, _, _, _) = fullProduct;
        Console.WriteLine($"Problematic: {name}, {price:C}");

        var product = new Product { Name = "Laptop", Category = "Computers", Price = 1200m, IsAvailable = true };
        Console.WriteLine($"Cleaner: {product.Name}, {product.Price:C}");

        Console.WriteLine();
        Console.WriteLine("2. Unclear names");
        var (a, b) = ("Keyboard", 79.99m);
        Console.WriteLine($"Problematic: {a}, {b}");
        var (productName, productPrice) = ("Keyboard", 79.99m);
        Console.WriteLine($"Cleaner: {productName}, {productPrice:C}");

        Console.WriteLine();
        Console.WriteLine("3. Nested deconstruction");
        Dictionary<(int CustomerId, int ProductId), int> quantities = new()
        {
            [(1, 10)] = 3
        };

        foreach (var ((customerId, productId), quantity) in quantities)
        {
            Console.WriteLine($"Problematic when larger: {customerId}, {productId}, {quantity}");
        }

        foreach (var entry in quantities)
        {
            var key = entry.Key;
            Console.WriteLine($"Cleaner: Customer {key.CustomerId}, Product {key.ProductId}, Quantity {entry.Value}");
        }

        Console.WriteLine();
        Console.WriteLine("4. Property access can be clearer");
        var (deconstructedName, deconstructedCategory, _) = product;
        Console.WriteLine($"Problematic: {deconstructedName}, {deconstructedCategory}");
        Console.WriteLine($"Cleaner: {product.Name}, {product.Category}");

        Console.WriteLine();
        Console.WriteLine("5. Avoid Deconstruct when position is not obvious");
        Console.WriteLine("Cleaner: use named properties when readers need the names to understand the values.");
    }
}
