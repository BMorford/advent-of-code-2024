IEnumerable<string> input = File.ReadLines("./Day1/input.txt");

IEnumerable<string[]> sortedInput = input.Select(x => x.Split(new char[0], StringSplitOptions.RemoveEmptyEntries));
(IOrderedEnumerable<string>, List<string>) lists = (sortedInput.Select(x => x[0]).Order(), sortedInput.Select(x => x[1]).Order().ToList());

int sum = lists.Item1.Select((x, i) => new { value = int.Parse(x), i }).Sum(y => Math.Abs(y.value - int.Parse(lists.Item2[y.i])));

Console.WriteLine(sum.ToString());