using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderStructure
    {
        public int UserId { get; set; }
        public float SubTotalPrice { get; set; }
        public List<CartData> CartData { get; set; }
    }
}
