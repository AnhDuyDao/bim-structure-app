using BimStructure.Dtos;

namespace BimStructure.Repositories;

public interface ISectionRepository
{
    Task<IReadOnlyList<SectionDto>> GetSectionsAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}