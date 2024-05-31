using System.Text.Json;
using KafkaFlow;

namespace KafkaFlowConsumer;

public class WeatherMessageBatchManualConsumer(ILogger<WeatherMessageBatchConsumer> logger) : IMessageMiddleware
{
    public Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        var batch = context.GetMessagesBatch();
        string batchInfo = LogHelper.MessageBatchInfo(batch);

        logger.LogInformation("Batch received - {batchInfo}", batchInfo);

        int count = 0;

        foreach (var message in batch)
        {
            count++;

            logger.LogDebug("Message - {info}, workerId: {workerId}", LogHelper.MessageDetails(message), message.ConsumerContext.WorkerId);

            if (count > batch.Count - 3)
                throw new Exception("Oooops!");

            message.ConsumerContext.Complete();
        }

        logger.LogInformation("Batch completed - {batchInfo}", batchInfo);

        return Task.CompletedTask;
    }
}