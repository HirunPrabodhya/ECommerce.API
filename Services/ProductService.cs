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
    public class ProductService : IProduct
    {
        private readonly MyData _myData;
        public ProductService(MyData myData)
        {
            _myData= myData;
        }
        public async Task<string> AddProductToDB(Product product)
        {
            await _myData.products.AddAsync(product);
            await _myData.SaveChangesAsync();
            return "Product is added";
            
        }

        public async Task<List<Product>> GetAllProductFromDB()
        {
            
            return await _myData.products.ToListAsync();
        }

        public async Task<List<Product>> GetSelectedProduct(int id)
        {
            var specificProduct = await(from product in _myData.products
                                        where product.CategoryId == id
                                        select product
                                  ).ToListAsync();
            return specificProduct;
        }

        public async Task<List<Product>> SearchProductFromDB(string letter)
        {
            var searchProduct = await _myData.products.Where(c => c.Name.Contains(letter)).ToListAsync();

            return searchProduct;
        }
        
    }
}
