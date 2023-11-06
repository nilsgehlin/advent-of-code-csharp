namespace AdventOfCode;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class Solver
{
    public static void RunSolutions(int? year, int? day, int? part, bool runExample)
    {
        Assembly.GetExecutingAssembly().GetTypes()
            .FindAllSolutions()
            .Where(s => year is null || s.Year == year)
            .Where(s => day is null || s.Day == day)
            .Where(s => part is null || s.Part == part)
            .EvaluateSolutions(runExample);
    }

    private static void EvaluateSolutions(this IEnumerable<SolutionMethod> solutions, bool runExample)
    {
        foreach (var solution in solutions)
        {
            string answer = solution.Run(runExample);

            Console.Write($"{solution.Year}, Day {solution.Day}, Part {solution.Part}: ");

            if (runExample)
            {
                Console.WriteLine(answer);
                continue;
            }

            var (result, correctAnswer) = solution.CheckAnswer(answer);
            switch (result)
            {
                case SolutionResult.Correct:
                    WriteWithColor($"{answer} +", ConsoleColor.Green);
                    Console.WriteLine();
                    break;
                case SolutionResult.Wrong:
                    WriteWithColor($"{answer} -", ConsoleColor.Red);
                    Console.WriteLine($" ({correctAnswer})");
                    break;
                case SolutionResult.CorrectAnswerNotAvailable:
                    Console.WriteLine($"{answer} ??");
                    break;
            }
        }
    }

    private static void WriteWithColor(string value, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(value);
        Console.ResetColor();
    }

    private static IEnumerable<SolutionMethod> FindAllSolutions(this IEnumerable<Type> types)
    {
        var assemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var assemblyType in assemblyTypes)
        {
            var solutionAttr = assemblyType.GetCustomAttribute<SolutionAttribute>();
            if (solutionAttr is null)
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

                yield return new SolutionMethod(
                    solutionAttr.Year, solutionAttr.Day, partAttribute.Part, method);
            }
        }
    }

}
