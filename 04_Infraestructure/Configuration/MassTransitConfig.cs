using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Configuration;

public static class MassTransitConfig
{
     public static IServiceCollection AddMassTransitPublisher(this IServiceCollection services, IConfiguration configuration)
     {
          var host = configuration["RabbitMq:Host"] ?? "localhost";
          var user = configuration["RabbitMq:User"] ?? "guest";
          var password = configuration["RabbitMq:Password"] ?? "guest";

          services.AddMassTransit(x =>
          {
               // Caso queira adicionar futuramente um consumer neste projeto, use:
               // x.AddConsumer<SeuConsumer>();
            
               x.UsingRabbitMq((context, cfg) =>
               {
                    cfg.Host(host, h =>
                    {
                         h.Username(user);
                         h.Password(password);
                    });

                    // Configura retries com backoff exponencial
                    cfg.UseMessageRetry(r =>
                    {
                         r.Exponential(
                              retryLimit: 5,
                              minInterval: TimeSpan.FromSeconds(1),
                              maxInterval: TimeSpan.FromSeconds(30),
                              intervalDelta: TimeSpan.FromSeconds(5)
                         );
                    });
                    // Dead-letter automatic (fila de erro)
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("video-processor", false));
               });
          });

          return services;
     }
}
