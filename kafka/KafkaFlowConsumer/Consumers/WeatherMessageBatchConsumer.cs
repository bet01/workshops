using KafkaFlow;
using Polly;

namespace KafkaFlowConsumer;

public class WeatherMessageBatchConsumer(ILogger<WeatherMessageBatchConsumer> logger, [FromKeyedServices("retry-backoff")] ResiliencePipeline retry) : IMessageMiddleware
{
    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        var batch = context.GetMessagesBatch();
        string batchInfo = LogHelper.MessageBatchInfoMixed(batch);

        logger.LogInformation("Batch received - {batchInfo}", batchInfo);

        foreach (var message in batch)
        {
            await retry.ExecuteAsync(async token =>
            {
                logger.LogDebug("Message - {info}, workerId: {workerId}", LogHelper.MessageDetails(message), message.ConsumerContext.WorkerId);
                throw new Exception("OOOPS!");
                message.ConsumerContext.Complete();
            });
        }

        logger.LogInformation("Batch completed - {batchInfo}", batchInfo);

        //return Task.CompletedTask;
    }
}