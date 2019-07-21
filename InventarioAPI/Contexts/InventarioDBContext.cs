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
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public DbSet<EmailProveedor> EmailProveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TelefonoProveedor> TelefonoProveedores { get; set; }
        public DbSet<EmailCliente> EmailClientes { get; set; }
        public DbSet<TelefonoCliente> TelefonoClientes { get; set; }

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
                .HasKey(key => key.IdDetalle);

            modelBuilder.Entity<DetalleFactura>()
                .ToTable("DetalleFactura")
                .HasKey(key => key.CodigoDetalle);

            modelBuilder.Entity<EmailProveedor>()
                .ToTable("EmailProveedor")
                .HasKey(key => key.CodigoEmail);

            modelBuilder.Entity<Compra>()
                .ToTable("Compra")
                .HasKey(key => key.IdCompra);

            modelBuilder.Entity<Factura>()
                .ToTable("Factura")
                .HasKey(key => key.NumeroFactura);

            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .HasKey(key => key.CodigoProveedor);

            modelBuilder.Entity<Cliente>()
                .ToTable("Cliente")
                .HasKey(key => key.Nit);

            modelBuilder.Entity<TelefonoProveedor>()
                .ToTable("TelefonoProveedor")
                .HasKey(key => key.CodigoTelefono);

            modelBuilder.Entity<EmailCliente>()
                .ToTable("EmailCliente")
                .HasKey(key => key.CodigoEmail);

            modelBuilder.Entity<TelefonoCliente>()
                .ToTable("TelefonoCliente")
                .HasKey(key => key.CodigoTelefono);
            

            base.OnModelCreating(modelBuilder);
        }

    }
}
