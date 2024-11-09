

using E_CommerceApp.Dtos.Requests.Product;
using E_CommerceApp.Dtos.Responses.Product;
using E_CommerceApp.Models;

namespace E_CommerceApp.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ProductToProductDto(this Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Stock = product.Stock,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }

        public static Product UpdateProductDtoToProduct(this UpdateProductDto updateProductDto, int prodectId)
        {
            return new Product
            {
                ProductId = prodectId,
                Name = updateProductDto.Name,
                Price = updateProductDto.Price,
                Description = updateProductDto.Description,
                Stock = updateProductDto.Stock,
                ImageUrl= updateProductDto.ImageUrl
            };
        }

        public static Product CreateProductDtoToProduct(this CreateProductDto createProductDto)
        {
            return new Product
            {
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                Description = createProductDto.Description,
                Stock = createProductDto.Stock,
            };
        }
    }
}
