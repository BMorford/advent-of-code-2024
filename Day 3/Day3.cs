using System.Text.RegularExpressions;

string input = File.ReadAllText("./input.txt");
string pattern = @"mul\((\d{1,3},\d{1,3})\)";
Regex regex = new(pattern);
IEnumerable<string> matches = regex.Matches(input).Select(m => m.Groups[1].Value);

int sum = matches.Sum(m =>
{
    int[] operands = [.. m.Split(",").Select(v => int.Parse(v))];
    return operands[0] * operands[1];
});

Console.WriteLine(sum.ToString());