using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GR_30321_Api.Data;
using GR_30321_Hasanov_Lb_3_Domain.Entities;

namespace GR_30321.UI.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly GR_30321_Api.Data.AppDbContext _context;

        public EditModel(GR_30321_Api.Data.AppDbContext context)
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

            var perfume =  await _context.Perfumes.FirstOrDefaultAsync(m => m.Id == id);
            if (perfume == null)
            {
                return NotFound();
            }
            Perfume = perfume;
           ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Perfume).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfumeExists(Perfume.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PerfumeExists(int id)
        {
            return _context.Perfumes.Any(e => e.Id == id);
        }
    }
}
