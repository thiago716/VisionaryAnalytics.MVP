using System;
using System.Text.Json.Serialization;
using Application.IServices;
using Application.Services;
using Core.Entity;
using Core.Interfaces;
using Infraestructure.Repository;
using Polly;
using Polly.Extensions.Http;

namespace WebApi.Extensions;

public static class WebApiConfigurationExtension
{
     public static void ConfigureLogger(this WebApplicationBuilder builder)
     {
          builder.Logging.ClearProviders();
          builder.Logging.AddConsole();        
     }
     
     public static void ConfigureControllers(this WebApplicationBuilder builder)
     {
          builder.Services
               .AddControllers()
               .ConfigureApiBehaviorOptions(options =>
               {
                    options.SuppressModelStateInvalidFilter = true;
               })
               .AddJsonOptions(opt =>
               {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
               });
     }

     public static void ConfigurePersistanceApiUrls(this WebApplicationBuilder builder)
     {
          builder.Services.Configure<PersistanceApiUrlsOptions>(
          builder.Configuration.GetSection("PersistanceApiUrls"));
     }

     public static void ConfigureHttpClient(this WebApplicationBuilder builder)
     {
          var logger = LoggerFactory.Create(logging => logging.AddConsole()).CreateLogger<Program>();
          var sharedCircuitBreakerPolicy = GetCircuitBreakerPolicy(logger);
          builder.Services.AddSingleton(sharedCircuitBreakerPolicy);

          builder.Services.AddHttpClient("PersistanceClientApi", client =>
          {
               client.BaseAddress = new Uri(builder.Configuration["PersistanceApiUrls:Http"]);
               client.DefaultRequestHeaders.Accept.Clear();
               client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
          })
          .AddPolicyHandler(GetCircuitBreakerPolicy(logger));
     }
     
    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ILogger logger)
     {
          return HttpPolicyExtensions
              .HandleTransientHttpError()
              .CircuitBreakerAsync(
                  handledEventsAllowedBeforeBreaking: 3,
                  durationOfBreak: TimeSpan.FromSeconds(30),
                  onBreak: (outcome, breakDelay) =>
                  {
                       logger.LogWarning($"Circuit broken for {breakDelay.TotalSeconds} seconds due to: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
                  },
                  onReset: () => logger.LogInformation("Circuit closed. Requests flow normally."),
                  onHalfOpen: () => logger.LogInformation("Circuit is half-open. Testing health.")
              );
     }

     public static void ConfigureServices(this WebApplicationBuilder builder)
     {
          var configuration = builder.Configuration;

          // Register application services
          builder.Services.AddScoped<IProcessedVideoAppService, ProcessedVideoAppService>();

          // Register repositories
          builder.Services.AddHttpClient<IVideoRepository, VideoRepository>(client =>
          {
               var baseUrl = configuration["VideoApi:BaseUrl"];
               if (string.IsNullOrEmpty(baseUrl))

                    throw new Exception("VideoApi:BaseUrl is not configured.");
               client.BaseAddress = new Uri(baseUrl);
          });
     }

     public static void ConfigureSwagger(this WebApplicationBuilder builder)
     {
          builder.Services.AddEndpointsApiExplorer();
          builder.Services.AddSwaggerGen();
     }
}
