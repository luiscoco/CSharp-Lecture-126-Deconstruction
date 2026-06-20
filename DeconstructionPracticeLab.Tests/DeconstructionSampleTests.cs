using DeconstructionPracticeLab.Models;
using DeconstructionPracticeLab.Services;

namespace DeconstructionPracticeLab.Tests;

[TestClass]
public sealed class DeconstructionSampleTests
{
    [TestMethod]
    public void CalculateInvoice_ReturnsExpectedSubtotalTaxAndTotal()
    {
        // Arrange
        const decimal amount = 100m;
        const decimal taxRate = 0.21m;

        // Act
        var (subtotal, tax, total) = DeconstructionSamples.CalculateInvoice(amount, taxRate);

        // Assert
        Assert.AreEqual(100m, subtotal);
        Assert.AreEqual(21m, tax);
        Assert.AreEqual(121m, total);
    }

    [TestMethod]
    public void VarDeconstruction_ProducesExpectedValues()
    {
        // Arrange and Act
        var (name, price, isAvailable) = DeconstructionSamples.GetProduct();

        // Assert
        Assert.AreEqual("Laptop", name);
        Assert.AreEqual(1200m, price);
        Assert.IsTrue(isAvailable);
    }

    [TestMethod]
    public void ExplicitTypeDeconstruction_ProducesExpectedValues()
    {
        // Arrange and Act
        (string name, decimal price, bool isAvailable) = DeconstructionSamples.GetProduct();

        // Assert
        Assert.AreEqual("Laptop", name);
        Assert.AreEqual(1200m, price);
        Assert.IsTrue(isAvailable);
    }

    [TestMethod]
    public void ExistingVariableDeconstruction_AssignsExpectedValues()
    {
        // Arrange
        string name;
        decimal price;
        bool isAvailable;

        // Act
        (name, price, isAvailable) = DeconstructionSamples.GetKeyboard();

        // Assert
        Assert.AreEqual("Keyboard", name);
        Assert.AreEqual(79.99m, price);
        Assert.IsTrue(isAvailable);
    }

    [TestMethod]
    public void DiscardsExample_KeepsOnlyNeededValues()
    {
        // Arrange and Act
        var (name, _, price, _) = DeconstructionSamples.GetProductDetails();

        // Assert
        Assert.AreEqual("Laptop", name);
        Assert.AreEqual(1200m, price);
    }

    [TestMethod]
    public void RecordDeconstruction_ProducesExpectedFields()
    {
        // Arrange
        var customer = new Customer("Anna", "Smith", "anna@example.com");

        // Act
        var (firstName, lastName, email) = customer;

        // Assert
        Assert.AreEqual("Anna", firstName);
        Assert.AreEqual("Smith", lastName);
        Assert.AreEqual("anna@example.com", email);
    }

    [TestMethod]
    public void CustomProductDeconstruct_ReturnsExpectedValues()
    {
        // Arrange
        var product = new Product { Name = "Laptop", Category = "Computers", Price = 1200m };

        // Act
        var (name, price) = product;

        // Assert
        Assert.AreEqual("Laptop", name);
        Assert.AreEqual(1200m, price);
    }

    [TestMethod]
    public void MultipleDeconstructOverloads_ReturnExpectedValues()
    {
        // Arrange
        var product = new Product { Name = "Laptop", Category = "Computers", Price = 1200m };

        // Act
        var (name, price) = product;
        var (name2, category, price2) = product;

        // Assert
        Assert.AreEqual("Laptop", name);
        Assert.AreEqual(1200m, price);
        Assert.AreEqual("Laptop", name2);
        Assert.AreEqual("Computers", category);
        Assert.AreEqual(1200m, price2);
    }

    [TestMethod]
    public void ExtensionDeconstructForOrder_ReturnsExpectedValues()
    {
        // Arrange
        var order = new Order { Id = 10, Total = 150m, Status = "Paid" };

        // Act
        var (id, total) = order;

        // Assert
        Assert.AreEqual(10, id);
        Assert.AreEqual(150m, total);
    }

    [TestMethod]
    public void LinqTupleProjection_ProducesExpectedValues()
    {
        // Arrange
        Product[] products =
        [
            new() { Name = "Laptop", Category = "Computers", Price = 100m, IsAvailable = true },
            new() { Name = "Monitor", Category = "Displays", Price = 200m, IsAvailable = false }
        ];

        // Act
        var summaries = DeconstructionSamples.GetAvailableProductSummaries(products);
        var (name, finalPrice) = summaries.Single();

        // Assert
        Assert.AreEqual("Laptop", name);
        Assert.AreEqual(121m, finalPrice);
    }

    [TestMethod]
    public void DictionaryEntryDeconstruction_GivesExpectedKeyAndValue()
    {
        // Arrange
        Dictionary<int, string> productNames = new() { [1] = "Laptop" };

        // Act
        var (productId, productName) = productNames.Single();

        // Assert
        Assert.AreEqual(1, productId);
        Assert.AreEqual("Laptop", productName);
    }

    [TestMethod]
    public void VariableSwappingUsingTupleAssignment_Works()
    {
        // Arrange
        const int a = 10;
        const int b = 20;

        // Act
        var (swappedA, swappedB) = DeconstructionSamples.Swap(a, b);

        // Assert
        Assert.AreEqual(20, swappedA);
        Assert.AreEqual(10, swappedB);
    }

    [TestMethod]
    public void GetShippingCategory_ReturnsExpectedResult()
    {
        // Arrange
        const string country = "Spain";
        const decimal orderTotal = 100m;

        // Act
        var category = DeconstructionSamples.GetShippingCategory(country, orderTotal);

        // Assert
        Assert.AreEqual("Free domestic shipping", category);
    }

    [TestMethod]
    public void ValidationResultDeconstruction_ReturnsExpectedValues()
    {
        // Arrange and Act
        var (isValid, errorMessage) = DeconstructionSamples.ValidateEmail("test@example.com");

        // Assert
        Assert.IsTrue(isValid);
        Assert.AreEqual(string.Empty, errorMessage);
    }
}
