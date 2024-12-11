using System;
using BenchmarkDotNet.Attributes;
using DotnetAsyncThreads.Examples;

namespace DotnetAsyncThreads;

[CategoriesColumn]
[MemoryDiagnoser]
public class BenchmarkThreadsAsync
{
    [BenchmarkCategory("I/O"), Benchmark]
    public void ThreadsIOBenchmark() => ThreadExample.RunIO();

    [BenchmarkCategory("I/O"), Benchmark]
    public async Task AsyncIOBenchmark() => await AsyncExample.RunIOAsync();

    [BenchmarkCategory("CPU"), Benchmark]
    public void ThreadsCPUBenchmark() => ThreadExample.RunCPU();

    [BenchmarkCategory("CPU"), Benchmark]
    public async Task AsyncCPUBenchmark() => await AsyncExample.RunCPUAsync();
}