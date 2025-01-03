using E_CommerceApp.Controllers;
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
        private readonly UnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private IOrderRepository _orderRepository;
        private readonly OrderController _orderController;



        public OrderControllerTest()
        {
            #region Dependencies
            _emailSender = A.Fake<IEmailSender>();
            _orderRepository = A.Fake<IOrderRepository>();


            var dbHelper = new DbContextHelper();
            var dbContext = dbHelper.GetDBContext();
            #endregion

            #region SUT
            _unitOfWork = new UnitOfWork(dbContext);
            _orderController = new OrderController(_unitOfWork, _emailSender);
            #endregion
        }
        [Fact]
        public  void OrderItemController_GetOrder_ReturnsSuccess()
        {
            #region Arrange
            int orderId = 1;
            var order = A.Fake<Order>();
            A.CallTo(() => _orderRepository.GetOrderByIdAsync(orderId)).Returns(order);
            #endregion

            #region Act
            var result = _orderController.GetOrder(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<IActionResult>>();
            #endregion
        }
    }
}
