using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abaya.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // أضيفي هذا السطر لربط كلاس المنتج بقاعدة البيانات
        public DbSet<Product> Products { get; set; }
    }
}