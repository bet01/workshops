using System;

namespace DotnetAsyncThreads.Examples;

public static class ThreadExample
{
    public static void RunIO()
    {
        List<Thread> threads = [];

        for (int i = 0; i < ExampleSettings.LoopCountIO; i++)
        {
            int taskId = i; // Capture the loop variable
            var thread = new Thread(() => LongRunningCode.LongRunningIOOperation(taskId));
            threads.Add(thread);
            thread.Start();
        }

        // Wait for all threads to complete
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }

    public static void RunCPU()
    {
        List<Thread> threads = [];

        for (int i = 0; i < ExampleSettings.LoopCountCPU; i++)
        {
            int taskId = i; // Capture the loop variable
            var thread = new Thread(() => LongRunningCode.LongRunningCPUOperation(taskId));
            threads.Add(thread);
            thread.Start();
        }

        // Wait for all threads to complete
        foreach (var thread in threads)
        {
            thread.Join();
        }
    }
}
