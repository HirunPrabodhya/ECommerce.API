using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public IFormFile? CategoryImage { get; set; }
        public string? ImagePath { get; set; }

        public ICollection<Product>? Products { get; set; }

    }
}
