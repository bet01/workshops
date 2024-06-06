
using KafkaFlow;
using KafkaFlow.Configuration;
using KafkaFlow.Consumers.DistributionStrategies;
using KafkaFlow.Serializer;

namespace KafkaFlowConsumer;

public static class KafkaConfigExtensions
{
    public static void ConfigureKafka(this IKafkaConfigurationBuilder x, KafkaConfig kafkaConfig)
    {
        x
        .UseMicrosoftLog()
        .AddCluster(cluster => cluster
            .WithBrokers([kafkaConfig.Host])
            .CreateTopicIfNotExists(kafkaConfig.Weather.Topic, 3, 1)
            .AddConsumer(consumer => consumer
                .Topic(kafkaConfig.Weather.Topic)                
                .WithGroupId(kafkaConfig.ConsumerGroup)
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
}