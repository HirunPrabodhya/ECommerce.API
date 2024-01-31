using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public float UnitPrice { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }

        public Category? Category { get; set; }
        //product details
        public ICollection<OrderItem>? OrderItem { get; set; }
    }
}
