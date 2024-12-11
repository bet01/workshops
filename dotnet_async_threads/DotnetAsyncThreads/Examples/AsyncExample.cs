using System;

namespace DotnetAsyncThreads.Examples;

public static class AsyncExample
{
    public static async Task RunIOAsync()
    {
        List<Task> tasks = [];

        for (int i = 0; i < ExampleSettings.LoopCountIO; i++)
        {
            int taskId = i; // Capture the loop variable
            tasks.Add(LongRunningCode.LongRunningIOOperationAsync(taskId));
        }

        await Task.WhenAll(tasks);
    }

    public static async Task RunCPUAsync()
    {
        List<Task> tasks = [];

        for (int i = 0; i < ExampleSettings.LoopCountCPU; i++)
        {
            int taskId = i; // Capture the loop variable
            tasks.Add(LongRunningCode.LongRunningCPUOperationAsync(taskId));
        }

        await Task.WhenAll(tasks);
    }
}
