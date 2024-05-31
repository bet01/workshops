using KafkaFlow;

namespace KafkaFlowConsumer;

public class WeatherMessageConsumer(ILogger<WeatherMessageConsumer> logger) : IMessageHandler<WeatherMessage>
{
    public Task Handle(IMessageContext context, WeatherMessage message)
    {


        //logger.LogDebug("Non-Batch Message: {info} worker: {workerId}", LogHelper.MessageDetails(context), context.ConsumerContext.WorkerId);
        //context.ConsumerContext.Complete();

        logger.LogDebug("Non-Batch Message - partition: {partition} offset: {offset} workerId: {workerId} match: {match}",
            context.ConsumerContext.Partition,
            context.ConsumerContext.Offset,
            context.ConsumerContext.WorkerId,
            (context.Message.Value == message).ToString()
        );

        return Task.CompletedTask;
    }
}