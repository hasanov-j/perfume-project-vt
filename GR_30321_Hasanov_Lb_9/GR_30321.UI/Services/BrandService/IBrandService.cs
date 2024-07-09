using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321_Hasanov_Lb_3_Domain.Models;

namespace GR_30321.UI.Services.BrandService
{
    public interface IBrandService
    {
        public Task<ResponseData<List<Brand>>> GetBrandListAsync();
    }
}
