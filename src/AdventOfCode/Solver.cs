namespace AdventOfCode;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public record Solution(int Year, int Day, MethodInfo Part1, MethodInfo Part2);

public static class Solver
{
    private const string DataDir = "../../data";

    // public static void RunSolutions(int? year, int? day, int? part, bool runExample) =>
    //     Assembly.GetExecutingAssembly().GetTypes()
    //         .Where(IsSolution)
    //         .Where(t => CorrectYear(t, year))
    //         .Where(t => CorrectDay(t, day))
    //         .ToList().ForEach(t => RunSolution(t, part, runExample));

    public static void RunSolutions(int? year, int? day, int? part, bool runExample)
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .SelectMany(t => t.GetMethods())
            .RunIfSolution(runExample);
    }

    private static IEnumerable<Solution> RunIfSolution(this IEnumerable<MethodInfo> assemblyMethods, bool runExample)
    {
        foreach (var method in assemblyMethods)
        {
            var solution = method.GetCustomAttribute<SolutionAttribute>();
            if (solution is not null)
            {
                var result = (string?)method.Invoke(null, new object[] { GetInput(solution.Year, solution.Day, runExample) });
                EvaluateResult(year, day, part, result);
            }

        }
    }


    private static bool IsSolution(Type t) => t.GetCustomAttribute<SolutionAttribute>() is not null;

    private static bool CorrectYear(Type t, int? year)
    {
        if (year is null)
        {
            return true;
        }

        var attr = t.GetCustomAttribute<SolutionAttribute>();

        return attr!.Year == year;
    }

    private static bool CorrectDay(Type t, int? day)
    {
        if (day is null)
        {
            return true;
        }

        var attr = t.GetCustomAttribute<SolutionAttribute>();

        return attr!.Day == day;
    }

    private static string GetInput(int year, int day, bool runExample)
    {
        var extension = runExample ? "example" : "in";
        return File.ReadAllText($"{DataDir}/{year}/Day{day}.{extension}");
    }

    private static void EvaluateResult(int year, int day, int part, string? result)
    {
        Console.WriteLine(result);
        return;
    }

    private static void RunSolution(Type solution, int year, int day, int? part, bool runExample)
    {
        var solutionInfo = solution.GetCustomAttribute<SolutionAttribute>();
        year ??= solutionInfo!.Year;
        day ??= solutionInfo!.Day;

        var methods = solution.GetMethods();

        foreach (var method in methods)
        {
            var partAttribute = method.GetCustomAttribute<PartAttribute>();
            if (partAttribute is null)
            {
                return;
            }

            var part = partAttribute.Part;

            var result = (string?)method.Invoke(null, new object[] { GetInput(year, day, runExample) });
            EvaluateResult(year, day, part, result);
        }
    }
}
