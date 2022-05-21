[![.NET](https://github.com/aimenux/WebApiHttpLoggingDemo/actions/workflows/ci.yml/badge.svg)](https://github.com/aimenux/WebApiHttpLoggingDemo/actions/workflows/ci.yml)

# WebApiHttpLoggingDemo
```
Using http logging middleware in order to log requests/responses
```

In this demo, i m using [http logging middleware](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-logging/?view=aspnetcore-6.0#enabling-http-logging) in order to enable logging of requests/responses.
>
In addition to http logging middleware, i m enhancing application insights telemetries through the using of :
>

:heavy_minus_sign: [Telemetry processors](https://docs.microsoft.com/en-us/azure/azure-monitor/app/api-filtering-sampling#create-a-telemetry-processor) : filter swagger/health logs and not send them to application insights
>

:heavy_minus_sign: [Telemetry initializers](https://docs.microsoft.com/en-us/azure/azure-monitor/app/api-filtering-sampling#addmodify-properties-itelemetryinitializer) : add apikey header to related request application insights telemetries
>

**`Tools`** : vs22, net 6.0, serilog, telemetry processor, telemetry initializer, application-insights, healthchecks