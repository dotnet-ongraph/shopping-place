using Microsoft.AspNetCore.Mvc;
using Catalog.Core.Services;
using System;


namespace CatalogApi.Controllers
{
    [Route("category")]
    [ApiController]
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
        public IActionResult GetAll()
        {
            try
            {
                return  Ok(_categoryService.GetAllCategories());
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
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_categoryService.GetCategory(id));
            }
            catch(Exception ex)
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
