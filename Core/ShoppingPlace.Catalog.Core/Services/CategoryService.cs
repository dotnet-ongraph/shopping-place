using System.Collections.Generic;
using Catalog.Core.Entities;
using Catalog.Core.Services;
using System;
using Core.Interfaces;
using System.Linq;

namespace Catalog.Core.Services
{

    public class CategoryService
    {
        private readonly IEntityRepository<Category> _categoryRepository;

        public CategoryService(IEntityRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();
            return categories.ToList();
        }

        /// <summary>
        /// get category
        /// </summary>
        /// <param name="id">databaseid</param>
        /// <returns>category</returns>
        public Category GetCategory(int id)
        {
            return _categoryRepository.Get(id);
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _categoryRepository.Find(x => x.Name == categoryName && x.IsDeleted==false).SingleOrDefault();
        }
    }
}