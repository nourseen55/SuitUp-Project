using clothes_store.Models;

namespace clothes_store.Repository
{
    public class CategoryRepository : ICategory
    {
        Project context=new Project();
        public List<Category> GetAll()
        {
            List<Category> SmallCat = context.Categories.ToList();
            return SmallCat;
        }
        public Category GetbyId(int id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public void Insert(Category Category)
        {
            context.Categories.Add(Category);
            context.SaveChanges();
        }
        public void Update(int id, Category newCat)
        {
            Category oldcategory = GetbyId(id);
            oldcategory.Name = newCat.Name;
            oldcategory.Image = newCat.Image;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Category Cats = GetbyId(id);
            context.Categories.Remove(Cats);
            context.SaveChanges();
        }
    }
}
