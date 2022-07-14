using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Formatting.Json;
using Serilog.Sinks.Seq;

namespace MicroserviceNET.Logging;
public static class HostBuilderExtensions
{
  public static IHostBuilder UseLogging(this IHostBuilder builder, string url) =>
    builder.UseSerilog((context, logger) => 
    {
      logger
        .Enrich.FromLogContext()
        .Enrich.WithSpan();
      if(context.HostingEnvironment.IsDevelopment()) {
        logger.WriteTo.Console(
          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {TraceId} {Level:u3} {Message}{NewLine}{Exception}"
        );
        logger.WriteTo.Seq(url);
      } else {
        logger.WriteTo.Console(new JsonFormatter());
        logger.WriteTo.Seq(url);
      }
    });
}
