using System.Collections.Generic;
using System.Linq;
using Mendeo.Mercurius.Dto.AttributeAutocomplete;
using Mendeo.Mercurius.Dto.AttributeSuggest;
using Mendeo.Mercurius.Service.Contract;
using System.Web.Http;

namespace Mendeo.Mercurius.WebApi
{
    public class AttributeController : ApiController
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet]
        public AttributeSuggestContainerDto GetAttributeSuggests(int skip, int count, int categoryId)
        {
            return _attributeService.GetAttributeSuggests(skip, count, categoryId);
        }

        [HttpGet]
        public AttributeSuggestContainerDto GetAttributeSuggestsFromQuery(int skip, int count, string query)
        {
            return _attributeService.GetAttributeSuggestsFromQuery(skip, count, query);
        }

        [HttpGet]
        public IList<AttributeAutocompleteTypeDto> GetAttributeAutocompleType(string query)
        {
            return _attributeService.GetAttributeAutocompleteType(query, 25);
        }

        [HttpGet]
        public IList<AttributeAutocompleteItemDto> GetAttributeAutocompleItem(int? attributeTypeId, string query)
        {
            if (!attributeTypeId.HasValue)
            {
                return Enumerable.Empty<AttributeAutocompleteItemDto>().ToList();
            }

            return _attributeService.GetAttributeAutocompleteItem(attributeTypeId.Value, query, 25);
        }
    }
}
