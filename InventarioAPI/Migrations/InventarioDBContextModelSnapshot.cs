﻿// <auto-generated />
using System;
using InventarioAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventarioAPI.Migrations
{
    [DbContext(typeof(InventarioDBContext))]
    partial class InventarioDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InventarioAPI.Entities.Categoria", b =>
                {
                    b.Property<int>("codigoCategoria")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("descripcion")
                        .IsRequired();

                    b.HasKey("codigoCategoria");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("InventarioAPI.Entities.Cliente", b =>
                {
                    b.Property<string>("Nit")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DPI")
                        .IsRequired();

                    b.Property<string>("Direccion")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Nit");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("InventarioAPI.Entities.Compra", b =>
                {
                    b.Property<int>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoProveedor");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("NumeroDocumento");

                    b.Property<decimal>("Total");

                    b.HasKey("IdCompra");

                    b.HasIndex("CodigoProveedor");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("InventarioAPI.Entities.DetalleCompra", b =>
                {
                    b.Property<int>("IdDetalle")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad");

                    b.Property<int>("CodigoProducto");

                    b.Property<int>("IdCompra");

                    b.Property<decimal>("Precio");

                    b.HasKey("IdDetalle");

                    b.HasIndex("CodigoProducto");

                    b.HasIndex("IdCompra");

                    b.ToTable("DetalleCompra");
                });

            modelBuilder.Entity("InventarioAPI.Entities.DetalleFactura", b =>
                {
                    b.Property<int>("CodigoDetalle")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad");

                    b.Property<int>("CodigoProducto");

                    b.Property<decimal>("Descuento");

                    b.Property<int>("NumeroFactura");

                    b.Property<decimal>("Precio");

                    b.HasKey("CodigoDetalle");

                    b.HasIndex("CodigoProducto");

                    b.HasIndex("NumeroFactura");

                    b.ToTable("DetalleFactura");
                });

            modelBuilder.Entity("InventarioAPI.Entities.EmailCliente", b =>
                {
                    b.Property<int>("CodigoEmail")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nit")
                        .IsRequired();

                    b.HasKey("CodigoEmail");

                    b.HasIndex("Nit");

                    b.ToTable("EmailCliente");
                });

            modelBuilder.Entity("InventarioAPI.Entities.EmailProveedor", b =>
                {
                    b.Property<int>("CodigoEmail")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoProveedor");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.HasKey("CodigoEmail");

                    b.HasIndex("CodigoProveedor");

                    b.ToTable("EmailProveedor");
                });

            modelBuilder.Entity("InventarioAPI.Entities.Factura", b =>
                {
                    b.Property<int>("NumeroFactura")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Nit")
                        .IsRequired();

                    b.Property<decimal>("Total");

                    b.HasKey("NumeroFactura");

                    b.HasIndex("Nit");

                    b.ToTable("Factura");
                });

            modelBuilder.Entity("InventarioAPI.Entities.Inventario", b =>
                {
                    b.Property<int>("CodigoInventario")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoProducto");

                    b.Property<int>("Entradas");

                    b.Property<DateTime>("Fecha");

                    b.Property<decimal>("Precio");

                    b.Property<int>("Salidas");

                    b.Property<string>("TipoRegistro")
                        .IsRequired();

                    b.HasKey("CodigoInventario");

                    b.HasIndex("CodigoProducto");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("InventarioAPI.Entities.Producto", b =>
                {
                    b.Property<int>("codigoProducto")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("codigoCategoria");

                    b.Property<int>("codigoEmpaque");

                    b.Property<string>("descripcion")
                        .IsRequired();

                    b.Property<int>("existencia");

                    b.Property<string>("imagen")
                        .IsRequired();

                    b.Property<decimal>("precioPorDocena");

                    b.Property<decimal>("precioPorMayor");

                    b.Property<decimal>("precioUnitario");

                    b.HasKey("codigoProducto");

                    b.HasIndex("codigoCategoria");

                    b.HasIndex("codigoEmpaque");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("InventarioAPI.Entities.Proveedor", b =>
                {
                    b.Property<int>("CodigoProveedor")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactoPrincipal")
                        .IsRequired();

                    b.Property<string>("Direccion")
                        .IsRequired();

                    b.Property<string>("Nit")
                        .IsRequired();

                    b.Property<string>("PaginaWeb")
                        .IsRequired();

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.HasKey("CodigoProveedor");

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("InventarioAPI.Entities.TelefonoCliente", b =>
                {
                    b.Property<int>("CodigoTelefono")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.Property<string>("Nit")
                        .IsRequired();

                    b.Property<string>("Numero")
                        .IsRequired();

                    b.HasKey("CodigoTelefono");

                    b.HasIndex("Nit");

                    b.ToTable("TelefonoCliente");
                });

            modelBuilder.Entity("InventarioAPI.Entities.TelefonoProveedor", b =>
                {
                    b.Property<int>("CodigoTelefono")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoProveedor");

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.Property<string>("Numero")
                        .IsRequired();

                    b.HasKey("CodigoTelefono");

                    b.HasIndex("CodigoProveedor");

                    b.ToTable("TelefonoProveedor");
                });

            modelBuilder.Entity("InventarioAPI.Entities.TipoEmpaque", b =>
                {
                    b.Property<int>("codigoEmpaque")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("descripcion")
                        .IsRequired();

                    b.HasKey("codigoEmpaque");

                    b.ToTable("TipoEmpaque");
                });

            modelBuilder.Entity("InventarioAPI.Entities.Compra", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Proveedor", "Proveedor")
                        .WithMany("Compras")
                        .HasForeignKey("CodigoProveedor")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.DetalleCompra", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("CodigoProducto")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InventarioAPI.Entities.Compra", "Compras")
                        .WithMany("DetalleCompras")
                        .HasForeignKey("IdCompra")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.DetalleFactura", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("CodigoProducto")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InventarioAPI.Entities.Factura", "Factura")
                        .WithMany("DetalleFacturas")
                        .HasForeignKey("NumeroFactura")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.EmailCliente", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Cliente", "Cliente")
                        .WithMany("EmailClientes")
                        .HasForeignKey("Nit")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.EmailProveedor", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Proveedor", "Proveedor")
                        .WithMany("EmailProveedores")
                        .HasForeignKey("CodigoProveedor")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.Factura", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Cliente", "Cliente")
                        .WithMany("Facturas")
                        .HasForeignKey("Nit")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.Inventario", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("CodigoProducto")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.Producto", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("codigoCategoria")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InventarioAPI.Entities.TipoEmpaque", "TipoEmpaque")
                        .WithMany()
                        .HasForeignKey("codigoEmpaque")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.TelefonoCliente", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Cliente", "Cliente")
                        .WithMany("TelefonoClientes")
                        .HasForeignKey("Nit")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InventarioAPI.Entities.TelefonoProveedor", b =>
                {
                    b.HasOne("InventarioAPI.Entities.Proveedor", "Proveedor")
                        .WithMany("TelefonoProveedores")
                        .HasForeignKey("CodigoProveedor")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
