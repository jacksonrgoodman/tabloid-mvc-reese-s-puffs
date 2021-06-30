using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();

        Category GetCategoryById(int id);

        void CreateCategory(Category category);

        void DeleteCategory(int categoryId);

        void EditCategory(Category category);
    }
}