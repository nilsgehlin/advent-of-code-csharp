using System;
using AdventOfCode.Lib;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Y2023;

[Solution(Year = 2023, Day = 1)]
public class Day1
{
    public static int GetCalibrationValue1(string line)
    {
        if (line == string.Empty) return 0;

        var digits = line.ToArray().Where(char.IsDigit);
        return int.Parse(new string([digits.First(), digits.Last()]));
    }

    [Part(1)]
    public static int SolvePartOne(string input)
    {
        return input.Split(Environment.NewLine)
                    .Select(GetCalibrationValue1)
                    .Sum();
    }

    public static int GetCalibrationValue2(string line)
    {
        var spelledDigits = new Dictionary<string, char>()
        {
            {"one", '1'},
            {"two", '2'},
            {"tree", '3'},
            {"four", '4'},
            {"five", '5'},
            {"six", '6'},
            {"seven", '7'},
            {"eight", '8'},
            {"nine", '9'}
        };

        var digits = string.Empty;
        var possibleDigits = spelledDigits.Keys.ToList();
        var currSearchTerm = string.Empty;
        foreach (var chr in line.ToArray())
        {
            if (char.IsDigit(chr))
            {
                digits = string.Concat(digits, chr);
                currSearchTerm = string.Empty;
                possibleDigits = [.. spelledDigits.Keys];
                continue;
            }

            currSearchTerm = string.Concat(currSearchTerm, chr);
            foreach (var possibleDigit in possibleDigits)
            {
                if (possibleDigit[..currSearchTerm.Length] != currSearchTerm)
                {
                    possibleDigits.Remove(possibleDigit);
                    continue;
                }

                if (possibleDigit.Length == currSearchTerm.Length)
                {
                    digits = string.Concat(digits, spelledDigits[currSearchTerm]);
                    currSearchTerm = string.Empty;
                    possibleDigits = [.. spelledDigits.Keys];
                    break;
                }
            }
        }

        return int.Parse(new string([digits.First(), digits.Last()]));
    }

    [Part(2)]
    public static int SolvePartTwo(string input)
    {
        return input.Split(Environment.NewLine)
                    .Select(GetCalibrationValue2)
                    .Sum();
    }
}
