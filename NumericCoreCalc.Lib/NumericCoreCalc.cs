using System.Linq;
// Blue Prince numeric core calculator

// Take any number with 4 or more digits. Without changing the sequence, 
//   split that number into 4 smaller numbers (ex 86455 > 8, 6, 45, 5)

// Assign each entry in the sequence a different mathematical operation (+, -, /, *).  
//   The first item in the sequence will always be +.
//   The sequence should find the lowest whole number possible.

// If the result is a number with more than 3 numbers, repeat the process.
//  The final number you obtain that is less than 4 digits is considered the numeric core of the larger number

// Example:
//  86455
//  8, 6, 45, 5
//  +8, -6, *45, /5
//  18

namespace NumericCoreCalc.Lib;

public class NumericCoreCalculator
{

    // public int Calculate(string val)
    public int Calculate(int[][] partitions)
    {
        var operationSequences = OperationSequenceFactory.GetAllSequences();
        var minResult = int.MaxValue;
        foreach (var parts in partitions)
        {
            if (parts.Length != 4)
                throw new ArgumentException("Input must be split into exactly 4 parts.");

            int minPartitionResult = int.MaxValue;
            foreach (var sequence in operationSequences)
            {
                try
                {
                    // Calculate the result for this partition and operation sequence
                    int result = CalculateParts(parts, sequence.Sequence);
                    if (result < minPartitionResult && result > 0)
                    {
                        minPartitionResult = result;
                    }
                }
                catch (DivideByZeroException)
                {
                    // Ignore divisions by zero
                }
                catch (InvalidOperationException)
                {
                    // Ignore non-whole number results
                }
            }

            if (minPartitionResult < 1000)
            {
                if (minPartitionResult < minResult && minPartitionResult > 0)
                    minResult = minPartitionResult;
            }
            else
            {
                // Recursively calculate the numeric core for results >= 1000
                if (minPartitionResult != int.MaxValue)
                {
                    var parse = new Parse();
                    var parseParts = parse.Partitions(minPartitionResult.ToString());
                    var core = Calculate(parseParts);
                    if (core < minResult && core > 0)
                    {
                        minResult = core;
                    }
                }
            }
        }
        return minResult == int.MaxValue ? throw new Exception("No valid result found") : minResult;
    }

    private int CalculateParts(int[] parts, Operation[] operationSequence)
    {
        if (parts.Length != 4 || operationSequence.Length != 4)
            throw new ArgumentException("Both parts and operation sequence must contain exactly 4 elements.");

        double result = parts[0]; // Start with the first part
        for (int i = 1; i < parts.Length; i++)
        {
            switch (operationSequence[i])
            {
                case Operation.Add:
                    result += parts[i];
                    break;
                case Operation.Subtract:
                    result -= parts[i];
                    break;
                case Operation.Multiply:
                    result *= parts[i];
                    break;
                case Operation.Divide:
                    if (parts[i] == 0)
                        throw new DivideByZeroException("Cannot divide by zero.");
                    result /= parts[i];
                    break;
            }
        }
        if (result % 1 != 0) // Check if result is not a whole number
            throw new InvalidOperationException("Result is not a whole number.");
        return (int)result;
    }
}


public enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide
}

public class OperationSequence
{
    public Operation[] Sequence { get; set; }
    public OperationSequence(Operation[] operations)
    {
        if (operations.Length != 4)
            throw new ArgumentException("Operation sequence must contain exactly 4 operations.");
        Sequence = operations;
    }
}

public static class OperationSequenceFactory
{
    public static OperationSequence[] GetAllSequences()
    {
        var operations = Enum.GetValues(typeof(Operation)).Cast<Operation>().ToArray();
        var sequences = new List<OperationSequence>();

        foreach (var op2 in operations.Where(op => op != Operation.Add))
        {
            foreach (var op3 in operations.Where(op => op != Operation.Add && op != op2))
            {
                foreach (var op4 in operations.Where(op => op != Operation.Add && op != op2 && op != op3))
                {
                    sequences.Add(new OperationSequence([Operation.Add, (Operation)op2, (Operation)op3, (Operation)op4]));
                }
            }
        }

        return sequences.ToArray();
    }
}