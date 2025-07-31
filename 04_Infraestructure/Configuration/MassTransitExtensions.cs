using System;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Configuration;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
          var rabbitHost = configuration["RabbitMq:Host"] ?? "localhost";
          var rabbitUser = configuration["RabbitMq:User"] ?? "guest";
          var rabbitPass = configuration["RabbitMq:Password"] ?? "guest";

          services.AddMassTransit(x =>
          {
               x.UsingRabbitMq((context, cfg) =>
               {
                    cfg.Host(rabbitHost, "/", h =>
                    {
                         h.Username(rabbitUser);
                         h.Password(rabbitPass);
                    });

                    cfg.ConfigureEndpoints(context);
               });
          });

          return services;
     }

}
