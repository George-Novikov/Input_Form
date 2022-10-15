using Microsoft.EntityFrameworkCore;
using Input_Form.Models;

namespace Input_Form
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Indicator> Indicators { get; set; } = null!;
        public DbSet<Form> Forms { get; set; } = null!;
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=InputForms;Trusted_Connection=True");
        }
    }
}
