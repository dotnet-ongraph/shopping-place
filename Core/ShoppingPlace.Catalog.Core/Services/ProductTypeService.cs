using System;
using Catalog.Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Catalog.Core.Services
{
    public class ProductTypeService
    {
        private readonly IEntityRepository<ProductType> _productTypeRepository;


        public ProductTypeService(IEntityRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public List<ProductType> GetProductTypes()
        {
            var productTypes = _productTypeRepository.GetAll();
            return productTypes.ToList();
        }

        public ProductType CreateProductType(ProductType productType)
        {
            _productTypeRepository.Insert(productType);
            _productTypeRepository.SaveChanges();
            return productType;
        }

        public ProductType GetProductType(int id)
        {
            return _productTypeRepository.Get(id);
        }

        public List<ProductType> GetProductTypeByCategory(int categoryId)
        {
            return _productTypeRepository.Find(x => x.Category.Id == categoryId).ToList();
        }

        public ProductType GetProductTypeById(int producttypeId)
        {
            return _productTypeRepository.Find(x => x.Id == producttypeId).SingleOrDefault();
        }
    }
}
