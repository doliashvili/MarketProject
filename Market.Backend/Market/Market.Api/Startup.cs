using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Repository;
using EventStore.MSSQL;
using Market.Application.CommandHandlers;
using Market.Domain.Products.Commands;
using Market.Domain.Products.Entities;
using Market.Domain.Products.Events;
using Market.Infrastructure;
using Market.Infrastructure.Data;
using Market.ReadModels.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ReadModelRepository;
using ReadModelRepository.MSSQL;

namespace Market.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get;  }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MarketDb>(options => options.UseSqlServer(Configuration["DefaultConnection"]));
            //services.AddScoped<IReadModelRepository<ProductReadModel, Guid>>(x =>
            //   new RepositoryMSSQL<ProductReadModel, Guid>(x.GetService<MarketDb>()));

            services.AddMSSQLReadModelRepository<MarketDb>();
            services.AddCQRS(typeof(CreateProductCommand).Assembly,typeof(ProductReadModel).Assembly,typeof(ProductCommandHandler).Assembly);
            services.AddInternalEventingSystem(typeof(CreateProductCommand).Assembly, typeof(ProductReadModel).Assembly, typeof(ProductCommandHandler).Assembly,typeof(CreatedProductEvent).Assembly);
            services.AddEntityDbEventStore("Server=DESKTOP-R41JSOT;Initial Catalog=EventStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            services.AddTransient<IAggregateRepository<Product, Guid>, AggregateRepository<Product, Guid>>();

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen();

            services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Mango Api");
                o.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
