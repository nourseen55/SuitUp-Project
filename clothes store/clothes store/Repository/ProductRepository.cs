using clothes_store.Models;

namespace clothes_store.Repository
{
    public class ProductRepository:IProduct
    {
       Project context = new Project();
        public List<Product> GetAll()
        {
            List<Product> products = context.Products.ToList();
            return products;
        }
        public Product GetbyId(int id)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
        }
        public Product GetbyName(string name)
        {
            return context.Products.FirstOrDefault(p => p.Name == name);
        }
        public void Insert(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
        public void Update(int id, Product NewProduct)
        {
            Product oldproduct = GetbyId(id);
            oldproduct.Name = NewProduct.Name;
            oldproduct.Price = NewProduct.Price;
            oldproduct.Category_id = NewProduct.Category_id;
            context.SaveChanges();



            oldproduct.urlimg = NewProduct.urlimg;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Product prod=GetbyId(id);
            context.Products.Remove(prod);
            context.SaveChanges();
        }
        public List<Product> top10pro()
        {
            List<Product> pros = context.Products.Skip(88).Take(25).ToList();
            return pros;
        }

    }
}
