using GR_30321.UI.Services.BrandService;
using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321_Hasanov_Lb_3_Domain.Models;
using System.Threading.Channels;

namespace GR_30321.UI.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        List<Brand> _brands;
        List<Perfume> _perfumes;
        IConfiguration _config;

        public MemoryProductService(
            IBrandService brandService,
            IConfiguration config
        ) {
            _brands = brandService.GetBrandListAsync().Result.Data;
            _config = config;

            SetupData();
        }

        private void SetupData()
        {
            _perfumes = new List<Perfume>
            {
                new Perfume { Id = 1, Name = "Chanel No. 5", Description = "Classic floral fragrance", Price = 150, Image = "images/сhanel-no-5.jpeg", BrandId = 1 },
                new Perfume { Id = 2, Name = "Bleu de Chanel", Description = "Fresh and woody scent", Price = 120, Image = "images/bleu-de-chanel.jpg", BrandId = 1 },

                new Perfume { Id = 3, Name = "Dior Sauvage", Description = "Spicy and fresh fragrance", Price = 130, Image ="images/dior-sauvage.jpg", BrandId = 2 },
                new Perfume { Id = 4, Name = "J'adore", Description = "Floral and fruity scent", Price = 140, Image = "images/j-adore.jpg", BrandId = 2 },

                new Perfume { Id = 5, Name = "Gucci Bloom", Description = "Floral fragrance", Price = 110, Image = "images/gucci-bloom.jpg", BrandId = 3 },
                new Perfume { Id = 6, Name = "Guilty", Description = "Warm and spicy scent", Price = 125, Image = "images/guilty.jpg", BrandId = 3 },

                new Perfume { Id = 7, Name = "Versace Eros", Description = "Fresh and oriental fragrance", Price = 200, Image = "images/versace-eros.jpg", BrandId = 4 },
                new Perfume { Id = 8, Name = "Bright Crystal", Description = "Fruity and floral scent", Price = 100, Image = "images/guilty.jpg", BrandId = 4 },

                new Perfume { Id = 9, Name = "Black Opium", Description = "Warm and spicy fragrance", Price = 135, Image = "images/black-opium.jpg", BrandId = 5 },
                new Perfume { Id = 10, Name = "Libre", Description = "Floral and lavender scent", Price = 130, Image = "images/libre.jpg", BrandId = 5 },

                new Perfume { Id = 11, Name = "Tom Ford Noir", Description = "Warm and spicy fragrance", Price = 200, Image = "images/tom-ford-noir.jpg", BrandId = 6 },
                new Perfume { Id = 12, Name = "Black Orchid", Description = "Oriental and floral scent", Price = 190, Image = "images/black-orchid.jpg", BrandId = 6 },

                //new Perfume { Id = 13, Name = "Eternity", Description = "Fresh and floral fragrance", Price = 90, Image = "images/dior-sauvage.jpg", BrandId = 7 },
                //new Perfume { Id = 14, Name = "CK One", Description = "Citrus and aromatic scent", Price = 85, Image = "images/dior-sauvage.jpg", BrandId = 7 },

                //new Perfume { Id = 15, Name = "Bvlgari Man in Black", Description = "Warm and spicy fragrance", Price = 160, Image = "images/dior-sauvage.jpg", BrandId = 8 },
                //new Perfume { Id = 16, Name = "Omnia Crystalline", Description = "Floral and woody scent", Price = 140, Image = "images/dior-sauvage.jpg", BrandId = 8 },

                //new Perfume { Id = 17, Name = "Jo Malone Peony & Blush Suede", Description = "Floral and fruity fragrance", Price = 150, Image = "images/dior-sauvage.jpg", BrandId = 9 },
                //new Perfume { Id = 18, Name = "Wood Sage & Sea Salt", Description = "Fresh and woody scent", Price = 140, Image = "images/dior-sauvage.jpg", BrandId = 9 },

                //new Perfume { Id = 19, Name = "Hermès Terre d'Hermès", Description = "Earthy and woody fragrance", Price = 170, Image = "images/dior-sauvage.jpg", BrandId = 10 },
                //new Perfume { Id = 20, Name = "Twilly d'Hermès", Description = "Floral and spicy scent", Price = 160, Image = "images/dior-sauvage.jpg", BrandId = 10 }
            };
        }

        public Task<ResponseData<Perfume>> CreateProductAsync(Perfume product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Perfume>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ProductListModel<Perfume>>> GetProductListAsync(string? brandNormalizedName, int pageNumber = 1)
        {
            var result = new ResponseData<ProductListModel<Perfume>>();
            int? brandId = null;

            if (brandNormalizedName != null)
            {
                brandId = _brands.Find(b =>
                b.NormalizedName.Equals(brandNormalizedName))
                ?.Id;
            }

            var data = _perfumes.Where(p => brandId == null ||
                p.BrandId.Equals(brandId))
                ?.ToList();

            result.Data = new ProductListModel<Perfume>()
            {
                Items = data
            };

            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            // получить данные страницы
            var listData = new ProductListModel<Perfume>()
            {
                Items = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }

            return Task.FromResult(result);
        }

        public Task UpdateProductAsync(int id, Perfume product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
