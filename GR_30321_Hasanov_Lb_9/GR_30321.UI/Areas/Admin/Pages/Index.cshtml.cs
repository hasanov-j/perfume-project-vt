using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GR_30321_Api.Data;
using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321.UI.Services.ProductService;
using Microsoft.AspNetCore.Authorization;

namespace GR_30321.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Perfume> Perfume { get;set; } = default!;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public async Task OnGetAsync(int pageNumber=1)
        {
            var response = await _productService.GetProductListAsync(null, pageNumber);
            if (response.Success)
            {
                Perfume = response.Data.Items;
                CurrentPage = response.Data.CurrentPage;
                TotalPages = response.Data.TotalPages;
            }
        }
    }
}
