using System.Text;
using System.Text.Json;
using KafkaFlow;

namespace KafkaFlowConsumer;

public class LogHelper
{
    public static string MessageBatchInfoMixed(IReadOnlyCollection<IMessageContext> batch)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("Count: {0} - [Partiiton, Offset]", batch.Count);
        _ = batch.Aggregate(
                            stringBuilder,
                            (sb, x) => sb.AppendFormat(", [{0}, {1}]", x.ConsumerContext.Partition, x.ConsumerContext.Offset)
                        );

        return stringBuilder.ToString();
    }

    public static string MessageBatchInfoSamePartition(IReadOnlyCollection<IMessageContext> batch)
    {
        var first = batch.First().ConsumerContext;
        var last = batch.Last().ConsumerContext;

        return string.Format("topic: {0} partition: {1} size: {2} min offset: {3} max offset: {4}",
            first.Topic,
            first.Partition,
            batch.Count,
            first.Offset,
            last.Offset);
    }

    public static string MessageDetails(IMessageContext message)
    {
        var offset = message.ConsumerContext.TopicPartitionOffset;

        return string.Format("topic: {0} partition: {1} offset: {2} json: {3}",
            offset.Topic,
            offset.Partition,
            offset.Offset,
            JsonSerializer.Serialize(message.Message));
    }
}