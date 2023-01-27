using Prometheus;

namespace WeatherAPI
{
    public class AppMetrics
    {
        public static readonly Counter WeatherRequestCount = Metrics
            .CreateCounter("weather_request_total", "Number of weather api calls.");

        public static readonly Gauge LastRequestDuration = Metrics
            .CreateGauge("weather_last_request_duration", "Duration of last request.");

        public static readonly Histogram CallDuration = Metrics
            .CreateHistogram("weather_request_duration", "Histogram of weather api call duration.");
    }
}