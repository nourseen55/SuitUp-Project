using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace clothes_store.Models
{
    public class Project:IdentityDbContext<ApplicationUser>
    {
        public Project():base()
        {
            
        }
        public Project(DbContextOptions options):base(options)
        {

        }
        
        public DbSet<Product> Products { get; set;}
        public DbSet<Category> Categories { get; set;}
        public DbSet<LargeCategory> LargeCategories { get; set; }
        public DbSet<stylepro> stylepros { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<CheckoutData> CheckoutData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=db4967.public.databaseasp.net; Database=db4967; User Id=db4967; Password=qN_3!4ZjKc8?; Encrypt=False; MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);

        }
    }
}
