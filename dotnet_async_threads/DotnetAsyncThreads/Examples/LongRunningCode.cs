using System;

namespace DotnetAsyncThreads.Examples;

public static class LongRunningCode
{
    public static void LongRunningIOOperation(int id)
    {
        Console.WriteLine($"IO Task {id} started (Thread ID: {Environment.CurrentManagedThreadId})");
        // Simulate a long running operation
        Thread.Sleep(500);
        Console.WriteLine($"IO Task {id} completed (Thread ID: {Environment.CurrentManagedThreadId})");
    }

    public static async Task LongRunningIOOperationAsync(int id)
    {
        Console.WriteLine($"IO Task {id} started asynchronously (Thread ID: {Environment.CurrentManagedThreadId})");
        // Simulate a long running operation
        await Task.Delay(500);
        Console.WriteLine($"IO Task {id} completed asynchronously (Thread ID: {Environment.CurrentManagedThreadId})");
    }

    public static void LongRunningCPUOperation(int id)
    {
        Console.WriteLine($"CPU Task {id} started (Thread ID: {Environment.CurrentManagedThreadId})");
        // CPU intensive operation
        CalculateMandelbrotSet();

        Console.WriteLine($"CPU Task {id} completed (Thread ID: {Environment.CurrentManagedThreadId})");
    }

    public static async Task LongRunningCPUOperationAsync(int id)
    {
        Console.WriteLine($"CPU Task {id} started asynchronously (Thread ID: {Environment.CurrentManagedThreadId})");

        // CPU intensive operation
        // NOTE! There is no await for CPU intensive code without using Task.Run which then would make this example Multithreading.
        // So this example is effectively sequential as there is no await or thread.
        CalculateMandelbrotSet();

        Console.WriteLine($"CPU Task {id} completed asynchronously (Thread ID: {Environment.CurrentManagedThreadId})");
    }

    private static int[,] CalculateMandelbrotSet()
    {
        int width = 800;
        int height = 800;
        int maxIterations = 1000;

        int[,] result = new int[width, height];
        double xmin = -2.5, xmax = 1.0, ymin = -1.0, ymax = 1.0;

        for (int px = 0; px < width; px++)
        {
            for (int py = 0; py < height; py++)
            {
                // Map pixel coordinate to complex plane
                double x0 = xmin + (xmax - xmin) * px / width;
                double y0 = ymin + (ymax - ymin) * py / height;

                double x = 0, y = 0;
                int iteration = 0;

                // Iterative function: Z(n+1) = Z(n)^2 + C
                while (x * x + y * y <= 4 && iteration < maxIterations)
                {
                    double xtemp = x * x - y * y + x0;
                    y = 2 * x * y + y0;
                    x = xtemp;
                    iteration++;
                }

                result[px, py] = iteration;
            }
        }

        return result;
    }
}
