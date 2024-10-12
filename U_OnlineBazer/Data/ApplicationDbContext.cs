using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using U_OnlineBazer.Models;

namespace U_OnlineBazer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductType>ProductTypes { get; set; }
        public DbSet<SpecialTag> SpecialTags { get; set; }
    }
}
