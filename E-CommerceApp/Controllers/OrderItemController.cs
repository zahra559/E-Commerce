using E_CommerceApp.Constants;
using E_CommerceApp.Dtos.Requests.Order;
using E_CommerceApp.Dtos.Requests.OrderItem;
using E_CommerceApp.Extensions;
using E_CommerceApp.Interfaces;
using E_CommerceApp.Mappers;
using E_CommerceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApp.Controllers
{
    [Route("api/orderItem")]
    [ApiController]

    public class OrderItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;


        public OrderItemController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = RoleTypes.USER_ROLE)]
        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] CreateOrderItemDto dto)
        {
            Order? order = null;
            try
            {
                _unitOfWork.CreateTransaction();

                var username = User.GetUsername();
                var appUser = await _userManager.FindByNameAsync(username);
                if (dto.OrderId == null) {

                    List<Order> orders = await _unitOfWork.Orders.GetOrders(new GetOrdersDto { CheckedOut = false , ApplicantId = appUser.Id });    

                    if(orders.Count > 0)
                        order = orders[0];
                    
                    else{
                    
                        order = await _unitOfWork.Orders.InsertAsync(new Order { ApplicantId = appUser.Id });
                        await _unitOfWork.Save();

                        orders = await _unitOfWork.Orders.GetOrders(new GetOrdersDto { CheckedOut = false, ApplicantId = appUser.Id });
                        order = orders[0];
                    }
                    
                }
                else
                { 
                     order = await _unitOfWork.Orders.GetByIdAsync(dto.OrderId.Value);
                    if (order == null)
                        return BadRequest("Order does not exist");
                }

                var product = await _unitOfWork.Products.GetByIdAsync(dto.ProductId);
                if (product == null)
                    return BadRequest("Product does not exist");


                OrderItem? orderItem = await _unitOfWork.OrderItems.InsertAsync(dto.CreateOrderItemDtoToOrderItem(order.OrderId, product.Price * dto.Quantity));

                await _unitOfWork.Save();
                _unitOfWork.Commit();


                return Ok(orderItem.OrderItemToOrderItemDto());
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return StatusCode(500, ex);
            }

        }

        [Authorize(Roles = RoleTypes.USER_ROLE)]
        [HttpPut("{orderItemId:int}")]
        public async Task<IActionResult> UpdateProductItem(int orderItemId, [FromBody] UpdateOrderItemDto dto)
        {
            try
            {
                _unitOfWork.CreateTransaction();

                Product? product = await _unitOfWork.Products.GetByIdAsync(dto.ProductId);

                OrderItem? orderItem = await _unitOfWork.OrderItems.UpdateAsync(dto.UpdateOrderIemDtoToOrderItem(orderItemId, product.Price * dto.Quantity));
                if (orderItem == null) return NotFound("Product not found in the shopping cart");

                await _unitOfWork.Save();
                _unitOfWork.Commit();
                return Ok(orderItem.OrderItemToOrderItemDto());
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return StatusCode(500, ex);
            }

        }

        [Authorize(Roles = RoleTypes.USER_ROLE)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            try
            {
                _unitOfWork.CreateTransaction();

                var product = await _unitOfWork.OrderItems.DeleteAsync(id);
                if (product == null) return NotFound();

                await _unitOfWork.Save();
                _unitOfWork.Commit();

                return NoContent();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return StatusCode(500, ex);
            }
        }
    }
}
