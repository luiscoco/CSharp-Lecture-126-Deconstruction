namespace DeconstructionPracticeLab.Models;

public static class OrderDeconstructionExtensions
{
    public static void Deconstruct(this Order order, out int id, out decimal total)
    {
        id = order.Id;
        total = order.Total;
    }
}
