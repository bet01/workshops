using System.Text.Json;
using KafkaFlow;

namespace KafkaFlowConsumer;

public class WeatherMessageBatchConsumer : IMessageMiddleware
{
    public Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        var batch = context.GetMessagesBatch();

        Console.WriteLine($"BATCH SIZE: {batch.Count}");

        foreach (var messageContext in batch)
        {
            var offset = messageContext.ConsumerContext.TopicPartitionOffset;
            Console.WriteLine($"MESSAGE FROM BATCH: {offset.Topic} {offset.Partition} {offset.Offset} {JsonSerializer.Serialize(messageContext.Message)}");
            messageContext.ConsumerContext.Complete();
        }

        //throw new Exception("OOOPS!");

        return Task.CompletedTask;
    }
}