using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProduct
    {
        Task<string> AddProductToDB(Product product);
        Task<List<Product>> GetAllProductFromDB();
       Task<List<Product>> GetSelectedProduct(int id);
        Task<List<Product>> SearchProductFromDB(string letter);

    }
}
