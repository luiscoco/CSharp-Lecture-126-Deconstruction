namespace DeconstructionPracticeLab.Models;

public class Order
{
    public int Id { get; init; }
    public decimal Total { get; init; }
    public string Status { get; init; } = string.Empty;
}
