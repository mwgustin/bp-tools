// See https://aka.ms/new-console-template for more information

using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using NumericCoreCalc.Lib;

int[][]? partitions = null;
if (args.Length < 1)
{
    Console.WriteLine("Enter Number: ");
    string? x = Console.ReadLine();
    if (x is null)
    {
        Console.WriteLine("Nothing entered... Exiting.");
        return;
    }
    if (x.Contains(' '))
    {
        partitions = GetPartitions(x.Split(' '));
    }
    else
    {
        var parse = new Parse();
        partitions = parse.Partitions(x);
    }

    var calc = new NumericCoreCalculator();
    Console.WriteLine($"Result:  {calc.Calculate(partitions)}");

}
else if (args.Length == 1 && args[0] == "word")
{
    List<string> wordList = [
      "pigs"
    ];
    List<int> results = new List<int>();
    var wp = new WordParse();
    foreach (var word in wordList)
    {
        var nums = wp.Parse(word);
        var wpCalc = new NumericCoreCalculator();
        try
        {
            var result = wpCalc.Calculate([nums]);
            results.Add(result);
            Console.WriteLine($"Result for {word}: {result}");
        }
        catch (Exception ex)
        {
            results.Add(0);
            Console.WriteLine($"Error for {word}: {ex.Message}");
        }
    }
    List<char> chars = new List<char>();
    foreach (var r in results)
    {
        if (r >= 1 && r <= 26)
        {
            chars.Add(wp.ValueToChar(r));
        }
        else
        {
            chars.Add('?');
        }
    }
    Console.WriteLine($"Chars: {new string(chars.ToArray())}");
}
else
{
    partitions = GetPartitions(args);
    var calc = new NumericCoreCalculator();

    Console.WriteLine($"Result:  {calc.Calculate(partitions)}");
}




int[][] GetPartitions(string[] s)
{
    if (s.Length == 1)
    {
        var parse = new Parse();
        return parse.Partitions(s[0]);
    }

    if (s.Length == 4)
    {
        return new int[][] { s.Select(int.Parse).ToArray() };
    }

    throw new ArgumentException("Input must be a single string or an array of 4 strings.");
}


