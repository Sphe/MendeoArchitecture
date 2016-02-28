using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mendeo.Mercurius.Dto.ProductDetail;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Model.FullDomain.Enum;

namespace Mendeo.Mercurius.Dto
{
    public class ProductDetailProductDto : BaseDto
    {
        public int ProductID { get; set; }
        public Nullable<int> PostalCodeID { get; set; }
        public Nullable<int> CityID { get; set; }
        public string GoogleAddressInLine { get; set; }
        public decimal GoogleAddressLongitude { get; set; }
        public decimal GoogleAddressLatitude { get; set; }
        public string GoogleCountryCode { get; set; }
        public Nullable<int> ProductAnnounceTypeID { get; set; }
        public Nullable<int> SellerTypeID { get; set; }

        public bool IsProfessional
        {
            //Return true if the SellerTypeEnum is set to LegalEntity
            get { return SellerTypeID == (int) SellerTypeEnum.LEGAL_ENTITY; }
            set
            {
                if (value)
                {
                    SellerTypeID = (int) SellerTypeEnum.LEGAL_ENTITY;
                }
                else
                {
                    SellerTypeID = (int) SellerTypeEnum.NATURAL_PERSON;
                }
            }
        }

        public bool IsDemand
        {
            get { return ProductAnnounceTypeID == (int)ProductAnnounceTypeEnum.DEMAND; }
        }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> NumberOfImage { get; set; }
        public Nullable<int> NumberOfAttributes { get; set; }
        public ProductStatusTypeEnum ProductLastStatusID { get; set; }
        public bool Activate { get; set; }
        public Nullable<int> Sort { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public string UserEmail { get; set; }

        public string UserPhone { get; set; }

        public int ViewCount { get; set; }

        public bool? IsUserFavorite { get; set; }

        public int? UserID { get; set; }

        public ProductDetailCountryDto Country { get; set; }

        public ICollection<ProductDetailCategoryProductMapDto> CategoryProductMap { get; set; }
        public ICollection<ProductDetailProductAttributeMapDto> ProductAttributeMap { get; set; }
        public ICollection<ProductDetailProductCultureMapDto> ProductCultureMap { get; set; }
        public ICollection<ProductDetailImageMapDto> ProductImageMap { get; set; }
    }
}
