using Catalog.Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Core.Services
{
    public class ProductService
    {
        #region Private Fields

        private readonly IEntityRepository<Product> _productRepository;

        #endregion

        #region  Public methods

        public ProductService(IEntityRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get  all products
        /// </summary>
        /// <returns>List  of products</returns>
        public List<Product> GetProducts()
        {
            var products = _productRepository.GetAll();
            return products.ToList();
        }

        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Product</returns>
        public Product CreateProduct(Product product)
        {
            _productRepository.Insert(product);
            _productRepository.SaveChanges();
            return product;
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product</returns>
        public Product GetProduct(int id)
        {
            return _productRepository.Find(f=>f.Id == id, new List<string> { "Properties", "Type.Properties" }).FirstOrDefault();
        }

        /// <summary>
        /// Get product by producttype
        /// </summary>
        /// <param name="type">producttype</param>
        /// <returns>products</returns>
        /// 

     
        public List<Product> GetAllProductsByProductType(string type)
        {
            return _productRepository.Find(x => x.Type.ToString() == type && x.IsDeleted == false).ToList();
        }

        public List<Product> GetAllProductsByProductType(List<ProductType> type)
        {
            return _productRepository.Find(x => type.Contains(x.Type) && x.IsDeleted == false).ToList();
        }

        /// <summary>
        /// Get products by skuname
        /// </summary>
        /// <param name="sku">sku</param>
        /// <returns>Sku's product</returns>
        public Product GetProductBySku(string sku)
        {
            return _productRepository.Find(x => x.Sku == sku && x.IsDeleted == false).SingleOrDefault();
        }

        public Product GetProductByProductTypeId(int producttypeId)
        {
            return _productRepository.Find(x => x.Type.Id == producttypeId, new List<string> { "Type" }).SingleOrDefault();
        }

        #endregion
    }
}