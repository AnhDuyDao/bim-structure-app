using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface ISectionRepository
{
    Task<IReadOnlyList<SectionDto>> GetSectionsAsync(
        string databasePath,
        CancellationToken cancellationToken = default);
}