using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321.UI.Services.BrandService;
using GR_30321.UI.Services.ProductService;
using Microsoft.AspNetCore.Authorization;

namespace GR_30321.UI.Areas.Admin.Pages
{
    [Authorize(Policy = "admin")]
    public class CreateModel(
        IBrandService brandService,
        IProductService productService
    ) : PageModel {
        public async Task<IActionResult> OnGet()
        {
            var brandListData = await brandService.GetBrandListAsync();
            ViewData["BrandId"] = new SelectList(brandListData.Data, "Id","Name");

            return Page();
        }

        [BindProperty]
        public Perfume Perfume { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await productService.CreateProductAsync(Perfume, Image);

            return RedirectToPage("./Index");
        }
    }
}
