using E_CommerceApp.Controllers;
using E_CommerceApp.Dtos.Requests.OrderItem;
using E_CommerceApp.Dtos.Requests.Product;
using E_CommerceApp.Dtos.Responses.Order;
using E_CommerceApp.Dtos.Responses.OrderItem;
using E_CommerceApp.Dtos.Responses.Product;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using E_CommerceApp.Repositories;
using E_CommerceApp.Service;
using E_CommerceApp.Test.Helper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace E_CommerceApp.Test.ControllerTest
{
    public class ProductControllerTest
    {
        private DbContextHelper _dbHelper;
        private readonly EntityFactory _entityFactory;
        private readonly IMemoryCache _memoryCache;
        public ProductControllerTest()
        {
            #region Dependencies
            _entityFactory = A.Fake<EntityFactory>();
            _memoryCache = A.Fake<IMemoryCache>();
            _dbHelper = new DbContextHelper();

            #endregion
        }

        private async Task<ProductController> InitializeController()
        {
            var dbContext = await _dbHelper.GetDBContext();
            dbContext.ChangeTracker.Clear();
            var _unitOfWork = new UnitOfWork(dbContext);
            return new ProductController(_unitOfWork, _entityFactory, _memoryCache);
        }

        [Theory]
        [InlineData(1,10,null,null)]
        [InlineData(1, 10, "Product1", null)]
        [InlineData(1, 10, null, "Food")]
        [InlineData(0, 0, null, "clothing")]


        public async void ProductController_GetProducts_ReturnsSuccess(int pageNumber, int pageSize,string? productName,string? stockName)
        {
            #region Arrange
            var dto = new GetProductsDto {PageNumber = pageNumber , PageSize = pageSize, ProductName = productName , StockName= stockName };
            var ProductController = await InitializeController();
            #endregion

            #region Act
            var result = ProductController.GetProducts(dto);
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            var model = (result.Result as OkObjectResult)!.Value;
            model?.Should().NotBeNull();
            model?.Should().BeOfType(typeof(List<ProductDto>));
            #endregion
        }

        [Fact]
        public async void ProductController_GetCachedProducts_ReturnsSuccess()
        {
            #region Arrange
            var ProductController = await InitializeController();
            #endregion

            #region Act
            var result = ProductController.GetCachedProducts();
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            var model = (result.Result as OkObjectResult)!.Value;
            model?.Should().NotBeNull();
            model?.Should().BeOfType(typeof(List<ProductDto>));
            #endregion
        }

        [Theory]
        [MemberData(nameof(Products))]
        public async void ProductController_CreateProduct_ReturnsSuccess(CreateProductDto dto)
        {
            #region Arrange
            var _productController = await InitializeController();
            _productController.ControllerContext = new ControllerContext();
            #endregion

            #region Act
            var result = _productController.CreateProduct(dto);
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            var model = (result.Result as OkObjectResult)!.Value;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(ProductDto));
            #endregion
        }

        [Theory]
        [MemberData(nameof(UpdateProduct))]
        public async void ProductController_UpdateProduct_ReturnsSuccess(UpdateProductDto dto )
        {
            #region Arrange
            var _productController = await InitializeController();
            _productController.ControllerContext = new ControllerContext();
            #endregion

            #region Act
            var result = _productController.UpdateProduct(dto.Id,dto);
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            var model = (result.Result as OkObjectResult)!.Value;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(ProductDto));
            #endregion
        }

        [Fact]
        public async void ProductController_DeleteProduct_ReturnsSuccess()
        {
            #region Arrange
            var productId = 3;
            var _productController = await InitializeController();
            _productController.ControllerContext = new ControllerContext();
            #endregion

            #region Act
            var result = _productController.DeleteProduct(productId);
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }


        #region private Methods
        private static IFormFile GetTestFormFile()
        {
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Files");
            var file = Path.Combine(Directory.GetCurrentDirectory(), pathBuilt, "test.png");
            using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            return new FormFile(stream, 0, stream.Length, "streamFile", file.Split(@"\").Last());
        }
        public static IEnumerable<object[]> Products
        {
            get
            {
                yield return new object[]
                {
                    new CreateProductDto
                    { Name = "Product11" ,Price= 100, Description = "desc" , Stock = "clothes",Image = GetTestFormFile()}
                };
                yield return new object[]
                {
                    new CreateProductDto
                    { Name = "Product12", Price = 200, Description = "desc", Stock = "clothes", Image = GetTestFormFile()}
                };
            }
        }

                public static IEnumerable<object[]> UpdateProduct
        {
            get
            {

                yield return new object[]
                {
                    new UpdateProductDto{Id = 1,Name="Product1", Stock ="food", Description="desc", ImageUrl ="Resources\\Files\\test.png", Price = 100 ,Image = GetTestFormFile()}
                };

                yield return new object[]
                {

                   new UpdateProductDto{Id = 2,Name="Product2", Stock ="food", Description="desc",ImageUrl ="Resources\\Files\\test.png", Price = 100 }

                };
            }
        }

        #endregion
    }
}
