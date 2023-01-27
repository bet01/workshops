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

## Grafana


## Zipkin

