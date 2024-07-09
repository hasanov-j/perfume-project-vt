using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321_Hasanov_Lb_3_Domain.Models;

namespace GR_30321.UI.Services.BrandService
{
    public class MemoryBrandService : IBrandService
    {
        public Task<ResponseData<List<Brand>>> GetBrandListAsync()
        {
            var brands = new List<Brand>
            {
            new Brand { Id = 1, Name = "Chanel", NormalizedName = "chanel" },
            new Brand { Id = 2, Name = "Dior", NormalizedName = "dior" },
            new Brand { Id = 3, Name = "Gucci", NormalizedName = "gucci" },
            new Brand { Id = 4, Name = "Versace", NormalizedName = "versace" },
            new Brand { Id = 5, Name = "Yves Saint Laurent", NormalizedName = "yves-saint-laurent" },
            new Brand { Id = 6, Name = "Tom Ford", NormalizedName = "tom-ford" },
            new Brand { Id = 7, Name = "Calvin Klein", NormalizedName = "calvin-klein" },
            new Brand { Id = 8, Name = "Bvlgari", NormalizedName = "bvlgari" },
            new Brand { Id = 9, Name = "Jo Malone", NormalizedName = "jo-malone" },
            new Brand { Id = 10, Name = "Hermès", NormalizedName = "hermes" }
            };

            var result = new ResponseData<List<Brand>>();
            result.Data = brands;

            return Task.FromResult(result);
        }
    }
}
