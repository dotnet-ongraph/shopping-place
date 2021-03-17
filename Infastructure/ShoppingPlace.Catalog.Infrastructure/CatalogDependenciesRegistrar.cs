using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Core.Services;
using Core.Interfaces;
using Infrastructure;

namespace Catalog.Infrastructure
{
    public class CatalogDependenciesRegistrar
    {
        public void RegisterDependencies(IServiceCollection services)
        {
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer("Data Source=101.53.156.73,1434;Initial Catalog=ShoppingPlace;User Id=dbuser;Password=Ongraph@123;Integrated Security=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Trusted_connection=false;")); services.AddScoped<CatalogDbContext>();
            services.AddScoped(typeof(IEntityRepository<Catalog.Core.Entities.Product>), typeof(EntityRepository<Catalog.Core.Entities.Product, CatalogDbContext>));
            services.AddScoped<ProductService>();
            services.AddScoped(typeof(IEntityRepository<Catalog.Core.Entities.Category>), typeof(EntityRepository<Catalog.Core.Entities.Category, CatalogDbContext>));
            services.AddScoped<CategoryService>();
            services.AddScoped(typeof(IEntityRepository<Catalog.Core.Entities.ProductType>), typeof(EntityRepository<Catalog.Core.Entities.ProductType, CatalogDbContext>));
            services.AddScoped<ProductTypeService>();
            services.AddScoped(typeof(IEntityRepository<Catalog.Core.Entities.Inventory>), typeof(EntityRepository<Catalog.Core.Entities.Inventory, CatalogDbContext>));
            services.AddScoped<InventoryService>();
            services.AddScoped(typeof(IEntityRepository<Catalog.Core.Entities.Image>), typeof(EntityRepository<Catalog.Core.Entities.Image, CatalogDbContext>));
            services.AddScoped<ImageService>();
            services.AddScoped(typeof(IEntityRepository<Catalog.Core.Entities.PropertyType>), typeof(EntityRepository<Catalog.Core.Entities.PropertyType, CatalogDbContext>));
            services.AddScoped<PropertyTypeService>();
            services.AddScoped(typeof(IEntityRepository<Catalog.Core.Entities.Property>), typeof(EntityRepository<Catalog.Core.Entities.Property, CatalogDbContext>));
            services.AddScoped<PropertyService>();
        }
    }
}
