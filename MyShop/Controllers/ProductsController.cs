
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _productObj;
        public ProductsController(IProduct product)
        {
            _productObj = product;
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] Product products)
        {
            string message = "";
            if (products == null)
            {
                return BadRequest(new
                {
                    message = "Please Add All details"
                });
            }
            else
            {
                products.ImagePath = await AzureHelper.SendToAsureDb(products.ImageFile);
                message = await _productObj.AddProductToDB(products);
            }
            return Ok(
                    new
                    {
                        message =  message
                    }
              
                );
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productObj.GetAllProductFromDB();
            return Ok(products);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetSpecificProduct([FromRoute] int id)
        {
            var selectedProduct = await _productObj.GetSelectedProduct(id);
            if (selectedProduct.Count == 0)
            {
                return BadRequest(
                    new
                    {
                        message = "There is no any products"
                    });
            }
            return Ok(selectedProduct);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> SearchProduct(string letter)
        {
            var searchProduct = await _productObj.SearchProductFromDB(letter);
            if (searchProduct.Count == 0)
            {
                return BadRequest(
                    new
                    {
                        message = "There is no any products"
                    });
            }
            return Ok(searchProduct);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct()
        {
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok();
        }
    }
}
