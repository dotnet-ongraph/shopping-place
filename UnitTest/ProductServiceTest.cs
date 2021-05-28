using Catalog.Core.Entities;
using Catalog.Core.Services;
using Catalog.Infrastructure;
using Core.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTest.TestData;

namespace UnitTest
{
    public class ProductServiceTest
    {
        private ProductService _productService;
        private CategoryService _categoryService;
        private ProductTypeService _productTypeService;
        private ImageService _imageService;
        private IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
        private PropertyTypeService _propertyTypeService;
        private PropertyService _propertyService;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(_configuration["ConnectionStrings:CatalogDb"]));
            services.AddScoped<ProductService>();
            services.AddScoped(typeof(IEntityRepository<Category>), typeof(EntityRepository<Category, CatalogDbContext>));
            services.AddScoped<CatalogDbContext>();
            services.AddScoped(typeof(IEntityRepository<Product>), typeof(EntityRepository<Product, CatalogDbContext>));
            services.AddScoped<CategoryService>();
            services.AddScoped(typeof(IEntityRepository<ProductType>), typeof(EntityRepository<ProductType, CatalogDbContext>));
            services.AddScoped<ProductTypeService>();
            services.AddScoped(typeof(IEntityRepository<Image>), typeof(EntityRepository<Image, CatalogDbContext>));
            services.AddScoped<ImageService>();
            services.AddScoped(typeof(IEntityRepository<PropertyType>), typeof(EntityRepository<PropertyType, CatalogDbContext>));
            services.AddScoped<PropertyTypeService>();
            services.AddScoped(typeof(IEntityRepository<Property>), typeof(EntityRepository<Property, CatalogDbContext>));
            services.AddScoped<PropertyService>();

            var serviceProvider = services.BuildServiceProvider();
            _productService = serviceProvider.GetService<ProductService>();
            _categoryService = serviceProvider.GetService<CategoryService>();
            _productTypeService = serviceProvider.GetService<ProductTypeService>();
            _imageService = serviceProvider.GetService<ImageService>();
            _propertyTypeService = serviceProvider.GetService<PropertyTypeService>();
            _propertyService = serviceProvider.GetService<PropertyService>();
        }

        [Test]
        public void TestACompleteCycleOfCatalog()
        {
            // create caterogy
            var createdCategory = _categoryService.CreateCategory(new Category { Name = "Women Clothing", Description = "All women clothe category.", IsActive = true });
            // create producttype
            var createdProductType = _productTypeService.CreateProductType(new ProductType { Name = "Women Top", IsActive = true, CategoryId = createdCategory.Id, Prefix = "TOP" });
            // create product
            var createdProduct = _productService.CreateProduct(new Product { Name = "Cotton Women Top", IsActive = true, Description = "General women top", Price = 499, Quantity = 5, SellingPrice = 299, Sku = "TOP-001", ProductTypeId = createdProductType.Id });
            // create property type
            var createdPropertyTypeColor = _propertyTypeService.CreatePropertyType(new PropertyType { IsActive = true, Name = "Color", Prefix = "CLR" });
            var createdPropertyTypeFabric = _propertyTypeService.CreatePropertyType(new PropertyType { IsActive = true, Name = "Fabric", Prefix = "FBC" });
            // create properties on product type, product
            var createdProperties = _propertyService.CreateProperties(new System.Collections.Generic.List<Property> { new Property { PropertyTypeId = createdPropertyTypeColor.Id, ProductId = createdProduct.Id, Value = "Red" }, new Property { PropertyTypeId = createdPropertyTypeFabric.Id, ProductTypeId = createdProductType.Id, Value = "Cotton" } });
            // get properties
            var existingProduct = _productService.GetProduct(createdProduct.Id);
            var properties = existingProduct.GetProperties();
            Assert.AreEqual(createdProperties.Count, properties.Count);
            // save images

            // get images

        }

        [Test]
        public void CreateProductTest()
        {
            //create category
            ProductServiceTestData.category = _categoryService.CreateCategory(ProductServiceTestData.category);
            //create productType
            ProductServiceTestData.productType = _productTypeService.CreateProductType(ProductServiceTestData.productType);
            //create image for product type
            ProductServiceTestData.productTypeImage.EntityId = ProductServiceTestData.productType.Id;
            var productTypeImage = _imageService.Create(ProductServiceTestData.productTypeImage);
            //create product
            var product = _productService.CreateProduct(ProductServiceTestData.product);
            //create image for product 
            ProductServiceTestData.productImage.EntityId = product.Id;
            var productImage = _imageService.Create(ProductServiceTestData.productImage);

            Assert.AreEqual(ProductServiceTestData.product.Name, product.Name);
            Assert.AreEqual(ProductServiceTestData.product.Price, product.Price);
            Assert.AreEqual(ProductServiceTestData.product.Quantity, product.Quantity);
        }


    }
}