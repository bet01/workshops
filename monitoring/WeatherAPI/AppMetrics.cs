using Prometheus;

namespace WeatherAPI
{
    public class AppMetrics
    {
        public static readonly Counter WeatherRequestCount = Metrics
            .CreateCounter("weather_request_total", "Number of weather api calls.");
    }
}