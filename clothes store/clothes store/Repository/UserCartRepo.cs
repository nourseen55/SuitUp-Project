using clothes_store.Models;
using Microsoft.EntityFrameworkCore;

namespace clothes_store.Repository
{
    public class UserCartRepo : IUserCart
    {
        Project context = new Project();
        public void AddToCart(int productId, string userId)
        {
            var existingItem = context.Items.FirstOrDefault(item => item.ProductId == productId && item.UserId == userId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                existingItem.IsInCart = true;
                context.Update(existingItem);
            }
            else
            {
                var newItem = new Item
                {
                    ProductId = productId,
                    Quantity = 1,
                    IsInCart = true,
                    UserId = userId
                };

                context.Items.Add(newItem);
            }

            context.SaveChanges();
        }

        public void ClearCart(string userId)
        {
            var cartItems = context.Items.Where(item => item.UserId == userId && item.IsInCart).ToList();
            foreach (var item in cartItems)
            {
                item.IsInCart = false;
               
            }
            context.SaveChanges();
        }
        public void DecreaseFromCart(int productId, string UserId)
        {
            var existingItem = context.Items.FirstOrDefault(item => item.ProductId == productId && item.UserId == UserId);
            existingItem.Quantity--;
            context.Update(existingItem);
            context.SaveChanges();
        }

        public IEnumerable<Item> GetCartItems(string userId)
        {
            return context.Items.Include(item => item.Product).Where(item => item.UserId == userId).ToList();
        }


        public void RemoveFromCart(int productId, string userId)
        {
            var itemToRemove = context.Items.FirstOrDefault(item => item.ProductId == productId && item.UserId == userId);

            if (itemToRemove != null)
            {
                context.Items.Remove(itemToRemove);
                context.SaveChanges();
            }
        }

    }
}
