using Confluent.Kafka;

namespace Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    // Kafka stuff
    ConsumerConfig _consumerConfig = new ConsumerConfig
    {
        BootstrapServers = "127.0.0.1:29092",
        GroupId = "my-consumer-group",
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoCommit = false
    };
    
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build())
            {
                consumer.Subscribe("weather");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume(stoppingToken);
                    _logger.LogInformation(consumeResult.Message.Value);
                    //consumer.Commit(consumeResult);
                }

                consumer.Close();
            }
        }
    }
}
