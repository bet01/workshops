using KafkaFlowConsumer;
using KafkaFlow;
using System.Text.Json;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

// Configure Kafka with MassTransit
var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<KafkaConfig>() ?? throw new Exception("Kafka config section not found");
builder.Services.AddKafka(kafka => kafka.ConfigureKafka(kafkaConfig));

// Configure Json to convert between PascalCase and camelCase
var jsonOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};
builder.Services.AddSingleton(jsonOptions);

// Configure SeriLog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Build app host
var host = builder.Build();

// Start Kafka
var kafkaBus = host.Services.CreateKafkaBus();
var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStarted.Register(() => kafkaBus.StartAsync(lifetime.ApplicationStopped));

// Run host
host.Run();
