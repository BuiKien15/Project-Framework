using k.Models;
namespace k.Repository
{
    public interface ILoaiSpRepository
    {
        Category Add(Category category);
        Category Update(Category category);
        Category Delete(string category_id);
        Category GetCategory(string category_id);
        IEnumerable<Category> GetAllCategories();

    }
}
