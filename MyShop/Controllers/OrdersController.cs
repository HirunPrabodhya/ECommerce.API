using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _order;
        private readonly IEmail _email; 

        public OrdersController(IOrder order, IEmail email)
        {
            _order = order;
            _email = email;
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody]OrderStructure orderStructure)
        {
            int orderId = 0;
            string emailMessage = "";
            string message = await _order.AddItemToOrder(orderStructure.UserId, orderStructure.SubTotalPrice);
            if (message != null)
            {
                orderId = await _order.AddOrderIdToOrderItem(orderStructure.UserId, orderStructure.CartData);
            }
            if (orderId != 0)
            {
                emailMessage = await _email.GetDBDataToSendEmail(orderId);
            }
            emailMessage = await _email.GetDBDataToSendEmail(orderId);
            return Ok(new
            {
               message = message,
               email = emailMessage
            });
        }
    }
}
