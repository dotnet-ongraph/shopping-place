using Microsoft.AspNetCore.Mvc;
using Catalog.Core.Services;
using System;
using Catalog.Core.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Filters;

namespace CatalogApi.Controllers
{
    [Route("category")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        #region Private Fields

        private readonly CategoryService _categoryService;

        #endregion

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region Public Methods

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("categories")]
        public List<Category> GetAll()
        {
            try 
            {
                var categories =_categoryService.GetAllCategories();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns></returns>
        [HttpPost("create")]
        [ServiceFilter(typeof(GlobalModelValidator))]
        public IActionResult Create([FromBody] Category category)
        {
            try
            {
                return Ok(_categoryService.CreateCategory(category));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [HttpPost("createMultipleCategories")]
        [ServiceFilter(typeof(GlobalModelValidator))]
        public IActionResult CreateMultipleCategories([FromBody] List<Category> categories)
        {
            try
            {
                return Ok(_categoryService.CreateMultipleCategories(categories));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id">CategoriesId</param>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(ValidateEntityExistAttribute<Category>))]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var category = HttpContext.Items["entity"] as Category;
            return Ok(category);
        }


        //[HttpGet]
        //[ServiceFilter(typeof(ValidateEntityExistAttribute<Category>))]
        //[Route("{id:int}")]
        //public IActionResult GetCategoryById(int id)
        //{
        //    var category = _categoryService.GetCategory(id);
        //    return Ok(category);
        //}

        [HttpPut("{id}")]
        [ServiceFilter(typeof(GlobalModelValidator), Order = 1)]
        [ServiceFilter(typeof(ValidateEntityExistAttribute<Category>), Order = 2)]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            var existingCategory = HttpContext.Items["entity"] as Category;
            _categoryService.UpdateEntity(category, existingCategory);
            return Ok(existingCategory);
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateEntityExistAttribute<Category>))]
        [Route("getCategoryProducts/{id:int}")]
        public IActionResult GetCategoryProducts(int id)
        {

            try
            {
                var data = _categoryService.GetCategoryProducts(id);
                if (data == null)
                    return NoContent();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
    }


    /// <summary>
    /// Get category
    /// </summary>
    /// <param name="categoryName">categoryname</param>
    /// <returns><category/returns>
    [HttpGet]
        [Route("{categoryName}")]
        public IActionResult GetCategoryByName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException("CategoryName cannot be empty or null");
            }
            try
            {
                var data = _categoryService.GetCategoryByName(categoryName);
                if (data == null)
                    return NoContent();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}