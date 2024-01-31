using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float SubTotalPrice { get; set; }
        public DateTime Date { get; set; }
        public User? User { get; set; }
        public ICollection<OrderItem>? OrderItem { get; set; }
    }
}
