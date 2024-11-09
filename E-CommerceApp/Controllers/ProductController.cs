using E_CommerceApp.Constants;
using E_CommerceApp.Dtos.Requests.Product;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Mappers;
using E_CommerceApp.Models;
using E_CommerceApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace E_CommerceApp.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityFactory _entityFactory;
        private readonly IMemoryCache _memoryCache;
        public string cacheKey = "Products";


        public ProductController(IUnitOfWork unitOfWork, EntityFactory entityFactory, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _entityFactory = entityFactory;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsDto criteria)
        {
            var products = await _unitOfWork.Products.GetProducts(criteria);
            var productDtos = products.Select(s => s.ProductToProductDto());
            return Ok(productDtos);
        }

        [Authorize(Roles = RoleTypes.ADMIN_ROLE)]
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            IEnumerable<Product> products;

            if (!_memoryCache.TryGetValue(cacheKey, out products))
            {
                products = await _unitOfWork.Products.GetAllAsync();

                _memoryCache.Set(cacheKey, products,
                    new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(5000)));
            }
            var productDtos = products.Select(s => s.ProductToProductDto());
            return Ok(productDtos);
        }


        [Authorize(Roles = RoleTypes.ADMIN_ROLE)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto dto)
        {
            try
            {
                _unitOfWork.CreateTransaction();

                Product? product = await _unitOfWork.Products.UpdateAsync(dto.UpdateProductDtoToProduct(id));
                if (product == null) return NotFound("Product not found");

                product = _entityFactory.GetEntityService(typeof(Product)).Create(product, dto.Image) as Product;
                if (product == null) return StatusCode(500, "Please upload image");

                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok(product.ProductToProductDto());
            }
            catch(Exception ex)
            {
                _unitOfWork.Rollback();
                return StatusCode(500, ex);
            }

        }

        [Authorize(Roles = RoleTypes.ADMIN_ROLE)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductDto dto)
        {
            try
            {
                _unitOfWork.CreateTransaction();
                Product? product = await _unitOfWork.Products.InsertAsync(dto.CreateProductDtoToProduct());

                product = _entityFactory.GetEntityService(typeof(Product)).Create(product, dto.Image) as Product;
                if (product == null) return StatusCode(500, "Please upload image");

                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok(product.ProductToProductDto());
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return StatusCode(500, ex);
            }

        }

        [Authorize(Roles = RoleTypes.ADMIN_ROLE)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                _unitOfWork.CreateTransaction();

                var product = await _unitOfWork.Products.DeleteAsync(id);
                if (product == null) return NotFound();

                _entityFactory.GetEntityService(typeof(Product)).Delete(product);

                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return NoContent();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return StatusCode(500, ex);
            }
        }

    }
}
