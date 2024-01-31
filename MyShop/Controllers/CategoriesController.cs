using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _categoryObj;
        public CategoriesController(ICategory category)
        {
            _categoryObj= category;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] Category category)
        {
            string message = "";
            if (category == null)
            {
                return BadRequest(
                        new
                        {
                            message = "Please add all details"
                        }
                    );
            }
            else
            {
               category.ImagePath = await AzureHelper.SendToAsureDb(category.CategoryImage);
               
               message = await _categoryObj.AddCategoryToDB(category);
            }
            return Ok(
                new
                {
                    message = message
                }

                ) ;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategory()
        {
            var category = await _categoryObj.GetCategoryFromDB();
            if(category == null)
            {
                return BadRequest(new
                {
                    message = "There is no any category"
                });
            }
            return Ok(category);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCategoryIdName()
        {
            var categoryIdName = await _categoryObj.GetCategoryIdNameFromDB();
            return Ok(categoryIdName);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory()
        {
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory()
        {
            return Ok();
        }
        
    }
}
