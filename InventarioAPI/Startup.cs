using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InventarioAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get;  }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
                        
            services.AddAutoMapper(options => 
            {
                options.CreateMap<CategoriaCreacionDTO,Categoria>();
                options.CreateMap<TipoEmpaqueCreacionDTO, TipoEmpaque>();
                options.CreateMap<InventarioCreacionDTO, Inventario>();
                options.CreateMap<ProductoCreacionDTO, Producto>();
                options.CreateMap<DetalleCompraCreacionDTO, DetalleCompra>();
                options.CreateMap<DetalleFacturaCreacionDTO, DetalleFactura>();
                options.CreateMap<EmailProveedorCreacionDTO, EmailProveedor>();
                options.CreateMap<CompraCreacionDTO, Compra>();
                options.CreateMap<FacturaDTO, Factura>();
                options.CreateMap<ProveedorDTO, Proveedor>();
                options.CreateMap<ClienteDTO, Cliente>();
                options.CreateMap<TelefonoProveedorDTO, TelefonoProveedor>();
                options.CreateMap<EmailClienteDTO, EmailCliente>();
                options.CreateMap<TelefonoClienteDTO, TelefonoCliente>();

            });
            //services.AddAutoMapper(options => {options.CreateMap<TipoEmpaqueCreacionDTO, TipoEmpaque>();});
            //services.AddAutoMapper(options => {options.CreateMap<InventarioCreacionDTO, Inventario>();});

            services.AddDbContext<InventarioDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
