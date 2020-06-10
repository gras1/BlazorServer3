using Microsoft.EntityFrameworkCore;
using System;

namespace BlazorServer3.Database
{
    public interface IDbContext : IDisposable
    {
        void RecreateDatabase();

        DbSet<Category> Categories { get; set; }

        DbSet<Product> Products { get; set; }

        int SaveChanges();
    }
}
