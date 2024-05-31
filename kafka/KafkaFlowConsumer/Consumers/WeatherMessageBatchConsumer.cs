using System.Text.Json;
using KafkaFlow;

namespace KafkaFlowConsumer;

public class WeatherMessageBatchConsumer(ILogger<WeatherMessageBatchConsumer> logger) : IMessageMiddleware
{
    ILogger<WeatherMessageBatchConsumer> _logger = logger;

    public Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        var batch = context.GetMessagesBatch();
        string batchInfo = LogHelper.MessageBatchInfo(batch);

        _logger.LogInformation("Batch received - {batchInfo}", batchInfo);

        foreach (var message in batch)
        {
            _logger.LogDebug("Message - {info}, workerId: {workerId}", LogHelper.MessageDetails(message), message.ConsumerContext.WorkerId);
            //message.ConsumerContext.Complete();
        }

        //batch.Last().ConsumerContext.Complete();
        batch.First().ConsumerContext.Complete();

        _logger.LogInformation("Batch completed - {batchInfo}", batchInfo);

        return Task.CompletedTask;
    }
}