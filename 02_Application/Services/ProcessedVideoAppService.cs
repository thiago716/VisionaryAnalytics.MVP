using System;
using Application.IServices;
using Core.Entity;
using Core.Interfaces;

namespace Application.Services;

public class ProcessedVideoAppService : IProcessedVideoAppService
{
     private readonly IVideoRepository _videoRepository;
     public ProcessedVideoAppService(IVideoRepository videoRepository)
     {
          _videoRepository = videoRepository;
     }
     public async Task<ProcessedVideo> BuscarVideoAsync(int id)
     {
          return await _videoRepository.ObterPorIdAsync(id);
     }

     public async Task PublicarParaAtualizacaoAsync(int id)
     {
          await _videoRepository.EnfileirarParaAtualizarAsync(await BuscarVideoAsync(id));
     }

     public async Task PublicarParaCriacaoAsync(int id)
     {
          await _videoRepository.EnfileirarParaAdicionarAsync(await BuscarVideoAsync(id));
     }
}
