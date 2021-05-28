using Core.Interfaces;
using Fulfillment.Core.Entities;
using Fulfillment.Core.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fulfillment.Infrastructure
{
    public class FulfillmentDependencyRegistrar
    {
        public void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FulfillmentDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:FulfillmentDb"]));
            services.AddScoped<FulfillmentDbContext>();
            services.AddScoped(typeof(IEntityRepository<Order>), typeof(EntityRepository<Order, FulfillmentDbContext>));
            services.AddScoped<CustomerService>();
            services.AddScoped(typeof(IEntityRepository<OrderItem>), typeof(EntityRepository<OrderItem, FulfillmentDbContext>));
            services.AddScoped(typeof(IEntityRepository<Customer>), typeof(EntityRepository<Customer, FulfillmentDbContext>));
            services.AddScoped(typeof(IEntityRepository<CustomerAddress>), typeof(EntityRepository<CustomerAddress, FulfillmentDbContext>));
            services.AddScoped<OrderService>();
            services.AddScoped<IWriteEntityRepository, DapperRepository>();
            services.AddScoped<IReadEntityRepository, DapperRepository>();
        }
    }
}
