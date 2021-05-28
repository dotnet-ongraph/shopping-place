using System.Collections.Generic;
using Catalog.Core.Entities;
using Catalog.Core.Services;
using System;
using Core.Interfaces;
using System.Linq;
using Catalog.Core.Models;
using Dapper;

namespace Catalog.Core.Services
{

    public class CategoryService
    {
        private readonly IEntityRepository<Category> _categoryRepository;
        private IWriteEntityRepository _writeEntityRepository;

        public CategoryService(IEntityRepository<Category> categoryRepository, IWriteEntityRepository writeEntityRepository)
        {
            _categoryRepository = categoryRepository;
            _writeEntityRepository = writeEntityRepository;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll().Where(x=>x.IsDeleted==false && x.IsActive == true);
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

        public Category CreateCategory(Category category)
        {
            _categoryRepository.Insert(category);
            _categoryRepository.SaveChanges();
            return category;
        }

        public void UpdateEntity(Category category, Category existingCategory)
        {
            existingCategory.IsDeleted = category.IsDeleted;
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.IsActive = category.IsActive;
            _categoryRepository.Update(existingCategory);
            _categoryRepository.SaveChanges();
        }

        public dynamic ProductSearch(string search)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@search", search);
            var result = _writeEntityRepository.ExecuteProcedureWithResult<SearchDataModel>("GetBySearch", parameters).ToList();
            return result;
        }
    }
}