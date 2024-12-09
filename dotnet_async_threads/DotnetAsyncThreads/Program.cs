using System.Diagnostics;
using BenchmarkDotNet.Running;
using DotnetAsyncThreads;
using DotnetAsyncThreads.Examples;

Console.WriteLine(@"Async vs Threads Demo
----------------------
Run with: `dotnet run -c Release`
----------------------
Please selection option:
1. Run all
2. Run Sequential Example
3. Run Thread Example
4. Run Async Example
5. Run Benchmark (RELEASE MODE ONLY!)
");

var key = Console.ReadKey();

switch (key.Key)
{
    case ConsoleKey.D1:
        RunSequential();
        RunThreads();
        await RunAsync();
        break;
    case ConsoleKey.D2:
        RunSequential();
        break;
    case ConsoleKey.D3:
        RunThreads();
        break;
    case ConsoleKey.D4:
        await RunAsync();
        break;
    case ConsoleKey.D5:
        BenchmarkRunner.Run<BenchmarkThreadsAsync>();
        break;
}

Console.WriteLine("Demo complete!");

void RunSequential()
{
    Console.WriteLine("Running tasks sequentially...");
    var stopwatch = Stopwatch.StartNew();
    SequentialExample.Run();
    stopwatch.Stop();
    Console.WriteLine($"Sequential execution time: {stopwatch.ElapsedMilliseconds} ms\n");
}

void RunThreads()
{
    Console.WriteLine("Running tasks with threads...");
    var stopwatch = Stopwatch.StartNew();
    ThreadExample.Run();
    stopwatch.Stop();
    Console.WriteLine($"Thread execution time: {stopwatch.ElapsedMilliseconds} ms\n");
}

async Task RunAsync()
{
    Console.WriteLine("Running tasks asynchronously...");
    var stopwatch = Stopwatch.StartNew();
    await AsyncExample.RunAsync();
    stopwatch.Stop();
    Console.WriteLine($"Async execution time: {stopwatch.ElapsedMilliseconds} ms\n");
}