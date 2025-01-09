using E_CommerceApp.Models;
using E_CommerceApp.Repositories;
using E_CommerceApp.Test.Helper;
using FluentAssertions;

namespace E_CommerceApp.Test.RepositoryTest
{
    public class OrderRepositoryTest
    {
        private DbContextHelper _dbHelper;

        public OrderRepositoryTest()
        {
            _dbHelper = new DbContextHelper();

        }

        [Fact]
        public async void OrderRepository_InsertAsync_ReturnOrder()
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

            var dbContext = await _dbHelper.GetDBContext();
            var orderRepository = new OrderRepository(dbContext);
            #endregion

            #region Act
            var result = orderRepository.InsertAsync(order);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order>>();
            #endregion

        }

        [Fact]
        public async void OrderRepository_UpdateAsync_ReturnOrder()
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
            var dbContext = await _dbHelper.GetDBContext();
            var orderRepository = new OrderRepository(dbContext);
            #endregion

            #region Act
            var result = orderRepository.UpdateAsync(order);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order>>();
            #endregion
        }

        [Fact]
        public async void OrderRepository_DeleteAsync_ReturnOrder()
        {
            #region Arrange
            var orderId = 12;
            var dbContext = await _dbHelper.GetDBContext();
            var orderRepository = new OrderRepository(dbContext);
            #endregion
            #region Act
            var result = orderRepository.DeleteAsync(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order>>();
            #endregion
        }

        [Fact]
        public async void OrderRepository_GetOrderByIdAsync_ReturnNullableOrder()
        {
            #region Arrange
            var orderId = 1;
            var dbContext = await _dbHelper.GetDBContext();
            var orderRepository = new OrderRepository(dbContext);
            #endregion

            #region Act
            var result = orderRepository.GetOrderByIdAsync(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order?>>();
            #endregion
        }
        [Fact]
        public async void OrderRepository_GetAllAsync_ReturnIEnumerableOrder()
        {

            #region Arrange
            var dbContext = await _dbHelper.GetDBContext();
            var orderRepository = new OrderRepository(dbContext);
            #endregion

            #region Act
            var result = orderRepository.GetAllAsync();
            #endregion

            #region Assert
            result.Should().BeOfType<Task<IEnumerable<Order>>>();
            #endregion
        }

        [Fact]
        public async void OrderRepository_GetByIdAsync_ReturnNullableOrder()
        {
            #region Arrange
            var orderId = 1;
            var dbContext = await _dbHelper.GetDBContext();
            var orderRepository = new OrderRepository(dbContext);
            #endregion
            #region Act
            var result = orderRepository.GetByIdAsync(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<Order?>>();
            #endregion
        }

    }
}