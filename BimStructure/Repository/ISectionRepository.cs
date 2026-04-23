using BimStructure.Repository.Dtos;

namespace BimStructure.Repository;

public interface ISectionRepository
{
    IReadOnlyList<SectionDto> GetSections(string databasePath);
}