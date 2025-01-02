string[] input = File.ReadAllLines("./input.txt");
List<IEnumerable<int>> rules = input.Where(r => r.Contains('|')).Select(r => r.Split('|').Select(int.Parse)).ToList();
List<List<int>> updates = input.Where(r => r.Contains(',')).Select(r => r.Split(",").Select(int.Parse).ToList()).ToList();
List<List<int>> validUpdates = updates.Where(IsValidUpdate).ToList();

bool IsValidUpdate(List<int> update)
{
    foreach (var page in update.Select((p, i) => new { Value = p, Index = i }))
    {
        IEnumerable<IEnumerable<int>> applicableRules = rules.Where(r => r.Contains(page.Value));
        foreach (IEnumerable<int> rule in applicableRules)
        {
            if (rule.First() == page.Value)
            {
                int pageIndex = update.IndexOf(rule.Last());
                if (pageIndex > -1 && page.Index >= pageIndex)
                {
                    return false;
                }
            }
            else
            {
                int pageIndex = update.IndexOf(rule.First());
                if (pageIndex > -1 && page.Index <= pageIndex)
                {
                    return false;
                }
            }
        }
    }
    return true;
}

int sum = validUpdates.Sum(u => u[u.Count / 2]);
Console.WriteLine(sum);