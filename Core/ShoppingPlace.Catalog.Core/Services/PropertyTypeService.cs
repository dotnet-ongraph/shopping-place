using Catalog.Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Core.Services
{
   public class PropertyTypeService
    {
        #region Private Fields

        private readonly IEntityRepository<PropertyType> _propertyTypeRepository;

        #endregion

        public PropertyTypeService(IEntityRepository<PropertyType> propertyTypeRepository)
        {
            _propertyTypeRepository = propertyTypeRepository;
        }

        #region  Public methods

        /// <summary>
        /// Get all propertytypes
        /// </summary>
        /// <returns> List of propertytypes </returns>
        public List<PropertyType> GetAllPropertyType()
        {
            var propertyTypes = _propertyTypeRepository.GetAll();
            return propertyTypes.ToList();

        }

        /// <summary>
        /// Create propertytype
        /// </summary>
        /// <param name="propertyType">PropertyType</param>
        /// <returns>PropertyType</returns>
        public PropertyType CreatePropertyType(PropertyType propertyType)
        {
            _propertyTypeRepository.Insert(propertyType);
            _propertyTypeRepository.SaveChanges();
            return propertyType;
        }

        #endregion

    }
}
