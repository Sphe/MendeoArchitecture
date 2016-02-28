using Mendeo.Mercurius.Dto.AttributeAutocomplete;
using Mendeo.Mercurius.Dto.AttributeSuggest;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Model.FullDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = Mendeo.Mercurius.Model.FullDomain.Attribute;

namespace Mendeo.Mercurius.Service.Contract
{
    public interface IAttributeService
    {
        EntityWithValidation<AttributeType> GetOrCreateType(string type, ItemPrimitiveTypeEnum? primitiveType, string unitLabel);

        EntityWithValidation<AttributeItem> GetOrCreateItem(string type, string item, ItemPrimitiveTypeEnum? primitiveType, 
            bool? itemBool, DateTime? itemDate, decimal? itemNumber, string unitLabel);

        EntityWithValidation<Attribute> CreateAttribute(AttributeType type, AttributeItem item, int? categoryId = null);

        void DeleteAllAttributes(ICollection<ProductAttributeMap> attributeMap);

        void DeleteAttribute(Mendeo.Mercurius.Model.FullDomain.Attribute attribute);

        AttributeSuggestContainerDto GetAttributeSuggests(int skip, int count, int categoryId);

        IList<AttributeAutocompleteTypeDto> GetAttributeAutocompleteType(string query, int numTake);

        IList<AttributeAutocompleteItemDto> GetAttributeAutocompleteItem(int attributeTypeId, string query, int numTake);

        AttributeSuggestContainerDto GetAttributeSuggestsFromQuery(int skip, int count, string name);
    }
}
