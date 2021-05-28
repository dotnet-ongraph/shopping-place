using Catalog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Controllers
{
    public class SearchController : ControllerBase
    {
        private readonly CategoryService _categoryService;


        public SearchController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("{search}")]
        public IActionResult SearchProduct(string search)
        {
            try
            {
                var data = _categoryService.ProductSearch(search);
                if (data == null)
                    return NoContent();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }
}
