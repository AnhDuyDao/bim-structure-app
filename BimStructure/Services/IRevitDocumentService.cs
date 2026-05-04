using BimStructure.Models;

namespace BimStructure.Services;

public interface IRevitDocumentService
{
    void Save(Document document, Project project);
}