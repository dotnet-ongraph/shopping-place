using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Core.Services;
using Core.Interfaces;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Infrastructure.Filters;
using Catalog.Core.Entities;

namespace Catalog.Infrastructure
{
    public class CatalogDependenciesRegistrar
    {
        public void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:CatalogDb"]));
            services.AddScoped<CatalogDbContext>();
            services.AddScoped(typeof(IEntityRepository<Product>), typeof(EntityRepository<Product, CatalogDbContext>));
            services.AddScoped<ProductService>();
            services.AddScoped(typeof(IEntityRepository<Category>), typeof(EntityRepository<Category, CatalogDbContext>));
            services.AddScoped<CategoryService>();
            services.AddScoped(typeof(IEntityRepository<ProductType>), typeof(EntityRepository<ProductType, CatalogDbContext>));
            services.AddScoped<ProductTypeService>();
            services.AddScoped(typeof(IEntityRepository<Inventory>), typeof(EntityRepository<Inventory, CatalogDbContext>));
            services.AddScoped<InventoryService>();
            services.AddScoped(typeof(IEntityRepository<Image>), typeof(EntityRepository<Image, CatalogDbContext>));
            services.AddScoped<ImageService>();
            services.AddScoped(typeof(IEntityRepository<PropertyType>), typeof(EntityRepository<PropertyType, CatalogDbContext>));
            services.AddScoped<PropertyTypeService>();
            services.AddScoped(typeof(IEntityRepository<Property>), typeof(EntityRepository<Property, CatalogDbContext>));
            services.AddScoped<PropertyService>();

            services.AddScoped<ValidateEntityExistAttribute<Category>>();

            services.AddScoped<IWriteEntityRepository, DapperRepository>();
            services.AddScoped<IReadEntityRepository, DapperRepository>();
        }
    }
}