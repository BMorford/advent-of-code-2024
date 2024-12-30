// Get input as 2D array.
char[][] input = File.ReadLines("./input.txt")
    .Select(x => x.ToArray())
    .ToArray();
char[] searchWord = [.. "XMAS"];
(int, int)[] directions = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];

int sum = 0;
for (int x = 0; x < input.Length; x++)
{
    for (int y = 0; y < input[x].Length; y++)
    {
        if (input[x][y] == searchWord[0])
        {
            foreach ((int, int) dir in directions)
            {
                // Skip the first letter since we already matched it.
                if (Search(searchWord[1..], dir, x, y))
                {
                    sum++;
                }
            }
        }
    }
}

bool Search(char[] searchWord, (int, int) direction, int xPos, int yPos)
{
    int xCoord = xPos + direction.Item1;
    int yCoord = yPos + direction.Item2;

    if (!(xCoord >= 0 && xCoord < input.Length && yCoord >= 0 && yCoord < input[xCoord].Length))
    {
        return false;
    }
    if (input[xPos + direction.Item1][yPos + direction.Item2] == searchWord[0])
    {
        if (searchWord.Length == 1)
        {
            return true;
        }
        return Search(searchWord[1..], direction, xPos + direction.Item1, yPos + direction.Item2);
    }
    return false;
}

Console.WriteLine(sum);