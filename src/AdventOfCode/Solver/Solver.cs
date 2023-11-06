namespace AdventOfCode.Solver;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public enum SolutionResult
{
    Correct,
    Wrong,
    CorrectAnswerNotAvailable,
}

public class SolutionMethod
{
    private const string DataDir = "../../data";

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

    public SolutionResult CheckAnswer(string answer)
    {
        var correctAnswerFilePath = $"{DataDir}/{Year}/Day{Day}.out";
        if (!File.Exists(correctAnswerFilePath))
        {
            return SolutionResult.CorrectAnswerNotAvailable;
        }

        var correctAnswer = File.ReadAllText(correctAnswerFilePath);
        return answer == correctAnswer ? SolutionResult.Correct : SolutionResult.Wrong;
    }

    private static string GetInput(int year, int day, bool runExample)
    {
        var extension = runExample ? "example" : "in";
        return File.ReadAllText($"{DataDir}/{year}/Day{day}.{extension}");
    }
}

public static class Solver
{
    public static void RunSolutions(int? year, int? day, int? part, bool runExample)
    {
        Assembly.GetExecutingAssembly().GetTypes()
            .FindAllSolutions()
            .Where(s => year is null || s.Year == year )
            .Where(s => day is null || s.Day == day )
            .Where(s => part is null || s.Part == part )
            .EvaluateSolutions(runExample);
    }

    private static void EvaluateSolutions(this IEnumerable<SolutionMethod> solutions, bool runExample)
    {
        foreach (var solution in solutions)
        {
            string answer = solution.Run(runExample);

            Console.Write($"{solution.Year}, Day {solution.Day}, Part {solution.Part}: {answer} ");

            if (runExample)
            {
                Console.WriteLine();
                continue;
            }

            var result = solution.CheckAnswer(answer);
            switch (result)
            {
                case SolutionResult.Correct:
                    WriteLineWithColor("+", ConsoleColor.Green);
                    break;
                case SolutionResult.Wrong:
                    WriteLineWithColor("-", ConsoleColor.Red);
                    break;
                case SolutionResult.CorrectAnswerNotAvailable:
                    Console.WriteLine("??");
                    break;
            }
        }
    }

    private static void WriteLineWithColor(string value, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(value);
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
