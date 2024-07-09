using GR_30321.UI.Data;
using GR_30321.UI.Data.Seeds;
using GR_30321.UI.Services.BrandService;
using GR_30321.UI.Services.ProductService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("admin", p =>
    p.RequireClaim(ClaimTypes.Role, "admin"));
});
builder.Services.AddSingleton<IEmailSender, NoOpEmailSender>();

// Register IBrandService as scoped
//builder.Services.AddScoped<IBrandService, MemoryBrandService>();
builder.Services.AddScoped<IBrandService, ApiBrandService>();
// Register IProductService as scoped
//builder.Services.AddScoped<IProductService, MemoryProductService>();
builder.Services.AddScoped<IProductService, ApiProductService>();
// Register IhttpContextAccessor as scoped
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient<IProductService, ApiProductService>(opt
=> opt.BaseAddress = new Uri("https://localhost:7002/api/perfumes/"));

builder.Services.AddHttpClient<IBrandService, ApiBrandService>(opt
=> opt.BaseAddress = new Uri("https://localhost:7002/api/brands/"));


builder.Services.AddControllersWithViews();

var app = builder.Build();

await DbInit.SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
