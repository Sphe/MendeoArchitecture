using Common.Data.Infrastructure;
using Mendeo.Mercurius.Data.FullDomain;
using Mendeo.Mercurius.Data.FullDomain.Infrastructure;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Model.FullDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute=Mendeo.Mercurius.Model.FullDomain.Attribute;
using System.Data.Entity;

namespace Mendeo.Mercurius.Data.Repository.FullDomain
{
    public class ProductRepository : MercuritusFullDomainRepositoryBase<Product>, IProductRepository
    {
        private readonly IMercuritusFullDomainRepository<ProductCultureMap> _productCultureRepository;
        private readonly IMercuritusFullDomainRepository<ProductAttributeMap> _productAttributeRepository;
        private readonly IMercuritusFullDomainRepository<Attribute> _attributeRepository;
        private readonly IMercuritusFullDomainRepository<AttributeItem> _attributeItemRepository;
        private readonly IMercuritusFullDomainRepository<AttributeType> _attributeTypeRepository;
        private readonly IMercuritusFullDomainRepository<ProductImageMap> _productImageRepository;
        private readonly IMercuritusFullDomainRepository<ProductImageType> _productImageTypeRepository;
        private readonly IMercuritusFullDomainRepository<Image> _imageRepository;

        public ProductRepository(IDatabaseFactory<MercuriusEntities> databaseFactory,
            IMercuritusFullDomainRepository<ProductCultureMap> productCultureRepository,
            IMercuritusFullDomainRepository<Attribute> attributeRepository,
            IMercuritusFullDomainRepository<AttributeItem> attributeItemRepository,
            IMercuritusFullDomainRepository<AttributeType> attributeTypeRepository,
            IMercuritusFullDomainRepository<ProductAttributeMap> productAttributeRepository,
            IMercuritusFullDomainRepository<ProductImageMap> productImageRepository,
            IMercuritusFullDomainRepository<ProductImageType> productImageTypeRepository,
            IMercuritusFullDomainRepository<Image> imageRepository)
            : base(databaseFactory)
        {
            _productCultureRepository = productCultureRepository;
            _attributeRepository = attributeRepository;
            _attributeItemRepository = attributeItemRepository;
            _attributeTypeRepository = attributeTypeRepository;
            _productAttributeRepository = productAttributeRepository;
            _productImageRepository = productImageRepository;
            _productImageTypeRepository = productImageTypeRepository;
            _imageRepository = imageRepository;
        }

        public IndexedProduct GetProductToBeIndexed(int productId, int currentCultureId)
        {
            var fullProducts = (from prod in DbSet
                                join prodCulture in _productCultureRepository.DbSet on prod.ProductID equals prodCulture.ProductID
                                where prodCulture.CultureID == currentCultureId
                                        && prod.ProductID == productId
                                        && prod.Activate

                                let attributes = from attr in _attributeRepository.DbSet
                                                 join attrType in _attributeTypeRepository.DbSet on attr.AttributeTypeID equals attrType.AttributeTypeID
                                                 join attrItem in _attributeItemRepository.DbSet on attr.AttributeItemID equals attrItem.AttributeItemID
                                                 join prodAttr in _productAttributeRepository.DbSet on attr.AttributeID equals prodAttr.AttributeID
                                                 where prodAttr.ProductID == prod.ProductID
                                                 select new IndexedAttribute
                                                 {
                                                     Id = attr.AttributeID,
                                                     Type = attrType.Name,
                                                     Item = attrItem.Name,
                                                     ItemBool = attrItem.NameBool,
                                                     ItemDate = attrItem.NameDate,
                                                     ItemNumber = attrItem.NameNumber,
                                                     ItemPrimitiveType = attrItem.ItemPrimitiveType.HasValue ? (ItemPrimitiveTypeEnum)attrItem.ItemPrimitiveType.Value : ItemPrimitiveTypeEnum.String,
                                                     UnitLabel = attrItem.ItemUnitLabel
                                                 }

                                let images = from prodImg in _productImageRepository.DbSet
                                                join prodImgType in _productImageTypeRepository.DbSet on prodImg.ProductImageTypeID equals prodImgType.ProductImageTypeID
                                                join img in _imageRepository.DbSet on prodImg.ImageID equals img.ImageID
                                             where prodImg.ProductID == prod.ProductID
                                                select new IndexedImage
                                                 {
                                                     ImageType = prodImgType.Type,
                                                     ImageUrl = img.ThumbUrl
                                                 }

                                select new IndexedProduct
                                {
                                    Id = prod.ProductID,
                                    Coordinate = new IndexedCoordinate()
                                    {
                                        Lat = (double)prod.GoogleAddressLatitude,
                                        Lon = (double)prod.GoogleAddressLongitude
                                    },
                                    Description = prodCulture.Description,
                                    ShortDescription = prodCulture.ShortDescription,
                                    ProductName = prodCulture.ProductName,
                                    ProductCode = prod.ProductCode,
                                    Price = prodCulture.BaseUnitPrice,
                                    Attributes = attributes.ToList(),
                                    CreatedOn = prod.CreatedOn,
                                    Images = images.ToList(),
                                    CategoryIds = prod.CategoryProductMap.Select(x => x.CategoryID).ToList(),
                                    UserId = prod.UserID,
                                    LastStatus = prod.ProductLastStatusID.HasValue ? (ProductStatusTypeEnum) prod.ProductLastStatusID.Value : ProductStatusTypeEnum.NOT_DEFINED,
                                    ProductAnnounceTypeID = prod.ProductAnnounceTypeID
                                })
                                .AsNoTracking();

            return fullProducts.First();
        }

        public IQueryable<IndexedProduct> GetProductToBeIndexed(int currentCultureId)
        {
            var fullProducts = (from prod in DbSet
                                join prodCulture in _productCultureRepository.DbSet on prod.ProductID equals prodCulture.ProductID
                                where prodCulture.CultureID == currentCultureId
                                        && prod.Activate

                                let attributes = from attr in _attributeRepository.DbSet
                                                 join attrType in _attributeTypeRepository.DbSet on attr.AttributeTypeID equals attrType.AttributeTypeID
                                                 join attrItem in _attributeItemRepository.DbSet on attr.AttributeItemID equals attrItem.AttributeItemID
                                                 join prodAttr in _productAttributeRepository.DbSet on attr.AttributeID equals prodAttr.AttributeID
                                                 where prodAttr.ProductID == prod.ProductID
                                                 select new IndexedAttribute
                                                 {
                                                     Id = attr.AttributeID,
                                                     Type = attrType.Name,
                                                     Item = attrItem.Name,
                                                     ItemBool = attrItem.NameBool,
                                                     ItemDate = attrItem.NameDate,
                                                     ItemNumber = attrItem.NameNumber,
                                                     ItemPrimitiveType = attrItem.ItemPrimitiveType.HasValue ? (ItemPrimitiveTypeEnum)attrItem.ItemPrimitiveType.Value : ItemPrimitiveTypeEnum.String,
                                                     UnitLabel = attrItem.ItemUnitLabel
                                                 }

                                let images = from prodImg in _productImageRepository.DbSet
                                             join prodImgType in _productImageTypeRepository.DbSet on prodImg.ProductImageTypeID equals prodImgType.ProductImageTypeID
                                             join img in _imageRepository.DbSet on prodImg.ImageID equals img.ImageID
                                             where prodImg.ProductID == prod.ProductID
                                             select new IndexedImage
                                             {
                                                 ImageType = prodImgType.Type,
                                                 ImageUrl = img.ThumbUrl
                                             }

                                select new IndexedProduct
                                {
                                    Id = prod.ProductID,
                                    Coordinate = new IndexedCoordinate()
                                    {
                                        Lat = (double)prod.GoogleAddressLatitude,
                                        Lon = (double)prod.GoogleAddressLongitude
                                    },
                                    Description = prodCulture.Description,
                                    ShortDescription = prodCulture.ShortDescription,
                                    ProductName = prodCulture.ProductName,
                                    ProductCode = prod.ProductCode,
                                    Price = prodCulture.BaseUnitPrice,
                                    Attributes = attributes.ToList(),
                                    CreatedOn = prod.CreatedOn,
                                    CategoryIds = prod.CategoryProductMap.Select(x => x.CategoryID).ToList(),
                                    Images = images.ToList(),
                                    UserId = prod.UserID,
                                    LastStatus = prod.ProductLastStatusID.HasValue ? (ProductStatusTypeEnum)prod.ProductLastStatusID.Value : ProductStatusTypeEnum.NOT_DEFINED,
                                    ProductAnnounceTypeID = prod.ProductAnnounceTypeID
                                })
                                .AsNoTracking();

            return fullProducts;
        }
    }
}
