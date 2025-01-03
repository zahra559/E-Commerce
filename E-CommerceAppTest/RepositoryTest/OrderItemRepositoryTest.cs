using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Models;
using E_CommerceApp.Repositories;
using E_CommerceApp.Test.Helper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApp.Test.RepositoryTest
{
    public class OrderItemRepositoryTest
    {
        private OrderItemRepository _orderItemRepository;

        public OrderItemRepositoryTest()
        {
            var dbHelper = new DbContextHelper();
            var dbContext = dbHelper.GetDBContext();
            _orderItemRepository = new OrderItemRepository(dbContext);

        }

        [Fact]
        public void OrderItemRepository_InsertAsync_ReturnOrderItem()
        {
            #region Arrange
            var orderItem = new OrderItem
            {
                OrderId = 1,
                ProductId = 3,
                Quantity = 2,
                Price = 6
            };
            #endregion
            #region Act
            var result = _orderItemRepository.InsertAsync(orderItem);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<OrderItem>>();
            #endregion

        }

        [Fact]
        public void OrderItemRepository_UpdateAsync_ReturnOrderItem()
        {
            #region Arrange
            var orderItem = new OrderItem
            {
                OrderId = 1,
                ProductId= 3,
            };
            #endregion

            #region Act
            var result = _orderItemRepository.UpdateAsync(orderItem);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<OrderItem>>();
            #endregion
        }

        [Fact]
        public void OrderItemRepository_DeleteAsync_ReturnOrderItem()
        {
            #region Arrange
            var orderId = 12;
            #endregion

            #region Act
            var result = _orderItemRepository.DeleteAsync(orderId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<OrderItem>>();
            #endregion

        }

        [Fact]
        public void OrderItemRepository_GetOrderItems_ReturnListOrderItem()
        {
            #region Arrange
            var searchCriteria = new GetOrderItemsDto
            {
                ProductId = 3,
                OrderId = 1
            };
            #endregion

            #region Act
            var result = _orderItemRepository.GetOrderItems(searchCriteria);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<List<OrderItem>>>();
            #endregion

        }

        [Fact]
        public void OrderItemRepository_GetAllAsync_ReturnIEnumerableOrderItem()
        {
           
            #region Act
            var result = _orderItemRepository.GetAllAsync();
            #endregion

            #region Assert
            result.Should().BeOfType<Task<IEnumerable<OrderItem>>>();
            #endregion

        }

        [Fact]
        public void OrderItemRepository_GetByIdAsync_ReturnNullableOrderItem()
        {
            #region Arrange
            var ordeItemId = 1;
            #endregion

            #region Act
            var result = _orderItemRepository.GetByIdAsync(ordeItemId);
            #endregion

            #region Assert
            result.Should().BeOfType<Task<OrderItem?>>();
            #endregion

        }

    }
}

