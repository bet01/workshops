using System;

namespace DotnetAsyncThreads.Examples;

public static class SequentialExample
{
    public static void Run()
    {
        for (int i = 0; i < 3; i++)
        {
            LongRunningCode.LongRunningIOOperation(i);
        }
    }
}
