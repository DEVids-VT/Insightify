using Insightify.IdentityAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Insightify.IdentityAPI.Data
{
    public class IdentityApiDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityApiDbContext(DbContextOptions<IdentityApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

}
