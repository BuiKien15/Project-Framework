using QuanLyBanHang.Models;

namespace QuanLyBanHang.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QuanLyBanHangDbContext _context;
        public LoaiSpRepository(QuanLyBanHangDbContext context)
        {
            _context = context;
        }
        public Category Add(Category category)
        {
           _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(string category_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public Category GetCategory(string category_id)
        {
            return _context.Categories.Find(category_id);
        }

        public Category Update(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
            return category;
        }
    }
}
