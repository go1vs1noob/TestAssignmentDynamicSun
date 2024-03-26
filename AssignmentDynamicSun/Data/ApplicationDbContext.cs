using AssignmentDynamicSun.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDynamicSun.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
        }
        public DbSet<Weather> Weathers { get; set; }
    }
}
