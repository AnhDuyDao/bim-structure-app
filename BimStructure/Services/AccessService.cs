using BimStructure.Repository;

namespace BimStructure.Services;

public sealed class AccessService : IAccessService
{
    private readonly IAccessRepository _accessRepository;

    public AccessService(IAccessRepository accessRepository)
    {
        _accessRepository = accessRepository;
    }

    public void ValidateDatabase(string databasePath)
    {
        _accessRepository.ValidateDatabase(databasePath);
    }
}
