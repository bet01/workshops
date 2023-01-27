# Monitoring Workshop

## Why Monitor?

When something goes wrong, how do you know? When something goes right, how do you know it's better? Monitoring tools give us these insights to see problems early and to be able to prove the effectiveness of improvments.

## Getting Started

Create a simple boilerplate dotnet API with `dotnet new webapi --name WeatherAPI`

## Prometheus

### What is it?

Prometheus is a systems and service monitoring system. It collects metrics from configured targets at given intervals, evaluates rule expressions, displays the results, and can trigger alerts when specified conditions are observed.

### Let's code

Add the prometheus nuget packages to your project 

`dotnet add package prometheus-net --version 7.0.0`

`dotnet add package prometheus-net.AspNetCore --version 7.0.0`

Add the following to your Program.cs file just before `app.Run();`:

```
app.UseRouting();

// Capture metrics about all received HTTP requests.
app.UseHttpMetrics();

app.UseEndpoints(endpoints =>
{
    // Enable the /metrics page to export Prometheus metrics.
    // Open http://localhost:5099/metrics to see the metrics.

    endpoints.MapMetrics();
});
```

Run your app and navigate to `/metrics` full url should be similar to `https://localhost:7088/metrics`, you should see (truncated):

```
# HELP http_requests_in_progress The number of requests currently in progress in the ASP.NET Core pipeline. One series without controller/action label values counts all in-progress requests, with separate series existing for each controller-action pair.
# TYPE http_requests_in_progress gauge
http_requests_in_progress{method="GET",controller="",action="",endpoint=""} 0
# HELP process_start_time_seconds Start time of the process since unix epoch in seconds.
# TYPE process_start_time_seconds gauge
process_start_time_seconds 1674824115.91
# HELP prometheus_net_metric_instances Number of metric instances currently registered across all metric families.
# TYPE prometheus_net_metric_instances gauge
prometheus_net_metric_instances{metric_type="gauge"} 21
```

## Grafana


## Zipkin

