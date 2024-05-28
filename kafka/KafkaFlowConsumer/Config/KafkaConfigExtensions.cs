
using KafkaFlow;
using KafkaFlow.Configuration;
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
            .AddConsumer(consumer => consumer
                .Topic(kafkaConfig.Weather.Topic)
                .WithGroupId(kafkaConfig.ConsumerGroup)
                .WithAutoOffsetReset(AutoOffsetReset.Earliest)
                .WithManualMessageCompletion()
                .WithBufferSize(kafkaConfig.BatchMessageLimit)
                .WithWorkersCount(10)
                .AddMiddlewares(middlewares => middlewares
                    .AddSingleTypeDeserializer<WeatherMessage, JsonCoreDeserializer>()
                    .AddBatching(kafkaConfig.BatchMessageLimit, TimeSpan.FromMilliseconds(kafkaConfig.BatchTimeLimit))
                    .Add<WeatherMessageBatchConsumer>()
                )
            )
        );
    }
}