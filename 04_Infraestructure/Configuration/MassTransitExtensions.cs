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

          if (string.IsNullOrWhiteSpace(rabbitHost))
            throw new ArgumentException("RabbitMq:Host não está configurado.");
          if (string.IsNullOrWhiteSpace(rabbitUser))
            throw new ArgumentException("RabbitMq:User não está configurado.");
          if (string.IsNullOrWhiteSpace(rabbitPass))
            throw new ArgumentException("RabbitMq:Password não está configurado.");
            
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
