using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrder
    {
        private readonly MyData _myData;
        public OrderService(MyData myData)
        {
            _myData= myData;
        }
        public async Task<string> AddItemToOrder(int userId,float subTotalPrice)
        {
            Order order = new Order
            {
                UserId = userId,
                SubTotalPrice = subTotalPrice,
                Date= DateTime.Now,
            };
            await _myData.AddAsync(order);
            await _myData.SaveChangesAsync();   
            return "order is added";
        }

        public async Task<int> AddOrderIdToOrderItem(int id,List<CartData> cartData)
        {
            /*var orderId = await  (
                                     from or in _myData.orders
                                     where or.UserId == id 
                                     select or.Id
                                 ).SingleAsync();*/
            var orderId = await (from or in _myData.orders
                                 where or.UserId == id
                                 && or.Date.Date== DateTime.Now.Date
                                 && or.Date.TimeOfDay.Hours == DateTime.Now.TimeOfDay.Hours
                                 select or.Id
                                  ).FirstOrDefaultAsync();



            foreach (var item in cartData)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    OrderId = orderId,
                    Quentity = item.Quentity,
                    Date = DateTime.Now,
                    TotalPrice = item.TotalPrice,
                };

                await _myData.orderItems.AddAsync(orderItem);

            }
            await _myData.SaveChangesAsync();
            return orderId;
        }
    }
}
