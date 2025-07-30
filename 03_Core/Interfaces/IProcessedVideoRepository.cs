using Core.Entity;

namespace Core.Interfaces;

public interface IProcessedVideoRepository
{
     Task AddAsync(ProcessedVideo video);
    Task UpdateAsync(ProcessedVideo video);
    Task<ProcessedVideo> GetByIdAsync(int id);
    Task<IEnumerable<ProcessedVideo>> GetAllAsync();
}
