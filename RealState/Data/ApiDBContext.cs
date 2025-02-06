using Microsoft.EntityFrameworkCore;
using RealState.Models;

namespace RealState.Data
{
    public class ApiDBContext:DbContext
    {
        private const string _ConnectionString = "Server=.;Database=RealStateDb;Integrated Security=true;";
        public DbSet<Category> Categories{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=RealStateDb;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
