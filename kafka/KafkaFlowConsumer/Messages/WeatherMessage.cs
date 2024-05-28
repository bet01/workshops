namespace KafkaFlowConsumer;

public class WeatherMessage
{
    public DateTime Date { get; init; }
    public int TemperatureC { get; init; }
    public int TemperatureF { get; init; }
    public string? Summary { get; init; }
}