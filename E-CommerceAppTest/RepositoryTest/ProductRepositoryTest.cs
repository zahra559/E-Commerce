using E_CommerceApp.Dtos.Requests.Product;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using E_CommerceApp.Repositories;
using E_CommerceApp.Test.Helper;
using FluentAssertions;

namespace E_CommerceApp.Test.RepositoryTest
{
    public class ProductRepositoryTest
    {
        private ProductRepository _productRepository;

        public ProductRepositoryTest()
        {
            var dbHelper = new DbContextHelper();
            var dbContext = dbHelper.GetDBContext();
            _productRepository = new ProductRepository(dbContext);

        }

        [Fact]
        public async void ProductRepository_InsertAsync_ReturnProduct()
        {
            //Arrange
            var product = new Product
            {
                Name = "Product12",
                Description = "Desc",
                Price = 5,
                Stock = "Clothe"
            };
            //Act
            var result = _productRepository.InsertAsync(product);

            //Assert
            result.Should().BeOfType<Task<Product>>();
        }

        [Fact]
        public async void ProductRepository_UpdateAsync_ReturnProduct()
        {
            //Arrange
            var product = new Product
            {
                ProductId = 1,
                Name = "Product1",
                Description = "Desc",
                Price = 20,
                Stock = "Clothe"
            };
            //Act
            var result = _productRepository.UpdateAsync(product);

            //Assert
            result.Should().BeOfType<Task<Product>>();
        }

        [Fact]
        public async void ProductRepository_DeleteAsync_ReturnProduct()
        {
            //Arrange
            var productId = 12;
            //Act
            var result = _productRepository.DeleteAsync(productId);

            //Assert
            result.Should().BeOfType<Task<Product>>();
        }

        [Fact]
        public async void ProductRepository_GetProducts_ReturnListProduct()
        {
            //Arrange
            var searchCriteria = new GetProductsDto
            {
                ProductName = "Product2",
                StockName = "Food"
            };
            //Act
            var result = _productRepository.GetProducts(searchCriteria);

            //Assert
            result.Should().BeOfType<Task<List<Product>>>();
        }

        public async void ProductRepository_GetAllAsync_ReturnIEnumerableProduct()
        {
            //Arrange

            //Act
            var result = _productRepository.GetAllAsync();

            //Assert
            result.Should().BeOfType<Task<IEnumerable<Product>>>();
        }


        public async void ProductRepository_GetByIdAsync_ReturnNullableProduct()
        {
            //Arrange
            var ordeItemId = 1;
            //Act
            var result = _productRepository.GetByIdAsync(ordeItemId);

            //Assert
            result.Should().BeOfType<Task<Product?>>();
        }

    }
}