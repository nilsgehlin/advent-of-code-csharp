using System;
using AdventOfCode.Lib;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2023;

[Solution(Year = 2023, Day = 1)]
public class Day1
{
    private static readonly Dictionary<string, char> _spelledDigits = new()
        {
            {"one", '1'},
            {"two", '2'},
            {"three", '3'},
            {"four", '4'},
            {"five", '5'},
            {"six", '6'},
            {"seven", '7'},
            {"eight", '8'},
            {"nine", '9'}
        };

    public static int GetCalibrationValue(string line, bool allowSpelledOutDigits = false)
    {
        var digits = new List<char>();
        for (var i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                digits.Add(line[i]);
                continue;
            }

            if (!allowSpelledOutDigits) continue;

            var spelledDigit = _spelledDigits.Keys.ToArray()
                .Where(digit => digit.Length < line.Length - i + 1)
                .Where(digit => digit == line.Substring(i, digit.Length))
                .Select(digit => _spelledDigits[digit]);

            digits.AddRange(spelledDigit);
        }

        if (digits.Count == 0) return 0;

        return int.Parse(new string([digits.First(), digits.Last()]));
    }

    private static int Solve(string input, Func<string, int> calculateCalibrationValue)
    {
        return input.Split(Environment.NewLine)
            .Where(line => line != string.Empty)
            .Select(calculateCalibrationValue)
            .Sum();
    }


    [Part(1)]
    public static int SolvePartOne(string input)
    {
        return Solve(input, line => GetCalibrationValue(line));
    }

    [Part(2)]
    public static int SolvePartTwo(string input)
    {
        return Solve(input, line => GetCalibrationValue(line, true));
    }
}
