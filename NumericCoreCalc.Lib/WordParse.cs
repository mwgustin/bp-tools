
namespace NumericCoreCalc.Lib;

// given a 4 letter word, parse it into its numeric values
public class WordParse
{
    public int[] Parse(string word)
    {
        if (word.Length != 4)
        {
            throw new ArgumentException("word must be 4 letters");
        }

        word = word.ToUpper(); //force upper to ensure consistent mapping

        int[] result = new int[4];
        for (int i = 0; i < 4; i++)
        {
            char c = word[i];
            int value = CharToValue(c);
            result[i] = value;
        }
        return result;
    }

    public int CharToValue(char c)
    {
        return c switch
        {
            'A' => 1,
            'B' => 2,
            'C' => 3,
            'D' => 4,
            'E' => 5,
            'F' => 6,
            'G' => 7,
            'H' => 8,
            'I' => 9,
            'J' => 10,
            'K' => 11,
            'L' => 12,
            'M' => 13,
            'N' => 14,
            'O' => 15,
            'P' => 16,
            'Q' => 17,
            'R' => 18,
            'S' => 19,
            'T' => 20,
            'U' => 21,
            'V' => 22,
            'W' => 23,
            'X' => 24,
            'Y' => 25,
            'Z' => 26,
            _ => throw new ArgumentException($"invalid character: {c}"),
        };
    }
    public char ValueToChar(int value)
    {
        return value switch
        {
            1 => 'A',
            2 => 'B',
            3 => 'C',
            4 => 'D',
            5 => 'E',
            6 => 'F',
            7 => 'G',
            8 => 'H',
            9 => 'I',
            10 => 'J',
            11 => 'K',
            12 => 'L',
            13 => 'M',
            14 => 'N',
            15 => 'O',
            16 => 'P',
            17 => 'Q',
            18 => 'R',
            19 => 'S',
            20 => 'T',
            21 => 'U',
            22 => 'V',
            23 => 'W',
            24 => 'X',
            25 => 'Y',
            26 => 'Z',
            _ => throw new ArgumentException($"invalid value: {value}"),
        };
    }
}