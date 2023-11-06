namespace AdventOfCode;

using System;
using System.CommandLine;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var yearOption= new Option<int?>
            (name: "--year",
            description: "Filter solutions on year",
            getDefaultValue: () => null);
        yearOption.AddAlias("-y");

        var dayOption= new Option<int?>
            (name: "--day",
            description: "Filter solutions on day",
            getDefaultValue: () => null);
        dayOption.AddAlias("-d");

        var partOption= new Option<int?>
            (name: "--part",
            description: "Filter solutions on part",
            getDefaultValue: () => null);
        partOption.AddAlias("-p");

        var runExampleOption= new Option<bool>
            (name: "--run-example",
            description: "Run example data instead of standard .in-files",
            getDefaultValue: () => false);
        runExampleOption.AddAlias("-e");

        var rootCommand = new RootCommand("Advent Of Code solver");
        rootCommand.AddOption(yearOption);
        rootCommand.AddOption(dayOption);
        rootCommand.AddOption(partOption);
        rootCommand.AddOption(runExampleOption);

        rootCommand.SetHandler((year, day, part, runExample) =>
        {
            Solver.RunSolutions(year, day, part, runExample);
        },
        yearOption, dayOption, partOption, runExampleOption);

        await rootCommand.InvokeAsync(args);
    }
}