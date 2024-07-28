using clothes_store.Models;

namespace clothes_store.Repository
{
    public interface IProduct
    {
        List<Product> GetAll();
        Product GetbyId(int id);
        Product GetbyName(string name);
        void Insert(Product product);
        void Update(int id, Product NewProduct);
        void Delete(int id);
        List<Product> top10pro();

    }
}
