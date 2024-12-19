namespace advent_of_code_c_.sol;

internal static class Day5
{
    /// <summary>
    /// Store rules as a dictionary that maps page numbers to a hash set of the following page numbers.
    /// Then, for each page number in an update sequence, check if current number is supposed to be
    /// after the preceding page numbers.
    ///
    /// Time complexity: O(M^2), 2 nested for-loops of M updates
    /// Space complexity: O(N), dictionary mapping each unique int to a set of ints
    /// </summary>
    public static void First()
    {
        var rules = new Dictionary<int, HashSet<int>>();
        var sum = 0;
        using (var sr = new StreamReader("input/day5.txt"))
        {
            // read rules
            ParseRules(sr, rules);

            // process updates
            var line = sr.ReadLine();
            while (line is not null)
            {
                var update = line.TrimEnd().Split(',').Select(int.Parse).ToArray();
                // iterate from the second page number
                for (var i = 1; i < update.Length; i++)
                {
                    // check against each previous page number
                    for (var j = i - 1; j >= 0; j--)
                    {
                        // if any previous page number is supposed to be after the current number, fail
                        if (rules.TryGetValue(update[i], out var ruleSet) && ruleSet.Contains(update[j]))
                        {
                            goto nextIter;
                        }
                    }
                }

                sum += update[update.Length / 2];
                nextIter:
                line = sr.ReadLine();
            }
        }

        Console.WriteLine("Sum of middle page number from correctly-ordered updates: " + sum);
    }

    /// <summary>
    /// Using the similar flow, swap page numbers to satisfy the rule like insertion sort.
    ///
    /// Time complexity: O(M^2), 2 nested for-loops of M updates
    /// Space complexity: O(N), dictionary mapping each unique int to a set of ints
    /// </summary>
    public static void Second()
    {
        var rules = new Dictionary<int, HashSet<int>>();
        var sum = 0;
        using (var sr = new StreamReader("input/day5.txt"))
        {
            // read rules
            ParseRules(sr, rules);

            // process updates
            var line = sr.ReadLine();
            while (line is not null)
            {
                var update = line.TrimEnd().Split(',').Select(int.Parse).ToArray();
                var correct = true;
                // iterate from the second page number
                for (var i = 1; i < update.Length; i++)
                {
                    var curI = i;
                    // check against each previous page number
                    for (var j = i - 1; j >= 0; j--)
                    {
                        // continue if updates follow the rules
                        if (!rules.TryGetValue(update[curI], out var ruleSet) || !ruleSet.Contains(update[j])) continue;
                        // otherwise, mark this update incorrect and rectify the error by swapping locations
                        correct = false;
                        (update[curI], update[j]) = (update[j], update[curI]);
                        curI = j;
                    }
                }

                if (!correct) sum += update[update.Length / 2];
                line = sr.ReadLine();
            }
        }

        Console.WriteLine("Sum of middle page number from corrected incorrectly-ordered updates: " + sum);
    }

    private static void ParseRules(StreamReader sr, Dictionary<int, HashSet<int>> rules)
    {
        var line = sr.ReadLine();
        while (line is not null && line != "")
        {
            var ruleString = line.TrimEnd().Split('|');
            int num1 = int.Parse(ruleString[0]), num2 = int.Parse(ruleString[1]);
            if (!rules.TryAdd(num1, [num2]))
            {
                rules[num1].Add(num2);
            }

            line = sr.ReadLine();
        }
    }
}