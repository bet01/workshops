using System.Text.Json;
using KafkaFlow;

namespace KafkaFlowConsumer;

public class WeatherMessageBatchConsumer(ILogger<WeatherMessageBatchConsumer> logger) : IMessageMiddleware
{
    public Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        var batch = context.GetMessagesBatch();
        string batchInfo = LogHelper.MessageBatchInfo(batch);

        logger.LogInformation("Batch received - {batchInfo}", batchInfo);

        foreach (var message in batch)
        {
            bool success = false;

            /* With this loop we never want to move on until a message is complete. 
            This will ensure messages are not missed. This is OK to handle infrastrucutre issues, such as DB connections, timeouts, etc.
            However it can add load if no back-off retry strategy is in place (check out Polly). You may also end up having to manually tell Kafka to skip 
            messages if there is a legitimate message issue, like a bad messaged, etc. */
            do
            {
                try
                {
                    logger.LogDebug("Message - {info}, workerId: {workerId}", LogHelper.MessageDetails(message), message.ConsumerContext.WorkerId);
                    success = true;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Exception occured while processing message - {batchInfo} offset: {offset}",
                    batchInfo,
                    message.ConsumerContext.Offset);
                }
            } while (!success);

            message.ConsumerContext.Complete();
        }

        logger.LogInformation("Batch completed - {batchInfo}", batchInfo);

        return Task.CompletedTask;
    }
}