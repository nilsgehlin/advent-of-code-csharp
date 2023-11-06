namespace AdventOfCode;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
