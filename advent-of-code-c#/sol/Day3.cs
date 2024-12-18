using System.Diagnostics;

namespace advent_of_code_c_.sol;

internal static class Day3
{
    /**
     * find a number in the string and return it
     */
    private static int ParseInt(string memory, ref int i, ref int lenParse)
    {
        if (i >= memory.Length - lenParse - 1 || !char.IsDigit(memory[i]))
        {
            i += 1;
            return -1;
        }

        var startIdx = i;
        while (i < memory.Length && char.IsDigit(memory[i]))
        {
            i++;
            lenParse++;
        }

        return int.Parse(memory.Substring(startIdx, i - startIdx));
    }

    /**
     * find a character in the string
     */
    private static bool CharDoesntExist(string memory, char c, ref int i, ref int lenParse)
    {
        return i >= memory.Length - lenParse++ || memory[i++] != c;
    }

    /**
     * parse "mul(a,b)" and sum up a * b
     */
    public static void First()
    {
        try
        {
            var memory = File.ReadAllText("input/day3.txt").Replace('\n', ' ');
            var result = 0;
            var i = 0;
            while (i < memory.Length)
            {
                var lenMul = 0;
                // check for 'mul('
                if (CharDoesntExist(memory, 'm', ref i, ref lenMul)) continue;
                if (CharDoesntExist(memory, 'u', ref i, ref lenMul)) continue;
                if (CharDoesntExist(memory, 'l', ref i, ref lenMul)) continue;
                if (CharDoesntExist(memory, '(', ref i, ref lenMul)) continue;

                // look for digits
                var a = ParseInt(memory, ref i, ref lenMul);
                if (a == -1) continue;

                // look for ','
                if (CharDoesntExist(memory, ',', ref i, ref lenMul)) continue;

                // look for digits
                var b = ParseInt(memory, ref i, ref lenMul);
                if (b == -1) continue;

                // look for ')'
                if (CharDoesntExist(memory, ')', ref i, ref lenMul)) continue;

                result += a * b;
            }

            Console.WriteLine(result);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /**
     * same as first, except now, also parse "do()" and "don't()"
     */
    public static void Second()
    {
        try
        {
            var memory = File.ReadAllText("input/day3.txt").Replace('\n', ' ');
            var result = 0;
            var disabled = false;
            var i = 0;
            while (i < memory.Length)
            {
                var lenParse = 0;
                // need temp vars for i and lenMul because we don't want to advance them if parsing "don't()" fails
                var tempI = i;
                var tempLenParse = lenParse;

                if (disabled)
                {
                    // parse "do()": if successful, resume parsing "mul(a,b)", else continue parsing "do()"
                    // advancing i and lenMul here is ok
                    if (CharDoesntExist(memory, 'd', ref i, ref lenParse)) continue;
                    if (CharDoesntExist(memory, 'o', ref i, ref lenParse)) continue;
                    if (CharDoesntExist(memory, '(', ref i, ref lenParse)) continue;
                    if (CharDoesntExist(memory, ')', ref i, ref lenParse)) continue;
                    disabled = false;
                }
                else
                {
                    // parse "don't()": if successful, disable parsing, else continue parsing "mul(a,b)"
                    if (CharDoesntExist(memory, 'd', ref tempI, ref tempLenParse)) goto mulParsing;
                    if (CharDoesntExist(memory, 'o', ref tempI, ref tempLenParse)) goto mulParsing;
                    if (CharDoesntExist(memory, 'n', ref tempI, ref tempLenParse)) goto mulParsing;
                    if (CharDoesntExist(memory, '\'', ref tempI, ref tempLenParse)) goto mulParsing;
                    if (CharDoesntExist(memory, 't', ref tempI, ref tempLenParse)) goto mulParsing;
                    if (CharDoesntExist(memory, '(', ref tempI, ref tempLenParse)) goto mulParsing;
                    if (CharDoesntExist(memory, ')', ref tempI, ref tempLenParse)) goto mulParsing;
                    i = tempI;
                    disabled = true;
                    continue;
                }

                mulParsing:
                // advance i to tempI because of "don't()" parsing. Also need to decrement i by 1 since
                // tempI advances by one in CharDoesntExist, but don't want to advance i here
                i = tempI - 1;
                // look for 'mul('
                if (CharDoesntExist(memory, 'm', ref i, ref lenParse)) continue;
                if (CharDoesntExist(memory, 'u', ref i, ref lenParse)) continue;
                if (CharDoesntExist(memory, 'l', ref i, ref lenParse)) continue;
                if (CharDoesntExist(memory, '(', ref i, ref lenParse)) continue;

                // look for digits
                var a = ParseInt(memory, ref i, ref lenParse);
                if (a == -1) continue;

                // look for ','
                if (CharDoesntExist(memory, ',', ref i, ref lenParse)) continue;

                // look for digits
                var b = ParseInt(memory, ref i, ref lenParse);
                if (b == -1) continue;

                // look for ')'
                if (CharDoesntExist(memory, ')', ref i, ref lenParse)) continue;

                result += a * b;
            }

            Console.WriteLine(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}