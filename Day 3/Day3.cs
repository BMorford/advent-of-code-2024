using System.Text.RegularExpressions;

string input = File.ReadAllText("./input.txt");
string pattern = @"(?<=[\s\S]*?(do\(\)|don't\(\))[\s\S]*?)?(?:mul\((\d{1,3},\d{1,3})\))";
Regex regex = new(pattern);
IEnumerable<(string, string)> matches = regex.Matches(input).Select(m => (m.Groups[1].Value, m.Groups[2].Value));

int sum = matches.Sum(m =>
{
    int[] operands = [.. m.Item2.Split(",").Select(v => int.Parse(v))];
    return operands[0] * operands[1];
});


// Part 2.
bool enabled = true;
int enabledSum = 0;
foreach ((string, string) match in matches)
{
    enabled = match.Item1 switch
    {
        "do()" => true,
        "don't()" => false,
        _ => enabled
    };

    if (enabled)
    {
        int[] operands = [.. match.Item2.Split(",").Select(v => int.Parse(v))];
        enabledSum += operands[0] * operands[1];
    }
}

Console.WriteLine(sum.ToString());
Console.WriteLine(enabledSum.ToString());