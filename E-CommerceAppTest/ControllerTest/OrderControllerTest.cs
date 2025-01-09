using E_CommerceApp.Controllers;
using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Dtos.Responses.Order;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Repositories;
using E_CommerceApp.Test.Helper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace E_CommerceApp.Test.ControllerTest
{
    public class OrderControllerTest
    {
        private DbContextHelper _dbHelper;
        private readonly IEmailSender _emailSender;

        public OrderControllerTest()
        {
            #region Dependencies
            _emailSender = A.Fake<IEmailSender>();
            _dbHelper = new DbContextHelper();

            #endregion
        }

        private async Task<OrderController> InitializeController()
        {
            var dbContext = await _dbHelper.GetDBContext();
            var _unitOfWork = new UnitOfWork(dbContext);
            return new OrderController(_unitOfWork, _emailSender);
        }

        [Fact]
        public async void OrderItemController_GetOrder_ReturnsSuccess()
        {
            #region Arrange
            int orderId = 1;
            var _orderController = await InitializeController();
            #endregion

            #region Act
            var result = _orderController.GetOrder(orderId);
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            var model = (result.Result as OkObjectResult)!.Value;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(OrderDto));
            #endregion
        }

        [Fact]
        public async void OrderItemController_CheckoutOrder_ReturnsSuccess()
        {
            #region Arrange
            int orderId = 3;
            string summary = "Summary";
            var _orderController = await InitializeController();
            List<Claim> claims = new List<Claim>{
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "test.gmail.com"),
};
            var identity = new GenericIdentity("TEST");
            identity.AddClaims(claims);

            var contextUser = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext()
            {
                User = contextUser,
            };

            _orderController.ControllerContext = new ControllerContext();
            _orderController.ControllerContext.HttpContext = httpContext;
            #endregion

            #region Act
            var result = _orderController.CheckoutOrder(orderId, new CheckoutOrderDto { Summary = summary});
            #endregion

            #region Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            var model = (result.Result as OkObjectResult)!.Value ;
            model.Should().NotBeNull();
            model.Should().BeOfType(typeof(OrderDto));
            #endregion
        }
    }
}
