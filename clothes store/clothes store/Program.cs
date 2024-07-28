using clothes_store.Models;
using clothes_store.Repository;
using clothes_store.ViewComponents;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Project>(optionBuilder => {
    optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});

// Add services to the container.


////builder.Services.AddScoped<ICategory,CategoryRepo>();
//builder.Services.AddScoped<ICategory, CategoryRepository>();
//builder.Services.AddScoped<IProduct, ProductRepository>();
////builder.Services.AddScoped<IInventory, InventoryRepo>();
//builder.Services.AddScoped<IUserCart, UserCartRepo>();
////builder.Services.AddScoped<Project>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<Project>();

builder.Services.AddAuthentication().AddGoogle(options =>
{
    IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Auth:Google");
    options.ClientId = googleAuthSection["ClientId"];
    options.ClientSecret = googleAuthSection["ClientSecret"];

});
//    .AddFacebook(facebookoptions =>
//{
//    facebookoptions.AppId = builder.Configuration["Auth:Facebook:AppId"];
//    facebookoptions.AppSecret = builder.Configuration["Auth:Facebook:AppSecret"];

//});
builder.Services.AddScoped<RecommendedViewComponent>();

builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<Icheckout, CheckoutRepo>();
builder.Services.AddScoped<IUserCart, UserCartRepo>();

builder.Services.AddScoped<ProductRepository>();
//builder.Services.AddDbContext<Project>(op =>
// op.UseSqlServer("Data Source=.;Initial Catalog=clothesDB ;Integrated Security=True"));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
