namespace AdventOfCode;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class SolutionMethod
{
    public SolutionMethod(int year, int day, int part, MethodInfo method)
    {
        Year = year;
        Day = day;
        Part = part;
        Method = method;
    }

    public int Year { get; }

    public int Day { get; }

    public int Part { get; }

    public MethodInfo Method { get; }

    public string Run(bool runExample)
    {
        var args = new string[] { GetInput(Year, Day, runExample) };
        var result = (string?)Method.Invoke(null, args) ??
            throw new Exception("Solution returned null, which is not legal");
        return result;
    }

    private static string GetInput(int year, int day, bool runExample)
    {
        var extension = runExample ? "example" : "in";
        return File.ReadAllText($"{DataDir}/{year}/Day{day}.{extension}");
    }
}

public static class Solver
{
    private const string DataDir = "../../data";

    public static void RunSolutions(int? year, int? day, int? part, bool runExample)
    {
        Assembly.GetExecutingAssembly().GetTypes()
            .FindSolutionsToRun(year, day, part)
            .EvaluateSolutions(runExample);
    }

    private static void EvaluateSolutions(this IEnumerable<SolutionMethod> solutions, bool runExample)
    {
        foreach (var solution in solutions)
        {
            string result = RunSolutionMethod(runExample, solution);

            Console.Write($"{solution.Year}, Day {solution.Day}, Part {solution.Part}: {result} ");

            if (runExample)
            {
                Console.WriteLine();
                continue;
            }

            var answerFilePath = $"{DataDir}/{solution.Year}/Day{solution.Day}.out";
            if (!File.Exists(answerFilePath))
            {
                Console.WriteLine("??");
                continue;
            }

            var answer = File.ReadAllText(answerFilePath);
            if (answer == result)
            {
                WriteLineWithColor("+", ConsoleColor.Green);
            }
            else
            {
                WriteLineWithColor("-", ConsoleColor.Red);
            }
        }
    }

    private static void WriteLineWithColor(string value, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(value);
        Console.ResetColor();
    }


    private static IEnumerable<SolutionMethod> FindSolutionsToRun(
        this IEnumerable<Type> types,
        int? year,
        int? day,
        int? part)
    {
        var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

        foreach (var assemblyType in assemblyTypes)
        {
            var solutionAttr = assemblyType.GetCustomAttribute<SolutionAttribute>();
            if (solutionAttr is null)
            {
                continue;
            }

            if (year is not null && year != solutionAttr.Year)
            {
                continue;
            }

            if (day is not null && day != solutionAttr.Day)
            {
                continue;
            }

            var methods = assemblyType.GetMethods();
            foreach (var method in methods)
            {
                var partAttribute = method.GetCustomAttribute<PartAttribute>();
                if (partAttribute is null)
                {
                    continue;
                }

                if (part is not null && part != partAttribute.Part)
                {
                    continue;
                }

                yield return new SolutionMethod(
                    solutionAttr.Year, solutionAttr.Day, partAttribute.Part, method);
            }
        }
    }

}
