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
                    Details = LogHelper.MessageBatchInfoMixed(batch)
                });

            var consumer = consumerAccessor[context.ConsumerContext.ConsumerName];
            // Pause the consumer on error
            consumer.Pause(consumer.Assignment);

            logHandler.Warning("Consumer restarted", context.ConsumerContext.ConsumerName);
        }
    }
}