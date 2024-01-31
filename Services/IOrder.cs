using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IOrder
    {
        public Task<string> AddItemToOrder(int userId,float subTotalPrice);
        public Task<int> AddOrderIdToOrderItem(int userId,List<CartData> cartData);
    }
}
