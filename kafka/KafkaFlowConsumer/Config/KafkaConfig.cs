namespace KafkaFlowConsumer;

public class KafkaConfig
{
    public required string Host { get; init; }
    public required string ConsumerGroup { get; init; }
    public required KafkaTopicConfig Weather { get; init; }
    public required int BatchMessageLimit { get; init; }
    public required int BatchTimeLimit { get; init; }
    public required int BufferSize { get; init; }
}

public class KafkaTopicConfig
{
    public required string Topic { get; init; }
    public bool Enabled { get; init; }
    public int Workers { get; init; }
}