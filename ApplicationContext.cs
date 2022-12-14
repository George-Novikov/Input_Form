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
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Input_Form;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Indicator>().HasKey(e => e.IndicatorId);

            modelBuilder.Entity<Form>().HasKey(e => e.FormId);
            modelBuilder.Entity<Form>().HasOne(e => e.IndicatorA).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Form>().HasOne(e => e.IndicatorB).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Form>().HasOne(e => e.IndicatorC).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Form>().HasOne(e => e.Discriminant).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Form>().HasOne(e => e.FirstResult).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Form>().HasOne(e => e.SecondResult).WithMany().OnDelete(DeleteBehavior.NoAction);
            
        }
    }
}
