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
        public void OrderRepository_InsertAsync_ReturnOrder()
        {
            #region Arrange
            var order = new Order
            {
                ApplicantId = "1",
                CheckedOut = true,
                OrderSummary ="Summary",
                TotalAmount = 100,
                OrderDate = DateTime.Now,
            };
            #endregion

            #region Act
            var result = _orderRepository.InsertAsync(order);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order>>();
            #endregion

        }

        [Fact]
        public void OrderRepository_UpdateAsync_ReturnOrder()
        {
            #region Arrange
            var order = new Order
            {
                OrderId = 1,
                ApplicantId = "1",
                CheckedOut = true,
                OrderSummary = "Summary",
                TotalAmount = 100,
                OrderDate = DateTime.Now,
            };
            #endregion

            #region Act
            var result = _orderRepository.UpdateAsync(order);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order>>();
            #endregion
        }

        [Fact]
        public void OrderRepository_DeleteAsync_ReturnOrder()
        {
            #region Arrange
            var orderId = 12;
            #endregion
            #region Act
            var result = _orderRepository.DeleteAsync(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order>>();
            #endregion
        }

        [Fact]
        public void OrderRepository_GetOrderByIdAsync_ReturnNullableOrder()
        {
            #region Arrange
            var orderId = 1;
            #endregion
            #region Act
            var result = _orderRepository.GetOrderByIdAsync(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order?>>();
            #endregion
        }
        [Fact]
        public void OrderRepository_GetAllAsync_ReturnIEnumerableOrder()
        {

            #region Act
            var result = _orderRepository.GetAllAsync();
            #endregion

            #region Assert
            result.Should().BeOfType<Task<IEnumerable<Order>>>();
            #endregion
        }

        [Fact]
        public void OrderRepository_GetByIdAsync_ReturnNullableOrder()
        {
            #region Arrange
            var orderId = 1;
            #endregion
            #region Act
            var result = _orderRepository.GetByIdAsync(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order?>>();
            #endregion
        }

    }
}