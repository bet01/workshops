using System.Text.Json;
using KafkaFlow;

namespace KafkaFlowConsumer;

public class WeatherMessageBatchConsumer(ILogger<WeatherMessageBatchConsumer> logger) : IMessageMiddleware
{
    ILogger<WeatherMessageBatchConsumer> _logger = logger;

    public Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        var batch = context.GetMessagesBatch();

        var consumerContext = context.ConsumerContext;
        string topicPartition = $"topic: {consumerContext.Topic} partition: {consumerContext.Partition}";
        _logger.LogInformation("Batch received - size: {0} {1} min offset: {2} max offset: {3}",
            batch.Count,
            topicPartition,
            batch.First().ConsumerContext.Offset,
            batch.Last().ConsumerContext.Offset);

        foreach (var messageContext in batch)
        {
            long offset = messageContext.ConsumerContext.TopicPartitionOffset.Offset;
            Console.WriteLine($"Message - {topicPartition} offset: {offset} {JsonSerializer.Serialize(messageContext.Message)}");
            messageContext.ConsumerContext.Complete();
        }

        return Task.CompletedTask;
    }
}