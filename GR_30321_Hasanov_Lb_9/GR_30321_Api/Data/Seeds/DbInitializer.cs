using GR_30321_Hasanov_Lb_3_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GR_30321_Api.Data.Seeds
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            // Заполнение данными
            if (!context.Brands.Any() && !context.Perfumes.Any())
            {
                var brands = new Brand[]
                {
                    new Brand { Name = "Chanel", NormalizedName = "chanel" },
                    new Brand { Name = "Dior", NormalizedName = "dior" },
                    new Brand { Name = "Gucci", NormalizedName = "gucci" },
                    new Brand { Name = "Versace", NormalizedName = "versace" },
                    new Brand { Name = "Yves Saint Laurent", NormalizedName = "yves-saint-laurent" },
                    new Brand { Name = "Tom Ford", NormalizedName = "tom-ford" },
                    new Brand { Name = "Calvin Klein", NormalizedName = "calvin-klein" },
                    new Brand { Name = "Bvlgari", NormalizedName = "bvlgari" },
                    new Brand { Name = "Jo Malone", NormalizedName = "jo-malone" },
                    new Brand { Name = "Hermès", NormalizedName = "hermes" }
                };
                await context.Brands.AddRangeAsync(brands);
                await context.SaveChangesAsync();
                var perfumes = new List<Perfume>
                {
                    new Perfume
                    {
                        Name = "Chanel No. 5", 
                        Description = "Classic floral fragrance", 
                        Price = 150, 
                        Image = uri + "images/сhanel-no-5.jpeg", 
                        BrandId = 1,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("chanel")),
                    },
                    new Perfume
                    {
                        Name = "Bleu de Chanel", 
                        Description = "Fresh and woody scent", 
                        Price = 120, 
                        Image =uri + "images/bleu-de-chanel.jpg", 
                        BrandId = 1,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("chanel")),
                    },

                    new Perfume
                    {
                        Name = "Dior Sauvage", 
                        Description = "Spicy and fresh fragrance", 
                        Price = 130, 
                        Image =uri + "images/dior-sauvage.jpg", 
                        BrandId = 2,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("dior")),
                    }, 
                    new Perfume
                    {
                        Name = "J'adore", 
                        Description = "Floral and fruity scent", 
                        Price = 140, 
                        Image = uri + "images/j-adore.jpg", 
                        BrandId = 2,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("dior")),
                    },

                    new Perfume
                    {
                        Name = "Gucci Bloom", 
                        Description = "Floral fragrance", 
                        Price = 110, 
                        Image = uri + "images/gucci-bloom.jpg", 
                        BrandId = 3,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("gucci")),
                    },
                    new Perfume
                    {
                        Name = "Guilty", 
                        Description = "Warm and spicy scent", 
                        Price = 125, 
                        Image = uri + "images/guilty.jpg", 
                        BrandId = 3,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("gucci")),
                    }, 

                    new Perfume
                    {
                        Name = "Versace Eros", 
                        Description = "Fresh and oriental fragrance", 
                        Price = 200, 
                        Image = uri + "images/versace-eros.jpg", 
                        BrandId = 4,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("versace")),
                    },
                    new Perfume
                    {
                        Name = "Bright Crystal", 
                        Description = "Fruity and floral scent", 
                        Price = 100, 
                        Image = uri + "images/guilty.jpg", 
                        BrandId = 4,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("versace")),
                    },

                    new Perfume
                    {
                        Name = "Black Opium", 
                        Description = "Warm and spicy fragrance", 
                        Price = 135, 
                        Image = uri + "images/black-opium.jpg", 
                        BrandId = 5,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("yves-saint-laurent")),
                    },
                    new Perfume
                    {
                        Name = "Libre", 
                        Description = "Floral and lavender scent", 
                        Price = 130, 
                        Image = uri + "images/libre.jpg", 
                        BrandId = 5,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("yves-saint-laurent")),
                    },

                    new Perfume
                    {
                        Name = "Tom Ford Noir", 
                        Description = "Warm and spicy fragrance",
                        Price = 200, 
                        Image = uri + "images/tom-ford-noir.jpg", 
                        BrandId = 6,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("tom-ford")),
                    },
                    new Perfume
                    {
                        Name = "Black Orchid", 
                        Description = "Oriental and floral scent", 
                        Price = 190, 
                        Image = uri + "images/black-orchid.jpg", 
                        BrandId = 6,
                        Brand=brands.FirstOrDefault(b => b.NormalizedName.Equals("tom-ford")),
                    },
                };
                await context.AddRangeAsync(perfumes);
                await context.SaveChangesAsync();
            }
        }
    }
}
