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
        string topic = consumerContext.Topic;
        int partition = consumerContext.Partition;
        string batchInfo = string.Format("topic: {0} partition: {1} size: {2} min offset: {3} max offset: {4}",
            topic,
            partition,
            batch.Count,
            batch.First().ConsumerContext.Offset,
            batch.Last().ConsumerContext.Offset);

        _logger.LogInformation("Batch received - {batchInfo}", batchInfo);

        foreach (var messageContext in batch)
        {
            long offset = messageContext.ConsumerContext.TopicPartitionOffset.Offset;
            _logger.LogDebug("Message - topic: {topic} partition: {partition} offset: {offset} json: {json}",
                topic,
                partition,
                offset,
                JsonSerializer.Serialize(messageContext.Message));
            messageContext.ConsumerContext.Complete();
        }

        _logger.LogInformation("Batch completed - {batchInfo}", batchInfo);

        return Task.CompletedTask;
    }
}