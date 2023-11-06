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

    public static void RunSolutions(int? year, int? day, int? part, bool runExample)
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

                var extension = runExample ? "example" : "in";
                var input = File.ReadAllText($"{DataDir}/{solutionAttr.Year}/Day{solutionAttr.Day}.{extension}");
                var args = new string[] { input };
                var result = (string?)method.Invoke(null, args);

                Console.Write($"{solutionAttr.Year}, Day {solutionAttr.Day}, Part {partAttribute.Part}: {result} ");

                if (runExample)
                {
                    Console.WriteLine();
                    continue;
                }

                var answerFilePath = $"{DataDir}/{solutionAttr.Year}/Day{solutionAttr.Day}.out";
                if (!File.Exists(answerFilePath))
                {
                    Console.WriteLine("??");
                    continue;
                }

                var answer = File.ReadAllText(answerFilePath);
                if (answer == result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("+");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-");
                    Console.ResetColor();
                }
            }
        }
    }
}
