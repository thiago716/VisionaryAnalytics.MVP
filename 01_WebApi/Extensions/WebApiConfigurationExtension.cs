using System;
using System.Text.Json.Serialization;
using Application.IServices;
using Application.Services;
using Core.Interfaces;
using Infraestructure.Repository;

namespace WebApi.Extensions;

public static class WebApiConfigurationExtension
{
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

     public static void ConfigureServices(this WebApplicationBuilder builder)
     {
          // Register application services
          builder.Services.AddScoped<IProcessedVideoAppService, ProcessedVideoAppService>();
          
          // Register repositories
          builder.Services.AddScoped<IVideoRepository, VideoRepository>();
          
          // Add other necessary services here
     }
     public static void ConfigureSwagger(this WebApplicationBuilder builder)
     {
          builder.Services.AddEndpointsApiExplorer();
          builder.Services.AddSwaggerGen();
     }
}
