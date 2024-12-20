IEnumerable<string> input = File.ReadLines("./input.txt");

// Part 1
IEnumerable<string[]> splitInput = input.Select(x => x.Split(new char[0], StringSplitOptions.RemoveEmptyEntries));
(IOrderedEnumerable<string>, List<string>) sortedLists = (splitInput.Select(x => x[0]).Order(), splitInput.Select(x => x[1]).Order().ToList());

int sum = sortedLists.Item1.Select((x, i) => new { value = int.Parse(x), i }).Sum(y => Math.Abs(y.value - int.Parse(sortedLists.Item2[y.i])));

Console.WriteLine(sum.ToString());

// Part 2
int similarityScore = sortedLists.Item1.Sum(x => int.Parse(x) * sortedLists.Item2.Count(y => y == x));

Console.Write(similarityScore.ToString());
