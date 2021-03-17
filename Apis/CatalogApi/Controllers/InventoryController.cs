using Microsoft.AspNetCore.Mvc;
using Catalog.Core.Services;
using System;

namespace CatalogApi.Controllers
{
    [Route("inventory")]
    [ApiController]
    public class InventoryController:ControllerBase
    {
        #region Private Fields

        private readonly InventoryService _inventoryService;

        #endregion

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        #region Public Methods

        /// <summary>
        /// Get Product Stock
        /// </summary>
        /// <param name="productId">ProductId</param>
        /// <returns></returns>
        [HttpGet("product-stock")]
        public IActionResult GetProductStock(int productId)
        {
            try
            {
                return Ok( _inventoryService.GetProductStock(productId));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
