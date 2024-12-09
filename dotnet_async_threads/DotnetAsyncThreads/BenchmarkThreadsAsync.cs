using System;
using BenchmarkDotNet.Attributes;
using DotnetAsyncThreads.Examples;

namespace DotnetAsyncThreads;

[MemoryDiagnoser]
public class BenchmarkThreadsAsync
{
    [Benchmark]
    public void ThreadsBenchmark() => ThreadExample.Run();

    [Benchmark]
    public async Task AsyncBenchmark() => await AsyncExample.RunAsync();
}