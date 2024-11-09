using E_CommerceApp.Data;
using E_CommerceApp.Dtos.Requests.Product;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDBContext context) : base(context) { }

        public async Task<List<Product>> GetProducts(GetProductsDto criteria)
        {
            var stocks =  _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(criteria.ProductName))
                stocks = stocks.Where(x => x.Name.Contains(criteria.ProductName));
            
            if(!string.IsNullOrWhiteSpace(criteria.StockName)) 
                stocks = stocks.Where(x => x.Stock.Contains(criteria.StockName));
            
            var skipNumber = (criteria.PageNumber - 1) * criteria.PageSize;

            return await stocks
                .Skip(skipNumber)
                .Take(criteria.PageSize)
                .ToListAsync();
        }

    }
}
