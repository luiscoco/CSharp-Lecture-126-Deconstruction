namespace DeconstructionPracticeLab.Models;

public class Employee : Person
{
    public string Department { get; init; } = string.Empty;

    public void Deconstruct(out string firstName, out string lastName, out string department)
    {
        firstName = FirstName;
        lastName = LastName;
        department = Department;
    }
}
