using System;
using Core.Entity;

namespace Application.IServices;

public interface IProcessedVideoAppService
{
     Task<ProcessedVideo> BuscarVideoAsync(int id);
     Task PublicarParaCriacaoAsync(int id);
     Task PublicarParaAtualizacaoAsync(int id);
}
