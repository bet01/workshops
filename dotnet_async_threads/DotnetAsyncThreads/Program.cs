using System.Diagnostics;
using BenchmarkDotNet.Running;
using DotnetAsyncThreads;
using DotnetAsyncThreads.Examples;

Console.WriteLine(@"Async vs Threads Demo
----------------------
Run with: `dotnet run -c Release`
----------------------
Please selection option:
1. Run IO Thread Example
2. Run IO Async Example
3. Run Benchmark (RELEASE MODE ONLY!) Includes I/O and CPU benchmarks
");

var key = Console.ReadKey();

switch (key.Key)
{
    case ConsoleKey.D1:
        RunIOThreads();
        break;
    case ConsoleKey.D2:
        await RunIOAsync();
        break;
    case ConsoleKey.D3:
        BenchmarkRunner.Run<BenchmarkThreadsAsync>();
        break;
}

Console.WriteLine("Demo complete!");

void RunIOThreads()
{
    Console.WriteLine("Running tasks with threads...");
    var stopwatch = Stopwatch.StartNew();
    ThreadExample.RunIO();
    stopwatch.Stop();
    Console.WriteLine($"Thread execution time: {stopwatch.ElapsedMilliseconds} ms\n");
}

async Task RunIOAsync()
{
    Console.WriteLine("Running tasks asynchronously...");
    var stopwatch = Stopwatch.StartNew();
    await AsyncExample.RunIOAsync();
    stopwatch.Stop();
    Console.WriteLine($"Async execution time: {stopwatch.ElapsedMilliseconds} ms\n");
}