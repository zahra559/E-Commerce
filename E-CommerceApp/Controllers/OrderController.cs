using E_CommerceApp.Constants;
using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Dtos.Requests.OrderItem;
using E_CommerceApp.Extensions;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Mappers;
using E_CommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public OrderController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        [Authorize(Roles = RoleTypes.USER_ROLE)]
        [HttpPut("checkout/{orderId:int}")]
        public async Task<IActionResult> CheckoutOrder(int orderId, [FromBody] CheckoutOrderDto dto)
        {
            try
            {
                _unitOfWork.CreateTransaction();

                Order? order = await _unitOfWork.Orders.GetByIdAsync(orderId);
                List<OrderItem> orderItems = await _unitOfWork.OrderItems.GetOrderItems(new GetOrderItemsDto { OrderId = orderId});
                decimal orderTotalAmount = 0;
                foreach (OrderItem item in orderItems)
                {
                    orderTotalAmount +=  item.Price;
                }
                order.OrderSummary = dto.Summary;
                order.OrderDate = DateTime.Now;
                order.CheckedOut = true;
                order.TotalAmount = orderTotalAmount;

                order = await _unitOfWork.Orders.UpdateAsync(order);
                if (order == null) return NotFound("Order not found ");

                await _emailSender.SendEmailAsync(User.GetUserEmail(), "Order Confirmation", "Order is on the way");

                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok(order.OrderToOrderDto());
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return StatusCode(500, ex);
            }

        }

        [Authorize(Roles = RoleTypes.USER_ROLE)]
        [HttpGet("{orderId:int}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {

            Order? order = await _unitOfWork.Orders.GetOrderByIdAsync(orderId);
            return Ok(order?.OrderToOrderDto());

        }
    }
}
