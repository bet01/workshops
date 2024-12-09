using System;

namespace DotnetAsyncThreads.Examples;

public static class ThreadExample
{
    public static void Run()
    {
        List<Thread> threads = [];

        for (int i = 0; i < 5; i++)
        {
            int taskId = i; // Capture the loop variable
            var thread = new Thread(() => LongRunningCode.LongRunningOperation(taskId));
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
