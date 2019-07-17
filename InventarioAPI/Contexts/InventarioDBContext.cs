using InventarioAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Contexts
{
    public class InventarioDBContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TipoEmpaque> TipoEmpaques { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }

        public InventarioDBContext(DbContextOptions<InventarioDBContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .ToTable("Categoria")
                .HasKey(key => key.CodigoCategoria);
                        
            modelBuilder.Entity<TipoEmpaque>()
                .ToTable("TipoEmpaque")
                .HasKey(key => key.CodigoEmpaque);

            modelBuilder.Entity<Inventario>()
                .ToTable("Inventario")
                .HasKey(key => key.CodigoInventario);

            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .HasKey(key => key.CodigoProducto);

            modelBuilder.Entity<DetalleCompra>()
                .ToTable("DetalleCompra")
                .HasKey(key => key.IdCompra);
            

            base.OnModelCreating(modelBuilder);
        }

    }
}
