using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode.Y2023;

public record Range(int Lower, int Upper);
public record Number(int Value, int RowIdx, Range ColumnIdxRange);
public record Symbol(string Value, int RowIdx, int ColumnIdx);
public record Gear(int FirstNumber, int SecondNumber);

[Solution(Year = 2023, Day = 3)]
public partial class Day3
{

    [Part(1)]
    public static int SolvePartOne(string input)
    {
        var lines = input.Split(Environment.NewLine)
            .Where(line => line != string.Empty);

        return lines
            .GetMatchesAndIndicies(NumbersRegex)
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
    public static int SolvePartTwo(string input)
    {
        var lines = input.Split(Environment.NewLine)
            .Where(line => line != string.Empty);

        var numbers = lines
            .GetMatchesAndIndicies(NumbersRegex)
            .Select(matchAndIdx => new Number(int.Parse(matchAndIdx.match.Value),
                                              matchAndIdx.idx,
                                              new Range(matchAndIdx.match.Index, matchAndIdx.match.Index + matchAndIdx.match.Value.Length - 1)));

        return lines
            .GetMatchesAndIndicies(SymbolsRegex)
            .Select(matchAndIdx => new Symbol(matchAndIdx.match.Value, matchAndIdx.idx, matchAndIdx.match.Index))
            .Select(symbol => ToGear(symbol, numbers))
            .Sum(gear => (gear is null) ? 0 : gear.FirstNumber * gear.SecondNumber);

    }


    private static Gear? ToGear(Symbol symbol, IEnumerable<Number> numbers)
    {
        if (symbol.Value != "*") return null;

        var neighbours = numbers.Where(number => IsNeighbour(symbol, number));
        if (neighbours.Count() != 2) return null;

        return new Gear(neighbours.ElementAt(0).Value, neighbours.ElementAt(1).Value);
    }

    private static bool IsNeighbour(Symbol symbol, Number number)
    {
        for (var i = symbol.RowIdx - 1; i <= symbol.RowIdx + 1; i++)
        {
            if (number.RowIdx != i) continue;

            for (var j = symbol.ColumnIdx - 1; j <= symbol.ColumnIdx + 1; j++)
            {
                if (j >= number.ColumnIdxRange.Lower && j <= number.ColumnIdxRange.Upper) return true;
            }
        }
        return false;
    }

    [GeneratedRegex(@"[^\d.\s]")]
    private static partial Regex SymbolsRegex();

    [GeneratedRegex(@"\d{1,}")]
    private static partial Regex NumbersRegex();
}

internal static class Extensions
{
    public static IEnumerable<(Match match, int idx)> GetMatchesAndIndicies(this IEnumerable<string> lines, Func<Regex> regexMethod) =>
        lines.SelectMany((line, idx) => regexMethod().Matches(line).Select(match => (match, idx)));
}
