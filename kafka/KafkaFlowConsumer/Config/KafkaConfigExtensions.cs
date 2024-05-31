
using KafkaFlow;
using KafkaFlow.Configuration;
using KafkaFlow.Consumers.DistributionStrategies;
using KafkaFlow.Serializer;

namespace KafkaFlowConsumer;

public static class KafkaConfigExtensions
{
    public static void ConfigureKafkaPartitionKeyStrategyManualComplete(this IKafkaConfigurationBuilder x, KafkaConfig kafkaConfig)
    {
        x
        .UseMicrosoftLog()
        .AddCluster(cluster => cluster
            .WithBrokers([kafkaConfig.Host])
            .AddConsumer(consumer => consumer
                .Topic(kafkaConfig.Weather.Topic)
                .WithGroupId(kafkaConfig.ConsumerGroup)
                // PartitionKey maintains message order per partition (note ByteSum maintains order per message key)
                .WithWorkerDistributionStrategy<PartitionKeyDistributionStrategy>()
                // New consumers will start at the beginning
                .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                // Message must be manually marked as complete for the offsets to be committed
                .WithManualMessageCompletion()
                // How many messages to buffer
                .WithBufferSize(kafkaConfig.BufferSize)
                .WithWorkersCount(kafkaConfig.Weather.Workers)
                .AddMiddlewares(middlewares => middlewares
                    // All messages in this topic are assumed to be of the same type
                    .AddSingleTypeDeserializer<WeatherMessage, JsonCoreDeserializer>()
                    // Batch messages for batch processing
                    .AddBatching(kafkaConfig.BatchMessageLimit, TimeSpan.FromMilliseconds(kafkaConfig.BatchTimeLimit))
                    // Batch message handler
                    .Add<WeatherMessageBatchConsumer>()
                )
            )
        );
    }

    public static void ConfigureKafkaErrorRestart(this IKafkaConfigurationBuilder x, KafkaConfig kafkaConfig)
    {
        x
        .UseMicrosoftLog()
        .AddCluster(cluster => cluster
            .WithBrokers([kafkaConfig.Host])
            .AddConsumer(consumer => consumer
                .Topic(kafkaConfig.Weather.Topic)
                .WithGroupId(kafkaConfig.ConsumerGroup)
                // New consumers will start at the beginning
                .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                // How many messages to buffer
                .WithBufferSize(kafkaConfig.BufferSize)
                .WithWorkersCount(kafkaConfig.Weather.Workers)
                .AddMiddlewares(middlewares => middlewares
                    // All messages in this topic are assumed to be of the same type
                    .AddSingleTypeDeserializer<WeatherMessage, JsonCoreDeserializer>()
                    // Batch messages for batch processing
                    .AddBatching(kafkaConfig.BatchMessageLimit, TimeSpan.FromMilliseconds(kafkaConfig.BatchTimeLimit))
                    // Handle consumer errors
                    .Add<HandleBatchConsumerError>()
                    // Batch message handler
                    .Add<WeatherMessageBatchConsumer>()
                )
            )
        );
    }

    public static void ConfigureKafkaManualCompleteRetryIncomplete(this IKafkaConfigurationBuilder x, KafkaConfig kafkaConfig)
    {
        x
        .UseMicrosoftLog()
        .AddCluster(cluster => cluster
            .WithBrokers([kafkaConfig.Host])
            .AddConsumer(consumer => consumer
                .Topic(kafkaConfig.Weather.Topic)
                .WithGroupId(kafkaConfig.ConsumerGroup)
                .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                .WithManualMessageCompletion()
                .WithBufferSize(kafkaConfig.BufferSize)
                .WithWorkersCount((context, resolver) => 
                    Task.FromResult(context.AssignedTopicsPartitions.First().Partitions.Count()),
                    TimeSpan.FromMinutes(15))
                .AddMiddlewares(middlewares => middlewares
                    .AddSingleTypeDeserializer<WeatherMessage, JsonCoreDeserializer>()
                    .AddBatching(kafkaConfig.BatchMessageLimit, TimeSpan.FromMilliseconds(kafkaConfig.BatchTimeLimit))
                    .Add<HandleBatchConsumerError>()
                    .Add<WeatherMessageBatchManualConsumer>()
                )
            )
        );
    }
}