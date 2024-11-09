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
        public async void OrderItemRepository_InsertAsync_ReturnOrderItem()
        {
            //Arrange
            var orderItem = new OrderItem
            {
                OrderId = 1,
                ProductId = 3,
                Quantity = 2,
                Price = 6
            };
            //Act
            var result = _orderItemRepository.InsertAsync(orderItem);

            //Assert
            result.Should().BeOfType<Task<OrderItem>>();
        }

        [Fact]
        public async void OrderItemRepository_UpdateAsync_ReturnOrderItem()
        {
            //Arrange
            var orderItem = new OrderItem
            {
                OrderId = 1,
                ProductId= 3,
            };
            //Act
            var result = _orderItemRepository.UpdateAsync(orderItem);

            //Assert
            result.Should().BeOfType<Task<OrderItem>>();
        }

        [Fact]
        public async void OrderItemRepository_DeleteAsync_ReturnOrderItem()
        {
            //Arrange
            var orderId = 12;
            //Act
            var result = _orderItemRepository.DeleteAsync(orderId);

            //Assert
            result.Should().BeOfType<Task<OrderItem>>();
        }

        [Fact]
        public async void OrderItemRepository_GetOrderItems_ReturnListOrderItem()
        {
            //Arrange
            var searchCriteria = new GetOrderItemsDto
            {
                ProductId = 3,
                OrderId = 1
            };
            //Act
            var result = _orderItemRepository.GetOrderItems(searchCriteria);

            //Assert
            result.Should().BeOfType<Task<List<OrderItem>>>();
        }

        public async void OrderItemRepository_GetAllAsync_ReturnIEnumerableOrderItem()
        {
            //Arrange
           
            //Act
            var result = _orderItemRepository.GetAllAsync();

            //Assert
            result.Should().BeOfType<Task<IEnumerable<OrderItem>>>();
        }


        public async void OrderItemRepository_GetByIdAsync_ReturnNullableOrderItem()
        {
            //Arrange
            var ordeItemId = 1;
            //Act
            var result = _orderItemRepository.GetByIdAsync(ordeItemId);

            //Assert
            result.Should().BeOfType<Task<OrderItem?>>();
        }

    }
}

