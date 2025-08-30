namespace NumericCoreCalc.Lib;

public class Parse
{
    //given a string, split it into 4 parts in all possible ways
    // return a list of int arrays, each array is a possible split
    // eg. 86455 -> [[8,6,4,55],[8,6,45,5],[8,64,5,5],[86,4,5,5]]
    public int[][] Partitions(string s)
    {
        var result = new List<List<int>>();
        int n = s.Length;

        // We need to place 3 dividers in the string of length n
        // The dividers can be placed in positions 1 to n-1 (between characters)
        for (int i = 1; i < n - 2; i++)
        {
            for (int j = i + 1; j < n - 1; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    var part1 = s.Substring(0, i);
                    var part2 = s.Substring(i, j - i);
                    var part3 = s.Substring(j, k - j);
                    var part4 = s.Substring(k);

                    // Convert parts to integers and add to result
                    result.Add(new List<int> { int.Parse(part1), int.Parse(part2), int.Parse(part3), int.Parse(part4) });
                }
            }
        }

        return result.Select(part => part.ToArray()).ToArray();
    }
}