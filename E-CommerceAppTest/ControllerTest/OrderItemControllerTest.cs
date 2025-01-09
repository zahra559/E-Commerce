using E_CommerceApp.Controllers;
using E_CommerceApp.Dtos.Requests.OrderItem;
using E_CommerceApp.Dtos.Responses.OrderItem;
using E_CommerceApp.Models;
using E_CommerceApp.Repositories;
using E_CommerceApp.Test.Helper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;
namespace E_CommerceApp.Test.ControllerTest
{
    public class OrderItemControllerTest
    {
        private DbContextHelper _dbHelper;
        private readonly UserManager<AppUser> _userManager;

        public OrderItemControllerTest()
        {
            #region Dependencies
            _userManager = A.Fake<UserManager<AppUser>>();
            _dbHelper = new DbContextHelper();

            #endregion
        }

        private async Task<OrderItemController> InitializeController()
        {
            var dbContext = await _dbHelper.GetDBContext();
            dbContext.ChangeTracker.Clear();
            var _unitOfWork = new UnitOfWork(dbContext);

            return new OrderItemController(_unitOfWork, _userManager);
        }

        [Theory]
        [InlineData (1, 1, 1)]
        [InlineData (2, 2, null)]

        public async void OrderItemController_CreateOrderItem_ReturnsSuccess(int productId , int quantity , int? orderId)
        {
            #region Arrange
            var createDto = new CreateOrderItemDto
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
            };
            List<Claim> claims = new List<Claim>{
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "test"),
};
            var identity = new GenericIdentity("TEST");
            identity.AddClaims(claims);

            var contextUser = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext()
            {
                User = contextUser,
            };
            var _orderItemController = await InitializeController();

            _orderItemController.ControllerContext = new ControllerContext();
            _orderItemController.ControllerContext.HttpContext = httpContext;
            #endregion

            #region Act
            var result = _orderItemController.CreateOrderItem(createDto);
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            var model = (result.Result as OkObjectResult)!.Value;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(OrderItemDto));
            #endregion
        }
        [Theory]
        [InlineData(2, 2, 2, 2)]
        [InlineData(1, 1, 1, 2)]

        public async void OrderItemController_UpdateProductItem_ReturnsSuccess(int orderItemId, int orderId, int productId, int quantity)
        {
            #region Arrange
            var updateDto = new UpdateOrderItemDto
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
            };
            var _orderItemController = await InitializeController();
            
            _orderItemController.ControllerContext = new ControllerContext();
            #endregion

            #region Act
            var result = _orderItemController.UpdateProductItem(orderItemId,updateDto);
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            var model = (result.Result as OkObjectResult)!.Value;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(OrderItemDto));
            #endregion
        }

        [Theory]
        [InlineData(2)]

        public async void OrderItemController_DeleteOrderItem_ReturnsSuccess(int orderItemId)
        {
            #region Arrange
            var _orderItemController = await InitializeController();

            _orderItemController.ControllerContext = new ControllerContext();
            #endregion

            #region Act
            var result = _orderItemController.DeleteOrderItem(orderItemId);
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(NoContentResult));
            #endregion
        }

    }
}

