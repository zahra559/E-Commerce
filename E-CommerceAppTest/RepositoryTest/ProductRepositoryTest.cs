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
        public void ProductRepository_InsertAsync_ReturnProduct()
        {
            #region Arrange
            var product = new Product
            {
                Name = "Product12",
                Description = "Desc",
                Price = 5,
                Stock = "Clothe"
            };
            #endregion

            #region Act
            var result = _productRepository.InsertAsync(product);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Product>>();
            #endregion
        }

        [Fact]
        public void ProductRepository_UpdateAsync_ReturnProduct()
        {
            #region Arrange
            var product = new Product
            {
                ProductId = 1,
                Name = "Product1",
                Description = "Desc",
                Price = 20,
                Stock = "Clothe"
            };
            #endregion

            #region Act
            var result = _productRepository.UpdateAsync(product);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Product>>();
            #endregion
        }

        [Fact]
        public void ProductRepository_DeleteAsync_ReturnProduct()
        {
            #region Arrange
            var productId = 12;
            #endregion

            #region Act
            var result = _productRepository.DeleteAsync(productId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Product>>();
            #endregion
        }

        [Fact]
        public void ProductRepository_GetProducts_ReturnListProduct()
        {
            #region Arrange
            var searchCriteria = new GetProductsDto
            {
                ProductName = "Product2",
                StockName = "Food"
            };
            #endregion

            #region Act
            var result = _productRepository.GetProducts(searchCriteria);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<List<Product>>>();
            #endregion
        }

        [Fact]
        public void ProductRepository_GetAllAsync_ReturnIEnumerableProduct()
        {

            #region Act
            var result = _productRepository.GetAllAsync();
            #endregion

            #region Assert
            result.Should().BeOfType<Task<IEnumerable<Product>>>();
            #endregion
        }

        [Fact]
        public void ProductRepository_GetByIdAsync_ReturnNullableProduct()
        {
            #region Arrange
            var ordeItemId = 1;
            #endregion
            #region Act
            var result = _productRepository.GetByIdAsync(ordeItemId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Product?>>();
            #endregion
        }

    }
}