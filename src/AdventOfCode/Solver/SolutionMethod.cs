namespace AdventOfCode.Solver;

using System;
using System.IO;
using System.Reflection;

public class SolutionMethod(int year, int day, int part, MethodInfo method)
{
    private const string DataDir = "../../data";

    public int Year { get; } = year;

    public int Day { get; } = day;

    public int Part { get; } = part;

    public MethodInfo Method { get; } = method;

    public string Run(bool runExample)
    {
        var args = new string[] { GetInput(Year, Day, runExample) };
        var result = Method.Invoke(null, args)?.ToString()
                     ?? throw new Exception("Solution returned null, which is not legal");
        return result;
    }

    public (SolutionResult result, string? correctAnswer) CheckAnswer(string answer)
    {
        var correctAnswerFilePath = $"{DataDir}/{Year}/Day{Day}.out";
        if (!File.Exists(correctAnswerFilePath))
        {
            return (SolutionResult.CorrectAnswerNotAvailable, null);
        }

        var correctAnswers = File.ReadAllText(correctAnswerFilePath).Split(Environment.NewLine);
        if (correctAnswers.Length < Part)
        {
            return (SolutionResult.CorrectAnswerNotAvailable, null);
        }

        var correctAnswer = correctAnswers[Part - 1];
        var result = answer == correctAnswer ? SolutionResult.Correct : SolutionResult.Wrong;
        return (result, correctAnswer);
    }

    private static string GetInput(int year, int day, bool runExample)
    {
        var extension = runExample ? "example" : "in";
        return File.ReadAllText($"{DataDir}/{year}/Day{day}.{extension}");
    }
}
