using System;

namespace DotnetAsyncThreads.Examples;

public static class AsyncExample
{
    public static async Task RunAsync()
    {
        List<Task> tasks = [];

        for (int i = 0; i < 20; i++)
        {
            int taskId = i; // Capture the loop variable
            tasks.Add(LongRunningCode.LongRunningOperationAsync(taskId));
        }

        await Task.WhenAll(tasks);
    }
}
