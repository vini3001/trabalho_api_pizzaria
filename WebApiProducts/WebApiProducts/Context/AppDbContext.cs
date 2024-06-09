using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProducts.Models;

namespace WebApiProducts.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .Property(props => props.Nome)
                .HasMaxLength(80);

            modelBuilder.Entity<Pizza>()
                .Property(props => props.Preco)
                .HasPrecision(10, 2);
        }
    }
}
