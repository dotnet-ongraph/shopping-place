using Catalog.Core.Entities;
using System;

namespace UnitTest.TestData
{
    class ProductServiceTestData
    {
        public static Category category = new Category
        {
            Name = "Furniture",
            Description = "Wood made products",
            IsActive = true,
            CreatedDate = DateTime.Now,
            LastModified = DateTime.Now
        };

        public static Product product = new Product
        {
            Name = "Table",
            Description = "Study table",
            IsActive = true,
            Price = 100,
            Quantity = 12,
            SellingPrice = 100,
            Sku = Guid.NewGuid().ToString(),
            Type = productType,
            CreatedDate = DateTime.Now,
            LastModified = DateTime.Now
        };

        public static ProductType productType = new ProductType
        {
            Name = "Wooden-Table",
            Prefix = "Table",
            IsActive = true,
            Category = category,
            CreatedDate = DateTime.Now,
            LastModified = DateTime.Now
        };

        public static Image productTypeImage = new Image
        {
            PublicThumblUrl = "https://5.imimg.com/data5/RF/NE/MY-13929894/wooden-table-500x500.jpg",
            PublicUrl = "https://5.imimg.com/data5/RF/NE/MY-13929894/wooden-table-500x500.jpg",
            FileName = "Wooden-study-tabe",
            //EntityType = EntityType.ProductType,
            EntityId = productType.Id,
            IsDefault = true,
            CreatedDate = DateTime.Now,
            LastModified = DateTime.Now,
        };
        
        public static Image productImage =  new Image
        {
            PublicThumblUrl = "https://5.imimg.com/data5/RF/NE/MY-13929894/wooden-table-500x500.jpg",
            PublicUrl = "https://5.imimg.com/data5/RF/NE/MY-13929894/wooden-table-500x500.jpg",
            FileName = "Wooden-study-tabe",
            //EntityType = EntityType.Product,
            EntityId = product.Id,
            IsDefault = true,
            CreatedDate = DateTime.Now,
            LastModified = DateTime.Now
        };
    }
}
