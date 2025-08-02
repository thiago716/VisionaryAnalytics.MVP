using System.Net.Http.Json;
using Core.Entity;
using Core.Interfaces;
using MassTransit;
using Shared.DTOs;

namespace Infraestructure.Repository;

public class VideoRepository: IVideoRepository
{
     private readonly HttpClient _httpClient;
     private readonly IPublishEndpoint _publishEndpoint;
     public VideoRepository(HttpClient httpClient, IPublishEndpoint publishEndpoint)
     {
          _httpClient = httpClient;
          _publishEndpoint = publishEndpoint;
     }

     public async Task<ProcessedVideo> ObterPorIdAsync(int id)
     {
          var response = await _httpClient.GetAsync($"api/videos/{id}");
          response.EnsureSuccessStatusCode();

          var video = await response.Content.ReadFromJsonAsync<ProcessedVideo>();
          return video ?? throw new Exception("Video not found");
     }

     public async Task EnfileirarParaAtualizarAsync(ProcessedVideo video)
     {
          var massage = new AtualizarVideoMessage
          {
               Id = video.Id,
               Nome = video.FileName,
               Status = video.Status.ToString(),
               CriadoEm = video.CreatedAt,
               QrCodes = video.QrCodeDetections.Select(d => new QrCodeDetectionDto
               {
                    Content = d.Content,
                    TimestampSeconds = d.TimestampSeconds
               }).ToList()
          };

          await _publishEndpoint.Publish(massage);
    }

     public async Task EnfileirarParaAdicionarAsync(ProcessedVideo video)
     {
          var massage = new AdicionarVideoMessage
          {
               Id = video.Id,
               Nome = video.FileName,
               Status = video.Status.ToString(),
               CriadoEm = video.CreatedAt
          };

          await _publishEndpoint.Publish(massage);
    }
}
