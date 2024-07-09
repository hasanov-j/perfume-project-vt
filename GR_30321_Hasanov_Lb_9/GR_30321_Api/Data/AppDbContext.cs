using GR_30321_Hasanov_Lb_3_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace GR_30321_Api.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Perfume> Perfumes { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
