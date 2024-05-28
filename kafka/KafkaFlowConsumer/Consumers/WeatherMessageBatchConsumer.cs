using System.Text.Json;
using KafkaFlow;

namespace KafkaFlowConsumer;

public class WeatherMessageBatchConsumer : IMessageMiddleware
{
    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        var batch = context.GetMessagesBatch();

        Console.WriteLine($"BATCH SIZE: {batch.Count}");

        foreach (var messageContext in batch)
        {
            Console.WriteLine($"MESSAGE FROM BATCH: {JsonSerializer.Serialize(messageContext.Message)}");
        }
    }
}