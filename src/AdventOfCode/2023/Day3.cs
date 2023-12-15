using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode.Y2023;

public record Range(int Lower, int Upper);
public record Number(int Value, int RowIdx, Range ColumnIdxRange);

[Solution(Year = 2023, Day = 3)]
public partial class Day3
{

    [Part(1)]
    public static int SolvePartOne(string input)
    {
        var lines = input.Split(Environment.NewLine)
            .Where(line => line != string.Empty);

        return lines
            .SelectMany((line, idx) => NumbersRegex().Matches(line).Select(match => (match, idx)))
            .Select(matchAndIdx => new Number(int.Parse(matchAndIdx.match.Value),
                                              matchAndIdx.idx,
                                              new Range(matchAndIdx.match.Index, matchAndIdx.match.Index + matchAndIdx.match.Value.Length - 1)))
            .Where(number => IsPartNumber(number, lines))
            .Select(number => number.Value)
            .Sum();
    }

    private static bool IsPartNumber(Number number, IEnumerable<string> lines)
    {
        for (var i = Math.Max(number.RowIdx - 1, 0); i <= Math.Min(number.RowIdx + 1, lines.Count() - 1); i++)
        {
            for (var j = Math.Max(number.ColumnIdxRange.Lower - 1, 0); j <= Math.Min(number.ColumnIdxRange.Upper + 1, lines.ElementAt(i).Length - 1); j++)
            {
                if (SymbolsRegex().IsMatch(char.ToString(lines.ElementAt(i)[j])))
                {
                    return true;
                }
            }
        }

        return false;
    }

    [Part(2)]
    public static int SolvePartTwo(string input) =>
        0;

    [GeneratedRegex(@"[^\d.\s]")]
    private static partial Regex SymbolsRegex();

    [GeneratedRegex(@"\d{1,}")]
    private static partial Regex NumbersRegex();
}

