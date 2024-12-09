using System;

namespace DotnetAsyncThreads.Examples;

public static class LongRunningCode
{
    public static void LongRunningOperation(int id)
    {
        Console.WriteLine($"Task {id} started (Thread ID: {Environment.CurrentManagedThreadId})");
        // Simulate a long running operation
        Thread.Sleep(500); 
        Console.WriteLine($"Task {id} completed (Thread ID: {Environment.CurrentManagedThreadId})");
    }

    public static async Task LongRunningOperationAsync(int id)
    {
        Console.WriteLine($"Task {id} started asynchronously (Thread ID: {Environment.CurrentManagedThreadId})");
        // Simulate a long running operation
        await Task.Delay(500);
        Console.WriteLine($"Task {id} completed asynchronously (Thread ID: {Environment.CurrentManagedThreadId})");
    }
}
