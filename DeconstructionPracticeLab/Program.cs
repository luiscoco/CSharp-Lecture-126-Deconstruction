using DeconstructionPracticeLab.Demos;
using DeconstructionPracticeLab.Exercises;

var demos = new Dictionary<string, (string Title, Action Run)>
{
    ["1"] = ("Basic Tuple Deconstruction Demo", new BasicTupleDeconstructionDemo().Run),
    ["2"] = ("Var and Explicit Type Deconstruction Demo", new VarAndExplicitTypeDeconstructionDemo().Run),
    ["3"] = ("Existing Variable Deconstruction Demo", new ExistingVariableDeconstructionDemo().Run),
    ["4"] = ("Method Return Deconstruction Demo", new MethodReturnDeconstructionDemo().Run),
    ["5"] = ("Discards Deconstruction Demo", new DiscardsDeconstructionDemo().Run),
    ["6"] = ("Foreach Deconstruction Demo", new ForeachDeconstructionDemo().Run),
    ["7"] = ("Record Deconstruction Demo", new RecordDeconstructionDemo().Run),
    ["8"] = ("Custom Deconstruct Method Demo", new CustomDeconstructMethodDemo().Run),
    ["9"] = ("Multiple Deconstruct Overloads Demo", new MultipleDeconstructOverloadsDemo().Run),
    ["10"] = ("Extension Deconstruct Demo", new ExtensionDeconstructDemo().Run),
    ["11"] = ("LINQ Deconstruction Demo", new LinqDeconstructionDemo().Run),
    ["12"] = ("Dictionary Entry Deconstruction Demo", new DictionaryEntryDeconstructionDemo().Run),
    ["13"] = ("Switch Expression Deconstruction Demo", new SwitchExpressionDeconstructionDemo().Run),
    ["14"] = ("Deconstruction vs Manual Access Demo", new DeconstructionVsManualAccessDemo().Run),
    ["15"] = ("Deconstruction vs Tuple Access Demo", new DeconstructionVsTupleAccessDemo().Run),
    ["16"] = ("Variable Swapping Demo", new VariableSwappingDemo().Run),
    ["17"] = ("Validation Result Deconstruction Demo", new ValidationResultDeconstructionDemo().Run),
    ["18"] = ("Inheritance Deconstruction Demo", new InheritanceDeconstructionDemo().Run),
    ["19"] = ("Deconstruction Anti-Patterns Demo", new DeconstructionAntiPatternsDemo().Run),
    ["20"] = ("Student Challenges", new DeconstructionStudentChallenges().Run),
    ["21"] = ("Student Challenge Solutions", new DeconstructionStudentChallenges().ShowSolutions)
};

while (true)
{
    Console.Clear();
    Console.WriteLine("Deconstruction Practice Lab");
    Console.WriteLine("===========================");

    foreach (var (key, (title, _)) in demos)
    {
        Console.WriteLine($"{key}. {title}");
    }

    Console.WriteLine("0. Exit");
    Console.WriteLine();
    Console.Write("Choose a demo: ");

    var choice = Console.ReadLine();

    if (choice == "0")
    {
        break;
    }

    Console.Clear();

    if (choice is not null && demos.TryGetValue(choice, out var demo))
    {
        Console.WriteLine(demo.Title);
        Console.WriteLine(new string('-', demo.Title.Length));
        demo.Run();
    }
    else
    {
        Console.WriteLine("Unknown option.");
    }

    Console.WriteLine();
    Console.Write("Press Enter to return to the menu...");
    Console.ReadLine();
}
