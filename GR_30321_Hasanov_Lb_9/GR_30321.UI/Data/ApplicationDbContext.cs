using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GR_30321_Hasanov_Lb_3_Domain.Entities;

namespace GR_30321.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GR_30321_Hasanov_Lb_3_Domain.Entities.Perfume> Perfume { get; set; } = default!;
    }
}
