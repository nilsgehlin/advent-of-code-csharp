using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Lib;

namespace AdventOfCode.Y2023;


[Solution(Year = 2023, Day = 3)]
public partial class Day3
{

    [Part(1)]
    public static int SolvePartOne(string input)
    {
        var lines = input.Split(Environment.NewLine);
        for (var lineIdx = 0; lineIdx < lines.Length; lineIdx++)
        {
            line = lines[lineIdx];
            foreach (Match match in SymbolsRegex().Matches(line))
            {

            }
        }
    }

    [Part(2)]
    public static int SolvePartTwo(string input) =>
        0;

    [GeneratedRegex(@"[^\d.\s]")]
    private static partial Regex SymbolsRegex();
}

