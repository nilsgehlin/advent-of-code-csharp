namespace AdventOfCode;

using System;
using System.IO;
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
