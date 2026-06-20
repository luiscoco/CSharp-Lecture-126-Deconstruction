# Deconstruction Decision Guide

## 1. What deconstruction is

Deconstruction takes a value that contains multiple parts and assigns those parts to separate variables in one expression.

```csharp
var product = (Name: "Laptop", Price: 1200m);
var (name, price) = product;
```

Use it when the local variables are immediately useful and the order of values is obvious.

## Decision table

| Scenario | Use deconstruction? | Better alternative if not | Reason | Example |
|---|---|---|---|---|
| Small tuple returned from a private method | Yes | Named record for public APIs | Tuple order is easy to see locally | `var (subtotal, tax, total) = CalculateInvoice(...);` |
| Tuple with named fields used once | Maybe | Named tuple access | Field names may be clearer | `invoice.Total` |
| Positional record with two or three fields | Yes | Property access for larger records | Records define an intentional order | `var (first, last, email) = customer;` |
| Class with obvious positional meaning | Maybe | Properties | Deconstruct should not hide meaning | `var (name, price) = product;` |
| Class with many unrelated properties | No | Properties or DTO | Position is not obvious | `product.Category` |
| `foreach` over tuples | Yes | Loop variable with properties | Tuple values are local to the loop | `foreach (var (name, score) in scores)` |
| `foreach` over dictionaries | Yes | `entry.Key` and `entry.Value` | Key/value is an obvious pair | `foreach (var (id, name) in products)` |
| Nested tuple keys | Maybe | Intermediate variable | Nested patterns become hard to scan | `var key = entry.Key;` |
| Ignoring one or two values | Yes | Smaller tuple or record | `_` shows intentional ignoring | `var (name, _, price) = item;` |
| Ignoring many values | No | Redesign the return shape | Many discards suggest the shape is wrong | `var (name, _, _, _, _) = value;` |
| Switch expression over multiple values | Yes | `if` statements for complex logic | Tuple patterns make simple rules compact | `(country, total) switch { ... }` |
| Public reusable domain result | Usually no | Named record | Names document the concept | `InvoiceResult` |

## 2. Deconstructing tuples

Tuples are the most common beginner-friendly deconstruction example. Named tuple fields make the original tuple readable, and deconstruction creates local variables chosen by the caller.

```csharp
var invoice = (Subtotal: 100m, Tax: 21m, Total: 121m);
var (subtotal, tax, total) = invoice;
```

Prefer tuples for small local shapes. Prefer records for values that cross public boundaries.

## 3. Deconstructing records

Positional records automatically support deconstruction. The order comes from the primary constructor.

```csharp
public record Customer(string FirstName, string LastName, string Email);

var customer = new Customer("Anna", "Smith", "anna@example.com");
var (firstName, lastName, email) = customer;
```

This works well for small immutable data shapes. For large records, property access is usually clearer.

## 4. Creating custom Deconstruct methods

Any type can support deconstruction by exposing a `Deconstruct` method with `out` parameters.

```csharp
public void Deconstruct(out string name, out decimal price)
{
    name = Name;
    price = Price;
}
```

Only add this when readers can easily guess what the positions mean.

## 5. Using discards

Use `_` when a returned value is intentionally ignored.

```csharp
var (name, _, price, _) = GetProductDetails();
```

One or two discards are fine. Many discards suggest the return value is too large for deconstruction.

## 6. Deconstruction in foreach loops

Deconstruction is useful when iterating tuples or key-value pairs.

```csharp
foreach (var (name, score) in results)
{
    Console.WriteLine($"{name}: {score}");
}
```

Use clear names. Avoid deconstructing long tuples inside loops.

## 7. Deconstruction with LINQ

LINQ can project temporary results into tuples and consume them with deconstruction.

```csharp
var summaries = products.Select(p => (p.Name, FinalPrice: p.Price * 1.21m));

foreach (var (name, finalPrice) in summaries)
{
    Console.WriteLine($"{name}: {finalPrice:0.00}");
}
```

For reusable reports, prefer a named DTO or record.

## 8. Deconstruction with dictionaries

Dictionary entries can be deconstructed into key and value.

```csharp
foreach (var (productId, productName) in productNames)
{
    Console.WriteLine($"{productId}: {productName}");
}
```

Tuple keys can also be deconstructed, but nested deconstruction should stay small.

## 9. Deconstruction with pattern matching

Tuple patterns let switch expressions match multiple values at once.

```csharp
static string GetShippingCategory(string country, decimal total)
    => (country, total) switch
    {
        ("Spain", >= 100m) => "Free domestic shipping",
        (_, >= 300m) => "Free international shipping",
        _ => "Standard international shipping"
    };
```

Positional patterns can also use a record or type that supports `Deconstruct`.

## 10. When named tuple access is clearer

Named tuple access keeps the original field names visible.

```csharp
Console.WriteLine(invoice.Subtotal);
Console.WriteLine(invoice.Tax);
Console.WriteLine(invoice.Total);
```

Use named access when the names carry important meaning.

## 11. When property access is clearer

Property access is more explicit than deconstruction.

```csharp
var productName = product.Name;
var category = product.Category;
var price = product.Price;
```

Use property access when readers need the property names to understand the code.

## 12. Common readability mistakes

- Deconstructing too many values.
- Using unclear names like `a`, `b`, and `c`.
- Nesting deconstruction so deeply that the loop is hard to read.
- Adding `Deconstruct` to a type where the value order is not obvious.
- Deconstructing only to reduce line count.

## 13. Practical rules for professional C# projects

- Use deconstruction for small, obvious shapes.
- Prefer records or DTOs for reusable domain concepts.
- Keep custom `Deconstruct` methods rare and intentional.
- Use discards to show intentional ignoring, not to hide poor design.
- Choose property access when it communicates more than position.
- Keep examples in teaching code short enough to scan without guessing.
