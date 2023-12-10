using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode.Y2023;

[Solution(Year = 2023, Day = 2)]
public partial class Day2
{
    private static readonly Dictionary<string, uint> _colorDistribution = new()
   {
       {"red", 12},
       {"green", 13},
       {"blue", 14}
   };

    private static bool IsPossible(string line)
    {
        foreach (Match match in GetColorAndQtyRegex().Matches(line))
        {
            var qty = int.Parse(match.Value);
            var color = match.Groups[1].Value;

            if (qty > _colorDistribution[color])
            {
                return false;
            }
        }

        return true;
    }

    [GeneratedRegex(@"\d+(?= (red|green|blue))")]
    private static partial Regex GetColorAndQtyRegex();

    private static int GetId(string line) =>
        int.Parse(GetGameIdRegEx().Match(line).Value);

    [GeneratedRegex(@"(?<=Game )\d+(?=:)")]
    private static partial Regex GetGameIdRegEx();

    [Part(1)]
    public static int SolvePartOne(string input) =>
        input.Split(Environment.NewLine).ToArray()
             .Where(line => line != string.Empty)
             .Where(IsPossible)
             .Select(GetId)
             .Sum();

    [Part(2)]
    public static int SolvePartTwo(string input)
    {
        return 0;
    }

}
