using Catalog.Core.Entities;
using Catalog.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CatalogApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        #region Private Fields

        private readonly ILogger<ImageController> _logger;
        private readonly ImageService _imageService;

        #endregion

        public ImageController(ILogger<ImageController> logger, ImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }

        /// <summary>
        /// Get Images By Entity Type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("entityType/{entityType}")]
        public IActionResult GetImagesByEntityType(string entityType)
        {
            try
            {
                _logger.LogInformation($"Going to fetch images from database.");
                var images = _imageService.GetByEntityType(entityType);
                return Ok(images);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get image by Id
        /// </summary>
        /// <param name="id">Image Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                _logger.LogInformation($"Going to fetch image from database for id {id}");
                var image = _imageService.GetById(id);
                return Ok(image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Create a new image
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] Image image)
        {
            try
            {
                return Ok(_imageService.Create(image));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _imageService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
