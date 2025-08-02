using Core.Entity;

namespace Core.Interfaces;

public interface IVideoRepository
{
    /// <summary>
    /// Busca um vídeo da API externa via HTTP.
    /// </summary>
    Task<ProcessedVideo> ObterPorIdAsync(int id);
    /// <summary>
    /// Envia mensagem para fila para inclusão do vídeo.
    /// </summary>
    Task EnfileirarParaAdicionarAsync(ProcessedVideo video);
      /// <summary>
    /// Envia mensagem para fila para atualização do vídeo.
    /// </summary>
    Task EnfileirarParaAtualizarAsync(ProcessedVideo video);
}
