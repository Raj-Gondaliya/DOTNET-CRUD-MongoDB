using DOTNET_CRUD_MongoDB.Collection;

namespace DOTNET_CRUD_MongoDB.Repository;

public interface IPeopleRepository
{
    Task<List<People>> GetAllAsync();
    Task<People> GetByIdAsync(string id);
    Task CreateNewPeopleAsync(People newPeople);
    Task UpdatePeopleAsync(People peopleToUpdate);
    Task DeletePeopleAsync(string id);
}
