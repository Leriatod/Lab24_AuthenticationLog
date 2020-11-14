using Microsoft.EntityFrameworkCore;

namespace Lab23.Data.Models
{
    public class AuthContext : DbContext
    {
        // bad practise to store connection strings like this
        const string CONNECTION_STRING = "Server=localhost;Database=AuthDB;Trusted_Connection=True;";

        public DbSet<User> Users { get; set; }
        public DbSet<TotalRequest> TotalRequests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
        }
    }
}