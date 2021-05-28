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

        public List<Image> GetByEntityType(string entityType)
        {
            if (string.IsNullOrEmpty(entityType))
                throw new Exception("Entity Type can't be null");
            var images = _imageRepository.GetAll().Where(x => x.Type.Value.ToString() == entityType).ToList();
            return images;
        }
        
        public Image GetById(int id)
        {
            if (id == null)
                throw new Exception("Image id can't be null");
            var image = _imageRepository.Find(x => x.Id == id).SingleOrDefault();
            return image;
        }

        public Image Create(Image image)
        {
            image.CreatedDate = DateTime.Now;
            image.LastModified = DateTime.Now;
            _imageRepository.Insert(image);
            _imageRepository.SaveChanges();
            return image;
        }

        public Image Update(Image image)
        {
            if (image.Id == null)
                throw new Exception("Image id can't be null");
            var imageEntity = _imageRepository.Find(x => x.Id == image.Id).SingleOrDefault();
            if (imageEntity != null)
            {
                imageEntity.PublicUrl = image.PublicUrl;
                image.PublicThumblUrl = image.PublicThumblUrl;
                image.FileName = image.FileName;
                image.Type = image.Type;
                image.EntityId = image.EntityId;
                image.IsDefault = image.IsDefault;
                image.LastModified = DateTime.Now;
                _imageRepository.Update(image);
                _imageRepository.SaveChanges();
            }
            return image;
        }

        public void Delete(int id)
        {
            if (id == null)
                throw new Exception("Image id can't be null");
            var image = _imageRepository.Find(x => x.Id == id).SingleOrDefault();
            image.IsDeleted = true;
            _imageRepository.Update(image);
            _imageRepository.SaveChanges();
        }

        public List<Image> GetImagesByEntityIds(List<int> productTypesIds ,EntityType entityType)
        {
           return _imageRepository.GetAll().Where(x => productTypesIds.Contains(x.EntityId.Value) && x.Type==entityType).ToList();
        }

        public List<Image> GetImagesByEntityId(int producttypeId, EntityType productType)
        {
            return _imageRepository.Find(x => x.EntityId == producttypeId && x.Type == productType).ToList();
        }
    }
}