using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GR_30321_Api.Data;
using GR_30321_Hasanov_Lb_3_Domain.Entities;

namespace GR_30321.UI.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly GR_30321_Api.Data.AppDbContext _context;

        public DeleteModel(GR_30321_Api.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Perfume Perfume { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes.FirstOrDefaultAsync(m => m.Id == id);

            if (perfume == null)
            {
                return NotFound();
            }
            else
            {
                Perfume = perfume;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume != null)
            {
                Perfume = perfume;
                _context.Perfumes.Remove(Perfume);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
