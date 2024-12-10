namespace advent_of_code_c_.sol;

public class Day1
{
    public static void First()
    {
        try
        {
            var lines = File.ReadLines("input/day1.txt").ToArray();
            var list1 = new int[lines.Length];
            var list2 = new int[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                var pair = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                list1[i] = int.Parse(pair[0]);
                list2[i] = int.Parse(pair[1]);
            }
            
            Array.Sort(list1);
            Array.Sort(list2);

            // manhattan distance
            var dist = list1.Select((t, i) => Math.Abs(t - list2[i])).Sum();

            Console.WriteLine(dist);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static void Second()
    {
        try
        {
            var lines = File.ReadLines("input/day1.txt").ToArray();
            var list1 = new int[lines.Length];
            var map2 = new Dictionary<int, int>();
            for (var i = 0; i < lines.Length; i++)
            {
                var pair = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                list1[i] = int.Parse(pair[0]);
                var key = int.Parse(pair[1]);
                if (!map2.TryAdd(key, 1))
                {
                    map2[key]++;
                }
            }
            
            // sum up product of list1 elems and its count in the 2nd list (t * count) if t exists in map2, else 0
            var similarity = list1.Select(t => map2.TryGetValue(t, out var count) ? t * count : 0).Sum();
            Console.WriteLine(similarity);
        }
        catch (IOException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}