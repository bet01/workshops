# Multithreading vs Async Programming

It's important to understand the distinction between asynchronous programming and multithreading in .NET.

## Multithreading

When you create threads explicitly (e.g., using Thread), a new thread is allocated each time. Creating threads has significant overhead because each thread requires memory for its stack and incurs additional scheduling overhead. This can quickly lead to resource contention if too many threads are created.

To mitigate this, .NET provides the Thread Pool, which manages a pool of pre-created, reusable threads (called managed threads). The Thread Pool significantly reduces the overhead of thread creation and reuses threads for multiple tasks. However, it's crucial to note that the Thread Pool's benefits are primarily about reducing thread creation overhead—not about limiting the number of threads running concurrently.

If you run x operations in parallel, you will generally have x threads running concurrently, whether they are Thread Pool threads or manually created threads. This is because each parallel task still requires its own execution context to run simultaneously.

Example code:
```C#
var thread = new Thread(() => Thread.Sleep(1000));
thread.Start();
```

> [!NOTE]
> Mutlithreading can be tricky especially when you get into production. It is only recommended to use them in very specific use cases and where someone experienced with multithreading has reviewed the implementation.

### Example App Result

```
Task 4 started (Thread ID: 8)
Task 3 started (Thread ID: 7)
Task 0 started (Thread ID: 4)
Task 2 started (Thread ID: 6)
Task 1 started (Thread ID: 5)
Task 3 completed (Thread ID: 7)
Task 2 completed (Thread ID: 6)
Task 4 completed (Thread ID: 8)
Task 0 completed (Thread ID: 4)
Task 1 completed (Thread ID: 5)
```

Notice how each Task started on it's own thread and completed on the same thread.

## Async

When using asynchronous programming in .NET (or most environments), you are not necessarily creating new threads. A good analogy is JavaScript, which supports asynchronous programming but operates in a single-threaded environment. Asynchronous programming (async) is not the same as multithreading.

Async is particularly useful for I/O-bound operations, such as reading files, calling APIs, or querying databases. Instead of blocking a thread while waiting for the operation to complete, the runtime initiates the I/O operation and then frees the thread to perform other work. When the operation finishes, the runtime schedules the continuation of the task (e.g., your code after await) on an available thread, which could be the same one.

By taking this approach, you could handle multiple I/O-bound operations concurrently, theoretically using only a single thread—just like JavaScript's event loop.

Example code:
```C#
await LongRunningIO();

public async Task LongRunningIO()
{
    // Fake I/O
    await Task.Delay(1000);
}
```

> [!NOTE]
> `Task.Run` schedules work to be executed on a thread from the Thread Pool, rather than creating a completely new thread. It is best suited for CPU-bound  operations where you want to offload work to a background thread. You can then asynchronously wait for the operation to complete using await. This approach simplifies working with threads by abstracting much of the complexity involved in managing them directly.

### Example App Result

```
Task 0 started asynchronously (Thread ID: 1)
Task 1 started asynchronously (Thread ID: 1)
Task 2 started asynchronously (Thread ID: 1)
Task 3 started asynchronously (Thread ID: 1)
Task 4 started asynchronously (Thread ID: 1)
Task 5 started asynchronously (Thread ID: 1)
Task 0 completed asynchronously (Thread ID: 5)
Task 1 completed asynchronously (Thread ID: 12)
Task 2 cmpleted asynchronously (Thread ID: 5)
Task 3 completed asynchronously (Thread ID: 14)
Task 4 completed asynchronously (Thread ID: 12)
Task 5 completed asynchronously (Thread ID: 5)
```

Two key observations from the logs:

- All the asynchronous tasks were initiated from the same thread.
- Some asynchronous tasks completed on the same thread as others. (Note: This is a simplified version of the logs; we ran around 20 tasks to observe this behavior.)

## Conclusion

While the examples in this lab are basic, the key concepts they aim to demonstrate are:
- For CPU-bound operations, use threads to enable concurrent execution and prevent blocking the main thread.
- For I/O-bound operations, use asynchronous programming to avoid blocking threads and achieve efficient concurrency. Async programming is particularly efficient because it can use fewer threads—or even a single thread—by releasing threads back to the thread pool while waiting for I/O operations to complete.

### Benchmarks

I/O Benchmarks
| Method              | Categories | Mean       | Error    | StdDev   | Gen0      | Gen1      | Gen2      | Allocated   |
|-------------------- |----------- |-----------:|---------:|---------:|----------:|----------:|----------:|------------:|
| ThreadsIOBenchmark  | I/O        |   503.4 ms |  0.64 ms |  0.53 ms |         - |         - |         - |    33.05 KB |
| AsyncIOBenchmark    | I/O        |   501.2 ms |  0.38 ms |  0.34 ms |         - |         - |         - |    15.04 KB |

CPU Benchmarks
| Method              | Categories | Mean       | Error    | StdDev   | Gen0      | Gen1      | Gen2      | Allocated   |
|-------------------- |----------- |-----------:|---------:|---------:|----------:|----------:|----------:|------------:|
| ThreadsCPUBenchmark | CPU        |   515.2 ms |  0.85 ms |  0.80 ms | 1000.0000 | 1000.0000 | 1000.0000 |  12511.3 KB |
| AsyncCPUBenchmark   | CPU        | 2,457.6 ms | 18.51 ms | 15.45 ms | 1000.0000 | 1000.0000 | 1000.0000 | 12504.05 KB |

*AsyncCPUBenchmark is literally running sequentially as you cannot await CPU intensive tasks. `Task.Run` will start a thread therefall would fall under multithreading not async.