namespace advent_of_code_c_.sol;

internal static class Day4
{
    /// <summary>
    /// Search for instances of the word "XMAS" vertically, horizontally, and diagonally which can be written backwards.
    /// Scan through board and do DFS when the current character is 'X' and look for the rest.
    /// </summary>
    public static void First()
    {
        var board = File.ReadAllLines("input/day4.txt");
        var r = board.Length;
        var c = board[0].Length;
        var count = 0;

        void CountXmas(int i, int j)
        {
            var directions = new[]
            {
                (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)
            };

            foreach (var (di, dj) in directions)
            {
                var ni = i;
                var nj = j;
                var found = true;
                foreach (var ch in "MAS")
                {
                    ni += di;
                    nj += dj;
                    if (ni >= 0 && ni < r && nj >= 0 && nj < c && board[ni][nj] == ch) continue;
                    found = false;
                    break;
                }

                if (found) count++;
            }
        }

        for (var i = 0; i < r; i++)
        {
            for (var j = 0; j < c; j++)
            {
                if (board[i][j] == 'X') CountXmas(i, j);
            }
        }

        Console.WriteLine("\"XMAS\" appears " + count + " times.");
    }

    /// <summary>
    /// Look in 4 corners for crisscrossing "MAS".
    /// </summary>
    public static void Second()
    {
        var board = File.ReadAllLines("input/day4.txt");
        var r = board.Length;
        var c = board[0].Length;
        var count = 0;

        void CountXmas(int i, int j)
        {
            if (
                // diagonal left to right, look for "MAS" or "SAM"
                ((i - 1 >= 0 && j - 1 >= 0 && board[i - 1][j - 1] == 'M' && i + 1 < r && j + 1 < c &&
                  board[i + 1][j + 1] == 'S') ||
                 (i - 1 >= 0 && j - 1 >= 0 && board[i - 1][j - 1] == 'S' && i + 1 < r && j + 1 < c &&
                  board[i + 1][j + 1] == 'M'))
                &&
                // diagonal , look for "MAS" or "SAM"
                ((i - 1 >= 0 && j + 1 < c && board[i - 1][j + 1] == 'M' && i + 1 < r && j - 1 >= 0 &&
                  board[i + 1][j - 1] == 'S') ||
                 (i - 1 >= 0 && j + 1 < c && board[i - 1][j + 1] == 'S' && i + 1 < r && j - 1 >= 0 &&
                  board[i + 1][j - 1] == 'M'))
            )
            {
                count++;
            }
        }

        for (var i = 0; i < r; i++)
        {
            for (var j = 0; j < c; j++)
            {
                if (board[i][j] == 'A') CountXmas(i, j);
            }
        }

        Console.WriteLine("X-\"MAS\" appears " + count + " times.");
    }
}