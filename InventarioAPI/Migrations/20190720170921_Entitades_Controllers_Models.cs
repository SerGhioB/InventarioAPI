using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventarioAPI.Migrations
{
    public partial class Entitades_Controllers_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Nit = table.Column<string>(nullable: false),
                    DPI = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Nit);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompra",
                columns: table => new
                {
                    IdCompra = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdDetalle = table.Column<int>(nullable: false),
                    CodigoProducto = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompra", x => x.IdCompra);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    CodigoProveedor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nit = table.Column<string>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(nullable: false),
                    PaginaWeb = table.Column<string>(nullable: false),
                    ContactoPrincipal = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.CodigoProveedor);
                });

            migrationBuilder.CreateTable(
                name: "EmailCliente",
                columns: table => new
                {
                    CodigoEmail = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    Nit = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailCliente", x => x.CodigoEmail);
                    table.ForeignKey(
                        name: "FK_EmailCliente_Cliente_Nit",
                        column: x => x.Nit,
                        principalTable: "Cliente",
                        principalColumn: "Nit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    NumeroFactura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nit = table.Column<string>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.NumeroFactura);
                    table.ForeignKey(
                        name: "FK_Factura_Cliente_Nit",
                        column: x => x.Nit,
                        principalTable: "Cliente",
                        principalColumn: "Nit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelefonoCliente",
                columns: table => new
                {
                    CodigoTelefono = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false),
                    Nit = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonoCliente", x => x.CodigoTelefono);
                    table.ForeignKey(
                        name: "FK_TelefonoCliente_Cliente_Nit",
                        column: x => x.Nit,
                        principalTable: "Cliente",
                        principalColumn: "Nit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    IdCompra = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroDocumento = table.Column<int>(nullable: false),
                    CodigoProveedor = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.IdCompra);
                    table.ForeignKey(
                        name: "FK_Compra_Proveedor_CodigoProveedor",
                        column: x => x.CodigoProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "CodigoProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailProveedor",
                columns: table => new
                {
                    CodigoEmail = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    CodigoProveedor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailProveedor", x => x.CodigoEmail);
                    table.ForeignKey(
                        name: "FK_EmailProveedor_Proveedor_CodigoProveedor",
                        column: x => x.CodigoProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "CodigoProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelefonoProveedor",
                columns: table => new
                {
                    CodigoTelefono = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Numero = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false),
                    CodigoProveedor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelefonoProveedor", x => x.CodigoTelefono);
                    table.ForeignKey(
                        name: "FK_TelefonoProveedor_Proveedor_CodigoProveedor",
                        column: x => x.CodigoProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "CodigoProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleFactura",
                columns: table => new
                {
                    CodigoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroFactura = table.Column<int>(nullable: false),
                    CodigoProducto = table.Column<int>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio = table.Column<decimal>(nullable: false),
                    Descuento = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFactura", x => x.CodigoDetalle);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Producto_CodigoProducto",
                        column: x => x.CodigoProducto,
                        principalTable: "Producto",
                        principalColumn: "CodigoProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Factura_NumeroFactura",
                        column: x => x.NumeroFactura,
                        principalTable: "Factura",
                        principalColumn: "NumeroFactura",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CodigoEmpaque",
                table: "Producto",
                column: "CodigoEmpaque");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_CodigoProducto",
                table: "Inventario",
                column: "CodigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_CodigoProveedor",
                table: "Compra",
                column: "CodigoProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_CodigoProducto",
                table: "DetalleFactura",
                column: "CodigoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_NumeroFactura",
                table: "DetalleFactura",
                column: "NumeroFactura");

            migrationBuilder.CreateIndex(
                name: "IX_EmailCliente_Nit",
                table: "EmailCliente",
                column: "Nit");

            migrationBuilder.CreateIndex(
                name: "IX_EmailProveedor_CodigoProveedor",
                table: "EmailProveedor",
                column: "CodigoProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_Nit",
                table: "Factura",
                column: "Nit");

            migrationBuilder.CreateIndex(
                name: "IX_TelefonoCliente_Nit",
                table: "TelefonoCliente",
                column: "Nit");

            migrationBuilder.CreateIndex(
                name: "IX_TelefonoProveedor_CodigoProveedor",
                table: "TelefonoProveedor",
                column: "CodigoProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_Producto_CodigoProducto",
                table: "Inventario",
                column: "CodigoProducto",
                principalTable: "Producto",
                principalColumn: "CodigoProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_TipoEmpaque_CodigoEmpaque",
                table: "Producto",
                column: "CodigoEmpaque",
                principalTable: "TipoEmpaque",
                principalColumn: "CodigoEmpaque",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_Producto_CodigoProducto",
                table: "Inventario");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_TipoEmpaque_CodigoEmpaque",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "DetalleCompra");

            migrationBuilder.DropTable(
                name: "DetalleFactura");

            migrationBuilder.DropTable(
                name: "EmailCliente");

            migrationBuilder.DropTable(
                name: "EmailProveedor");

            migrationBuilder.DropTable(
                name: "TelefonoCliente");

            migrationBuilder.DropTable(
                name: "TelefonoProveedor");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Producto_CodigoEmpaque",
                table: "Producto");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_CodigoProducto",
                table: "Inventario");
        }
    }
}
