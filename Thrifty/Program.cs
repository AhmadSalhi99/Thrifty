using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Thrifty.Authentication;
using Thrifty.Context;
using Thrifty.Services.City;
using Thrifty.Services.Images;
using Thrifty.Services.Item;
using Thrifty.Services.ItemCategory;
using Thrifty.Services.Order;
using Thrifty.Services.OrderDetails;
using Thrifty.Services.Role;
using Thrifty.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionStringSql = builder.Configuration.GetConnectionString("sqlDatabase")!;
builder.Services.AddDbContext<ThriftyDbContext>(option => option.UseSqlServer(connectionStringSql));

builder.Services.AddScoped(typeof(IItemCategoryService), typeof(ItemCategoryService));
builder.Services.AddScoped(typeof(IItemService), typeof(ItemService));
builder.Services.AddScoped(typeof(IImagesService), typeof(ImagesService));
builder.Services.AddScoped(typeof(IUserServices), typeof(UserServices));
builder.Services.AddScoped(typeof(IRoleService), typeof(RoleService));
builder.Services.AddScoped(typeof(ICityServices), typeof(CityServices));
builder.Services.AddScoped(typeof(IOrderDetailsService), typeof(OrderDetailsService));
builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
builder.Services.AddMvc();


builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    // Configure the cookie options
    options.Cookie.Name = "AuthCookie";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use 'None' if not using HTTPS in development
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set your desired expiration time
    options.LoginPath = "/Account/Login"; // Specify the login page URL
    options.AccessDeniedPath = "/Account/AccessDenied"; // Specify the access denied page URL
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.Requirements.Add(new RoleRequirement("Admin")));
    options.AddPolicy("Seller", policy => policy.Requirements.Add(new RoleRequirement("Seller")));
    options.AddPolicy("Customer", policy => policy.Requirements.Add(new RoleRequirement("Customer")));
    options.AddPolicy("Admin,Seller", policy => policy.Requirements.Add(new RoleRequirement("Admin,Seller")));
    options.AddPolicy("Admin,Seller,Customer", policy => policy.Requirements.Add(new RoleRequirement("Admin,Seller,Customer")));
});

builder.Services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



AuthRole.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
