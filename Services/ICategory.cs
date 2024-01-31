using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICategory
    {
        public Task<string> AddCategoryToDB(Category category);
        public Task<List<Category>> GetCategoryFromDB();
        public Task<List<CategoryNameId>> GetCategoryIdNameFromDB();
       
    }
}
