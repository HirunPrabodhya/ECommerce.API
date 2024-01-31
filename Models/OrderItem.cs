using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quentity { get; set; }
        public float TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public Product? Product { get; set; }
        public Order? Order { get; set; }

    }
}
