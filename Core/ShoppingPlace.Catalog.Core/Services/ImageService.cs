using Catalog.Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Core.Services
{
    public class ImageService
    {
        private readonly IEntityRepository<Image> _imageRepository;

        public ImageService(IEntityRepository<Image> imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public List<Image> GetImages(string entityType)
        {
            if (string.IsNullOrEmpty(entityType))
                throw new Exception("Entity Type can't be null");
            var images = _imageRepository.GetAll().Where(x => x.EntityType.Value.ToString() == entityType).ToList();
            return images;
        }
    }
}