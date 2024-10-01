using Item_Code_management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Item_Code_management_System.Services
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User>Users { get; set; }

        public DbSet<Category>Categories { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<UserItemCodeMapping> ItemCodeMappings { get; set; }
    }
}


