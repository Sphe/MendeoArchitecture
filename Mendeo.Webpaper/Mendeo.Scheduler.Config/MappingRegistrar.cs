using AutoMapper;
using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Dto.ProductDetail;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Model.FullDomain;

namespace Mendeo.Scheduler.Config
{
    public class MappingRegistrar
    {
        public static void AddMapping()
        {
            #region ProductDetail

            Mapper.CreateMap<Product, ProductDetailProductDto>()
                .ForMember(c => c.ProductLastStatusID,
                    o => o.MapFrom(x => x.ProductLastStatusID.HasValue ?
                        (ProductStatusTypeEnum)x.ProductLastStatusID.Value : ProductStatusTypeEnum.NOT_DEFINED));

            Mapper.CreateMap<Image, ProductDetailImageDto>();
            Mapper.CreateMap<ProductImageType, ProductDetailImageTypeDto>();
            Mapper.CreateMap<Attribute, ProductDetailAttributeDto>();
            Mapper.CreateMap<AttributeItem, ProductDetailAttributeItemDto>();
            Mapper.CreateMap<AttributeType, ProductDetailAttributeTypeDto>();
            Mapper.CreateMap<CategoryCultureMap, ProductDetailCategoryCultureMapDto>();
            Mapper.CreateMap<Category, ProductDetailCategoryDto>();
            Mapper.CreateMap<CategoryProductMap, ProductDetailCategoryProductMapDto>();
            Mapper.CreateMap<ProductAttributeMap, ProductDetailProductAttributeMapDto>();
            Mapper.CreateMap<ProductCultureMap, ProductDetailProductCultureMapDto>();
            Mapper.CreateMap<ProductImageMap, ProductDetailImageMapDto>();
            
            #endregion

            Mapper.CreateMap<ProductMailQueue, ProductMailQueueDto>();

            Mapper.CreateMap<PeopleOnline, PeopleOnlineDto>();
            Mapper.CreateMap<PeopleOnlineDto, PeopleOnline>();

            Mapper.CreateMap<Country, ProductDetailCountryDto>();
        }
    }
}
