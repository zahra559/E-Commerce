using E_CommerceApp.Controllers;
using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using E_CommerceApp.Repositories;
using E_CommerceApp.Test.Helper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Test.ControllerTest
{
    public class OrderControllerTest
    {
        private DbContextHelper _dbHelper;

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IEmailSender _emailSender;
        private UnitOfWork _unitOfWork;

        private OrderController _orderController;

        public OrderControllerTest()
        {
            #region Dependencies
            _emailSender = A.Fake<IEmailSender>();
            _orderRepository = A.Fake<IOrderRepository>();
            _orderItemRepository = A.Fake<IOrderItemRepository>();


            _dbHelper = new DbContextHelper();

            #endregion

            #region SUT
     
            #endregion
        }
        [Fact]
        public async  void OrderItemController_GetOrder_ReturnsSuccess()
        {
            #region Arrange
            int orderId = 1;
            var dbContext = await _dbHelper.GetDBContext();
            _orderController = new OrderController(_unitOfWork, _emailSender);
            #endregion

            #region Act
            var result = _orderController.GetOrder(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<IActionResult>>();
            #endregion
        }

        [Fact]
        public async void OrderItemController_CheckoutOrder_ReturnsSuccess()
        {
            #region Arrange
            int orderId = 16;
            string summary = "Summary";
            var dbContext = await _dbHelper.GetDBContext();
            _unitOfWork = new UnitOfWork(dbContext);
            _orderController = new OrderController(_unitOfWork, _emailSender);
            #endregion

            #region Act
            var result = _orderController.CheckoutOrder(orderId, new CheckoutOrderDto { Summary = summary});
            #endregion

            #region Assert
            result.Should().BeOfType<Task<IActionResult>>();
            result.Should().NotBeNull();
            #endregion
        }
    }
}
