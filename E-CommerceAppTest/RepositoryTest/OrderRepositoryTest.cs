using E_CommerceApp.Interfaces;
using E_CommerceApp.Models;
using E_CommerceApp.Repositories;
using E_CommerceApp.Test.Helper;
using FluentAssertions;

namespace E_CommerceApp.Test.RepositoryTest
{
    public class OrderRepositoryTest
    {
        private OrderRepository _orderRepository;

        public OrderRepositoryTest()
        {
            var dbHelper = new DbContextHelper();
            var dbContext = dbHelper.GetDBContext();
            _orderRepository = new OrderRepository(dbContext);

        }

        [Fact]
        public async void OrderRepository_InsertAsync_ReturnOrder()
        {
            //Arrange
            var order = new Order
            {
                ApplicantId = "1",
                CheckedOut = true,
                OrderSummary ="Summary",
                TotalAmount = 100,
                OrderDate = DateTime.Now,
            };
            //Act
            var result = _orderRepository.InsertAsync(order);

            //Assert
            result.Should().BeOfType<Task<Order>>();
        }

        [Fact]
        public async void OrderRepository_UpdateAsync_ReturnOrder()
        {
            //Arrange
            var order = new Order
            {
                OrderId = 1,
                ApplicantId = "1",
                CheckedOut = true,
                OrderSummary = "Summary",
                TotalAmount = 100,
                OrderDate = DateTime.Now,
            };
            //Act
            var result = _orderRepository.UpdateAsync(order);

            //Assert
            result.Should().BeOfType<Task<Order>>();
        }

        [Fact]
        public async void OrderRepository_DeleteAsync_ReturnOrder()
        {
            //Arrange
            var orderId = 12;
            //Act
            var result = _orderRepository.DeleteAsync(orderId);

            //Assert
            result.Should().BeOfType<Task<Order>>();
        }

        [Fact]
        public async void OrderRepository_GetOrderByIdAsync_ReturnNullableOrder()
        {
            //Arrange
            var orderId = 1;
            //Act
            var result = _orderRepository.GetOrderByIdAsync(orderId);

            //Assert
            result.Should().BeOfType<Task<Order?>>();
        }

        public async void OrderRepository_GetAllAsync_ReturnIEnumerableOrder()
        {
            //Arrange

            //Act
            var result = _orderRepository.GetAllAsync();

            //Assert
            result.Should().BeOfType<Task<IEnumerable<Order>>>();
        }


        public async void OrderRepository_GetByIdAsync_ReturnNullableOrder()
        {
            //Arrange
            var orderId = 1;
            //Act
            var result = _orderRepository.GetByIdAsync(orderId);

            //Assert
            result.Should().BeOfType<Task<Order?>>();
        }

    }
}