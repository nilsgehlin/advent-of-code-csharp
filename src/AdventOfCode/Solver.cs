namespace AdventOfCode;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

public class Solver
{
    private const string DataDir = "../../data";

    public static void RunSolutions() =>
        Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<SolutionAttribute>() is not null)
            .ToList().ForEach(RunSolution);

    private static string GetInput(int year, int day)
    {
        // File.ReadAllText($"{DataDir}");
        return string.Empty;
    }

    private static void EvaluateResult(int year, int day, int part, string? result)
    {
        return;
    }

    private static void RunSolution(Type solution)
    {
        var solutionInfo = solution.GetCustomAttribute<SolutionAttribute>();
        var year = solutionInfo!.Year;
        var day = solutionInfo!.Day;

        var methods = solution.GetMethods();

        foreach (var method in methods)
        {
            var partAttribute = method.GetCustomAttribute<PartAttribute>();
            if (partAttribute is null)
            {
                return;
            }

            var part = partAttribute.Part;

            var result = (string?)method.Invoke(null, new object[] { GetInput(year, day) });
            EvaluateResult(year, day, part, result);
        }
    }
}
