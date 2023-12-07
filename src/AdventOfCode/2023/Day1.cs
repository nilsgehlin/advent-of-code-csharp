using System;
using AdventOfCode.Lib;
using System.Linq;

namespace AdventOfCode.Y2023;

[Solution(Year = 2023, Day = 1)]
public class Day1
{
    public static int GetCalibrationValue(string line)
    {
        if (line == string.Empty) return 0;

        var digits = line.ToArray().Where(char.IsDigit);
        return int.Parse(new string([digits.First(), digits.Last()]));
    }

    [Part(1)]
    public static int SolvePartOne(string input)
    {
        return input.Split(Environment.NewLine)
                    .Select(GetCalibrationValue)
                    .Sum();
    }
}
