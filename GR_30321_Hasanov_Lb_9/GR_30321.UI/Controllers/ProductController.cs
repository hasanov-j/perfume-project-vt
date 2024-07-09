using GR_30321.UI.Services.BrandService;
using GR_30321.UI.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace GR_30321.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;
        public ProductController(IBrandService brandService, IProductService productService) 
        {
            _brandService = brandService;
            _productService = productService;

        }
        [Route("Products/{brand?}")]
        public async Task<IActionResult> Index(string? brand, int pageNumber=1)
        {
            var brandsRespnonse = await _brandService.GetBrandListAsync();
            if (!brandsRespnonse.Success) return NotFound(brandsRespnonse.ErrorMessage);
            ViewData["brands"] = brandsRespnonse.Data;

            var currentBrand = brand == null ?
                "Все":
                brandsRespnonse.Data.FirstOrDefault(b => b.NormalizedName == brand)?.Name;
            ViewData["currentBrand"] = currentBrand;

            var productResponse = await _productService.GetProductListAsync(brand, pageNumber);
            if (!productResponse.Success) return NotFound(productResponse.ErrorMessage);

            return View(productResponse.Data);
        }
    }
}
