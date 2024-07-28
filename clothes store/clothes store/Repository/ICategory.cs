using clothes_store.Models;

namespace clothes_store.Repository
{
    public interface ICategory
    {
            List<Category> GetAll();
            Category GetbyId(int id);
            void Insert(Category Category);
            void Update(int id, Category Category);
            void Delete(int id);
       

    }
}
