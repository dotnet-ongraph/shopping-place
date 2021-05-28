using Catalog.Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Core.Services
{
  public  class PropertyService
    {
        #region Private Fields

        private readonly IEntityRepository<Property> _propertyRepository;

        #endregion

        public PropertyService(IEntityRepository<Property> propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        #region  Public methods

        /// <summary>
        /// Get property by property type
        /// </summary>
        /// <param name="propertyType">PropertyType</param>
        /// <returns>Property</returns>
        public Property GetPropertyByPropertyType(PropertyType propertyType)
        {
            return _propertyRepository.Find(x => x.Type.Name == propertyType.Name && x.IsDeleted == false).SingleOrDefault();
        }

        public List<Property> CreateProperties(List<Property> properties)
        {
            properties.ForEach(p => _propertyRepository.Insert(p));
            _propertyRepository.SaveChanges();
            return properties;
        }

        #endregion
    }
}
