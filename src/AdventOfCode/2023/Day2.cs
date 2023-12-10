using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode.Y2023;

public record CubeQty(string Color, int Qty);


[Solution(Year = 2023, Day = 2)]
public partial class Day2
{
    private static readonly Dictionary<string, int> _colorDistribution = new()
    {
        {"red", 12},
        {"green", 13},
        {"blue", 14}
    };

    [Part(1)]
    public static int SolvePartOne(string input) =>
        input.Split(Environment.NewLine).ToArray()
             .Where(line => line != string.Empty)
             .Where(IsPossible)
             .Select(GetId)
             .Sum();

    [Part(2)]
    public static int SolvePartTwo(string input) =>
        input.Split(Environment.NewLine).ToArray()
             .Where(line => line != string.Empty)
             .Select(PowerOfMinimumSets)
             .Sum();

    private static IEnumerable<CubeQty> ParseCudeQty(string gameLine)
    {
        foreach (Match match in GetColorAndQtyRegex().Matches(gameLine))
        {
            var qty = int.Parse(match.Value);
            var color = match.Groups[1].Value;

            yield return new CubeQty(color, qty);
        }
    }

    private static bool IsPossible(string line)
    {
        foreach (var cubeQty in ParseCudeQty(line))
        {
            if (cubeQty.Qty > _colorDistribution[cubeQty.Color])
            {
                return false;
            }
        }

        return true;
    }

    private static int GetId(string line) =>
        int.Parse(GetGameIdRegEx().Match(line).Value);

    public static int PowerOfMinimumSets(string line)
    {
        var maxQty = new Dictionary<string, int>()
        {
            {"red", 0},
            {"green", 0},
            {"blue", 0}
        };

        foreach (var cubeQty in ParseCudeQty(line))
        {
            if (cubeQty.Qty > maxQty[cubeQty.Color])
            {
                maxQty[cubeQty.Color] = cubeQty.Qty;
            }
        }

        return maxQty.Values.Aggregate(1, (accum, maxVal) => accum * maxVal);
    }

    [GeneratedRegex(@"\d+(?= (red|green|blue))")]
    private static partial Regex GetColorAndQtyRegex();

    [GeneratedRegex(@"(?<=Game )\d+(?=:)")]
    private static partial Regex GetGameIdRegEx();
}
