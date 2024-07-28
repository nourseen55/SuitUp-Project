using clothes_store.Models;

namespace clothes_store.Repository
{
    public class CheckoutRepo:Icheckout
    {
        Project context = new Project();
        public void Add(CheckoutData data)
        {
            context.CheckoutData.Add(data);
            context.SaveChanges();
        }
    }
}
