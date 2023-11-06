namespace AdventOfCode;

using System;

[AttributeUsage(AttributeTargets.Class)]
public class SolutionAttribute : Attribute
{
    public int Year { get; init; }

    public int Day { get; init; }
}

[AttributeUsage(AttributeTargets.Method)]
public class PartAttribute : Attribute
{
    public PartAttribute(int part)
    {
        Part = part;
    }

    public int Part { get; }
}