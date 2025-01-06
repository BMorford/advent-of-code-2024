using System.Collections.ObjectModel;

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

// Part 2.
List<Update> invalidUpdates = updates.Where(u => !IsValidUpdate(u)).Select(u => new Update(u, rules)).ToList();
foreach (Update update in invalidUpdates)
{
    update.Pages.Sort();
}

int correctedSum = invalidUpdates.Sum(u => u.Pages[u.Pages.Count / 2].Value);
Console.WriteLine(correctedSum);

public class Update
{
    public readonly List<Page> Pages;
    public readonly List<IEnumerable<int>> Rules;

    public Update(List<int> updateValues, List<IEnumerable<int>> rules)
    {
        Pages = updateValues.Select(u => new Page(this, u)).ToList();
        Rules = rules;
    }
}

public class Page(Update update, int pageValue) : IComparable
{
    public readonly int Value = pageValue;
    private Update Update = update;

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }
        Page comparePage = obj as Page;
        int[] applicableRule = Update.Rules.FirstOrDefault(r => r.Contains(Value) && r.Contains(comparePage.Value)).ToArray();

        if (applicableRule == null)
        {
            return 0;
        }

        if (applicableRule[0] == Value)
        {
            return -1;
        }
        return 1;
    }
}
