using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Catalog.Core.Entities;
using Catalog.Core.Services;
using System;

namespace ShoppingPlace.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Private Fields

        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productService;
        private readonly PropertyTypeService _propertyTypeService;
        private readonly PropertyService _propertyService;

        #endregion

        public ProductController(ILogger<ProductController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Get All products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("products")]
        public IEnumerable<Product> GetAll()
        {
            try
            {
                _logger.LogInformation($"Going to fetch products from database.");
                var products = _productService.GetProducts();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get Product by Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Product Get(int id)
        {
            try
            {
                _logger.LogInformation($"Going to fetch product from database for id {id}");
                var product = _productService.GetProduct(id);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody]Product product)
        {
            try
            {
                return Ok(_productService.CreateProduct(product));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get products by type
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>products by type</returns>
        [HttpGet]
        [Route("{type}")]
        public IActionResult GetProductsByType(string productType)
        {
            try
            {
                if (string.IsNullOrEmpty(productType))
                    throw new Exception("Type can't be empty");
                return Ok(_productService.GetAllProductsByProductType(productType));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// get product by sku
        /// </summary>
        /// <param name="productSku">sku</param>
        /// <returns>product bt sku</returns>
        [HttpGet]
        [Route("{product-sku}")]
        public IActionResult GetProductBySku(string productSku)
        {
            try
            {
                var product = _productService.GetProductBySku(productSku);
                if (product == null)
                    return NoContent();
                return Ok(product);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Get all propertytypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("property-types")]
        public IActionResult GetAllPropertyType()
        {
            try
            {
                var propertyTypes = _propertyTypeService.GetAllPropertyType();
                if (propertyTypes == null)
                    return NoContent();
                return Ok(propertyTypes);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("create/property-type")]
        public IActionResult CreatePropertyType(PropertyType propertyType)
        {
            try
            {
                var createPropertyTypes = _propertyTypeService.CreatePropertyType(propertyType);
                if (createPropertyTypes == null)
                    return NoContent();
                return Ok(createPropertyTypes);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Get property by property type
        /// </summary>
        /// <param name="propertyType">PropertyType</param>
        /// <returns></returns>
        [HttpGet]
        [Route("property-type")]
        public IActionResult GetPropertyByPropertyType(PropertyType propertyType)
        {
            try
            {
                var properyType = _propertyService.GetPropertyByPropertyType(propertyType);
                if (properyType == null)
                    return NoContent();
                return Ok(properyType);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}