using clothes_store.Models;

namespace clothes_store.Repository
{
    public interface IUserCart
    {
        IEnumerable<Item> GetCartItems(string UserId);
        void AddToCart(int productId, string UserId);
        void RemoveFromCart(int productId, string UserId);
        void DecreaseFromCart(int productId, string UserId);
        void ClearCart(string UserId);

    }
}
