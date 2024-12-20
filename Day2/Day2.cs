using System.Collections.Immutable;

IEnumerable<string> input = File.ReadLines("./input.txt");

var reports = input.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(level => int.Parse(level))).ToList();
Func<int, bool> acceptableRange = x => Math.Abs(x) >= 1 && Math.Abs(x) <= 3;

int safeReports = 0;
int safeDampenedReports = 0;
foreach (List<int> report in reports.Select(r => r.ToList()))
{
    int totalCells = report.Count - 1;
    int validCells = GetSafeCellCount(report);

    if (validCells == totalCells)
    {
        safeReports++;
    }
}

foreach (ImmutableList<int> report in reports.Select(r => r.ToImmutableList()))
{
    int totalCells = report.Count - 1;
    int maxValidCells = report.Select((_, i) => GetSafeCellCount([.. report.RemoveAt(i)])).Max();

    if (maxValidCells >= totalCells - 1)
    {
        safeDampenedReports++;
    }
}

Console.WriteLine(safeReports);
Console.WriteLine(safeDampenedReports);

static int GetSafeCellCount(List<int> report)
{
    int validCells = 0;
    for (int i = 1; i < report.Count; i++)
    {
        int diff = report[i] - report[i - 1];
        if (Math.Abs(diff) >= 1 && Math.Abs(diff) <= 3)
        {
            if (i > 1)
            {
                if ((report[i - 1] - report[i - 2]) * diff > 0)
                {
                    validCells++;
                }
            }
            else
            {
                validCells++;
            }
        }
    }
    return validCells;
}
