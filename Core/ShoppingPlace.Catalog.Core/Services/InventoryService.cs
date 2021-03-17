using Catalog.Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Core.Services
{
    public class InventoryService
    {
        private readonly IEntityRepository<Inventory> _inventoryRepository;

        public InventoryService(IEntityRepository<Inventory> inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public decimal GetProductStock(int productId)
        {
            return _inventoryRepository.Find(x => x.Id == productId).SingleOrDefault().SaleableQuantity;
        }
    }
}
