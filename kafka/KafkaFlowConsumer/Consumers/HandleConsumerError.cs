using KafkaFlow;
using KafkaFlow.Consumers;

namespace KafkaFlowConsumer;

public class HandleBatchConsumerError(IConsumerAccessor consumerAccessor, ILogHandler logHandler) : IMessageMiddleware
{
    public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var batch = context.GetMessagesBatch();
            logHandler.Error("Error handling message", exception,
                new
                {
                    context.ConsumerContext.ConsumerName,
                    context.ConsumerContext.Topic,
                    context.ConsumerContext.Partition,
                    MinOffset = batch.First().ConsumerContext.Offset,
                    MaxOffset = batch.Last().ConsumerContext.Offset
                });

            var consumer = consumerAccessor[context.ConsumerContext.ConsumerName];
            // !!! NOT FUNCTIONING AS EXPECTED eventually loses partitions completely !!!
            // Restart the consumer on error so we do not skip messages. Partial offset commit may still work. Is there a better way of doing this?
            await consumer.RestartAsync();

            logHandler.Warning("Consumer restarted", context.ConsumerContext.ConsumerName);
        }
    }
}