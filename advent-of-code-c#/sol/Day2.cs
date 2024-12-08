namespace advent_of_code_c_.sol;

internal static class Day2
{
    /**
     * safe if both are true
     *  - either all increasing or decreasing (monotonic)
     *  - adjacent levels differ by at least 1 and at most 3
     */
    public static void First()
    {
        try
        {
            var lines = File.ReadLines("input/day2.txt");
            var safeCount = 0;
            foreach (var line in lines)
            {
                var levels = line.Split(' ').Select(int.Parse).ToArray();
                var safe = true;
                // use prevDiff to check if it's monotonic
                var prevDiff = 0;
                // start from 1, since we need to compare with previous level
                for (var i = 1; i < levels.Length; i++)
                {
                    var diff = levels[i] - levels[i - 1];
                    // if it's not monotonic or difference is not between 1 and 3, it's not safe, so break
                    if (prevDiff * diff < 0 || Math.Abs(diff) is < 1 or > 3)
                    {
                        safe = false;
                        break;
                    }

                    prevDiff = diff;
                }

                if (safe) safeCount++;
            }

            Console.WriteLine(safeCount);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /**
     * same as first, except now, we can remove any one level to make it safe
     */
    public static void Second()
    {
        try
        {
            var lines = File.ReadLines("input/day2.txt");
            var safeCount = 0;
            foreach (var line in lines)
            {
                var levels = line.Split(' ').Select(int.Parse).ToArray();
                // try removing each level and check if it's safe
                var skipIdx = 0;
                bool safe;
                do
                {
                    // reset safe to true every time we remove a level
                    safe = true;
                    // remove the level at skipIdx
                    var newLevels = levels.Where((_, idx) => idx != skipIdx).ToArray();
                    // same as first, check if it's monotonic and difference is between 1 and 3
                    var prevDiff = 0;
                    for (var i = 1; i < newLevels.Length; i++)
                    {
                        var diff = newLevels[i] - newLevels[i - 1];
                        if (prevDiff * diff < 0 || Math.Abs(diff) is < 1 or > 3)
                        {
                            safe = false;
                            break;
                        }

                        prevDiff = diff;
                    }

                    skipIdx++;
                } while
                    (!safe && skipIdx <
                     levels.Length); // keep removing until it's safe or when we run out of levels

                if (safe) safeCount++;
            }

            Console.WriteLine(safeCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}