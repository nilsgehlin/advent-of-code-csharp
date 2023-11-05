namespace AdventOfCode;

using System;

[AttributeUsage(AttributeTargets.Method)]
public class SolutionAttribute : Attribute
{
    public int Year { get; init; }

    public int Day { get; init; }

    public int Part { get; init; }
}