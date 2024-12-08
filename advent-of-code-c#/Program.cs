using System.Reflection;

namespace advent_of_code_c_;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the day number as an argument.");
            return;
        }

        var dayNumber = args[0];
        var className = $"advent_of_code_c_.sol.Day{dayNumber}";
        var type = Type.GetType(className);

        if (type == null)
        {
            Console.WriteLine($"Class {className} not found.");
            return;
        }

        var firstMethod = type.GetMethod("First", BindingFlags.Public | BindingFlags.Static);
        var secondMethod = type.GetMethod("Second", BindingFlags.Public | BindingFlags.Static);

        if (firstMethod == null || secondMethod == null)
        {
            Console.WriteLine($"Methods First and/or Second not found in class {className}.");
            return;
        }

        firstMethod.Invoke(null, null);
        secondMethod.Invoke(null, null);
    }
}