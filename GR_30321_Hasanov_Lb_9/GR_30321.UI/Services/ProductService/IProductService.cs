using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321_Hasanov_Lb_3_Domain.Models;

namespace GR_30321.UI.Services.ProductService
{
    public interface IProductService
    {
        public Task<ResponseData<ProductListModel<Perfume>>> GetProductListAsync(
            string? brandNormalizedName,
            int pageNumber = 1
            );
        public Task<ResponseData<Perfume>> GetProductByIdAsync(int id);
        public Task UpdateProductAsync(
            int id,
            Perfume product,
            IFormFile? formFile
            );
        public Task DeleteProductAsync(int id);
        public Task<ResponseData<Perfume>> CreateProductAsync(
            Perfume product,
            IFormFile? formFile
            );
    }
}
