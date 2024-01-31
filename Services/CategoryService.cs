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
    public class CategoryService : ICategory
    {
        private readonly MyData _myData;
        public CategoryService(MyData myData)
        {
            _myData = myData;
        }

        public async Task<string> AddCategoryToDB(Category category)
        {
            await _myData.categories.AddAsync(category);
            await _myData.SaveChangesAsync();
            return "Category is added";
        }

        public async Task<List<Category>> GetCategoryFromDB()
        {
            return await _myData.categories.ToListAsync();
            
        }

        public async Task<List<CategoryNameId>> GetCategoryIdNameFromDB()
        {
            List<CategoryNameId> categoryNameId = await (from category in _myData.categories
                                        select new CategoryNameId()
                                        {
                                            Id = category.Id,
                                            Name = category.Name
                                        }).ToListAsync();
            return categoryNameId;
        }
    }
}
