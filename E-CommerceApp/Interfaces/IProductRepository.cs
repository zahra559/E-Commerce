using E_CommerceApp.Dtos.Requests.Product;
using E_CommerceApp.Models;

namespace E_CommerceApp.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProducts(GetProductsDto criteria);
    }
}
