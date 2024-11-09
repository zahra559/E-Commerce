using E_CommerceApp.Models;

namespace E_CommerceApp.Interfaces
{
    public interface IEntityService 
    {
        object? Create(object Entity, IFormFile? file);
        void Delete(object Entity);

    }
}
