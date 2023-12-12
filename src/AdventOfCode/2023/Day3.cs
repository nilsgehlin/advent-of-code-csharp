using System;
using System.Collections.Generic;
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
        var rows = input.Split(Environment.NewLine).Where(line => line != string.Empty).ToArray();
        var sum = 0;
        for (var i = 0; i < rows.Length; i++)
        {
            for (var j = 0; j < rows[i].Length; j++)
            {
                if (IsPartNumber(rows, i, j))
                {
                    Console.WriteLine(rows[i][j]);
                    sum += int.Parse(char.ToString(rows[i][j]));
                }
            }
        }
        return sum;
    }

    private static bool IsPartNumber(string[] rows, int rowIdx, int colIdx)
    {
        if (!char.IsDigit(rows[rowIdx][colIdx])) return false;

        char[] neighbours = [];

        // Console.WriteLine($"{rowIdx}({rows.Length}),{colIdx}({rows[rowIdx].Length})");
        for (var k = Math.Max(rowIdx - 1, 0); k <= Math.Min(rowIdx + 1, rows.Length - 1); k++)
        {
            for (var l = Math.Max(colIdx - 1, 0); l <= Math.Min(colIdx + 1, rows[rowIdx].Length - 1); l++)
            {
                if (k == rowIdx && l == colIdx) continue;

                // Console.WriteLine($"  {k},{l}");
                if (SymbolsRegex().IsMatch(char.ToString(rows[k][l])))
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
}

