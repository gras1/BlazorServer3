using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace BlazorServer3.Database
{
    public class MyDbContext : DbContext, IDbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public void RecreateDatabase()
        {
            if (File.Exists(DbName))
            {
                File.Delete(DbName);
            }
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=" + DbName, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);
            base.OnModelCreating(modelBuilder);
        }

        private string DbName
        {
            get
            {
                string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                string parentPath = Path.GetFullPath(Path.Combine(currentPath, ".."));

                if (parentPath.EndsWith("BlazorServer3"))
                {
                    return parentPath + "/TestDatabase.db";
                }
                while (!parentPath.EndsWith("BlazorServer3"))
                {
                    parentPath = Path.GetFullPath(Path.Combine(parentPath, ".."));
                };
                return parentPath + "/TestDatabase.db";
            }
        }
    }
}
