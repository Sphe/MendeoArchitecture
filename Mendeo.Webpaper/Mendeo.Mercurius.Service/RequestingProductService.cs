using Mendeo.Common.Core;
using Mendeo.Mercurius.Data.FullDomain.Infrastructure;
using Mendeo.Mercurius.Enum;
using Mendeo.Mercurius.Model.FullDomain;
using Mendeo.Mercurius.Service.Contract;
using Mendeo.Mercurius.Service.Helper;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.Service
{
    public class RequestingProductService : IRequestingProductService
    {
        private readonly ICultureService _cultureService;

        public RequestingProductService(ICultureService cultureService)
        {
            _cultureService = cultureService;
        }
        
        public RequestIndexedProductResponse RequestProductPaginated(RequestIndexedProduct request)
        {
            Check.Require(request != null, "please provide a request");

            //var response = _elasticClient.Search<IndexedProduct>(x => x
            //    .Size(request.Size)
            //    .From(request.From)

            //    .Sort(s =>
            //    {
            //        switch (request.Sort.ToLowerInvariant())
            //        {
            //            case "priceasc":
            //                return s.OnField(xf => xf.Price)
            //                    .Ascending();

            //            case "pricedesc":
            //                return s.OnField(xf => xf.Price)
            //                    .Descending();

            //            case "titleasc":
            //                return s.OnField(xf => xf.ProductName)
            //                    .Ascending();

            //            case "titledesc":
            //                return s.OnField(xf => xf.ProductName)
            //                    .Descending();

            //            case "dateasc":
            //                return s.OnField(xf => xf.CreatedOn)
            //                    .Ascending();

            //            case "datedesc":
            //                return s.OnField(xf => xf.CreatedOn)
            //                    .Descending();

            //            default:
            //                return s.OnField(xf => xf.CreatedOn)
            //                    .Descending();
            //        }
            //    })

            //    .Query(q =>
            //    {
            //        if ((request.Filters == null || request.Filters.Count == 0)
            //            && request.Distance <= 0
            //            && request.FilterCategoryId <= 0
            //            && !request.UserId.HasValue
            //            && string.IsNullOrWhiteSpace(request.QuerySearch))
            //        {
            //            var cont1 = q.Term(t1 => t1
            //            .Value(ProductStatusTypeEnum.APPROVED)
            //            .OnField(f1 => f1.LastStatus));

            //            var cont1bis = q.Term(t1 => t1
            //            .Value(ProductStatusTypeEnum.PERMANENT)
            //            .OnField(f1 => f1.LastStatus));

            //            var cont2 = q.Term(t1 => t1
            //            .Value(request.IsDemand ? (int)ProductAnnounceTypeEnum.DEMAND : (int)ProductAnnounceTypeEnum.OFFER)
            //            .OnField(f1 => f1.ProductAnnounceTypeID));

            //            return cont2 && (cont1 || cont1bis);
            //        }

            //        QueryContainer mandatoryContraint = q.Term(t1 => t1
            //            .Value(ProductStatusTypeEnum.APPROVED)
            //            .OnField(f1 => f1.LastStatus));

            //        QueryContainer mandatoryContraint2 = q.Term(t1 => t1
            //            .Value(ProductStatusTypeEnum.PERMANENT)
            //            .OnField(f1 => f1.LastStatus));

            //        QueryContainer mandatoryTypeDemand = q.Term(t1 => t1
            //            .Value(request.IsDemand ? (int)ProductAnnounceTypeEnum.DEMAND : (int)ProductAnnounceTypeEnum.OFFER)
            //            .OnField(f1 => f1.ProductAnnounceTypeID));

            //        QueryContainer searchUserContainer = null;
            //        if (request.UserId.HasValue)
            //        {
            //            searchUserContainer = q.Term(mm =>
            //            {
            //                mm.Value(request.UserId)
            //                    .OnField(mmf => mmf.UserId);
            //            });
            //        }

            //        QueryContainer searchQueryContainer = null;
            //        if (!string.IsNullOrWhiteSpace(request.QuerySearch))
            //        {
            //            searchQueryContainer = q.MultiMatch(mm =>
            //            {
            //                mm.Query(request.QuerySearch)
            //                    .OnFields(mmf => mmf.ProductName.Suffix("pn-french"),
            //                    //mmf => mmf.ProductName.Suffix("pn-pm"),
            //                    //mmf => mmf.ProductName.Suffix("pn-pmnp"),
            //                    mmf => mmf.ProductName.Suffix("pn-fm"),

            //                    mmf => mmf.ShortDescription.Suffix("sd-french"),
            //                    //mmf => mmf.ShortDescription.Suffix("sd-pm"),
            //                    //mmf => mmf.ShortDescription.Suffix("sd-pmnp"),
            //                    mmf => mmf.ShortDescription.Suffix("sd-fm"),

            //                    mmf => mmf.Description.Suffix("d-french"),
            //                    //mmf => mmf.Description.Suffix("d-pm"),
            //                    //mmf => mmf.Description.Suffix("d-pmnp"),
            //                    mmf => mmf.Description.Suffix("d-fm"));
            //            });
            //        }

            //        var res = q.Filtered(f1 => f1.Filter(f2 =>
            //        {
            //            var filterContainers = new List<FilterContainer>();
            //            var types = request.Filters;

            //            foreach (var type in types)
            //            {
            //                filterContainers.Add(f2.Terms("attributes.type", new[] { type.Key }));

            //                if (type.Childrens == null || type.Childrens.Count() == 0)
            //                    continue;

            //                var items = type.Childrens;

            //                switch (type.PrimitiveType)
            //                {
            //                    case ItemPrimitiveTypeEnum.String:
            //                        filterContainers.Add(f2.Terms("attributes.item", items.Select(localSelect => localSelect.Key).ToArray()));
            //                        break;

            //                    case ItemPrimitiveTypeEnum.Boolean:
            //                        var arBool = items.Select(localSelect => localSelect.BoolValue).ToArray();
            //                        var valBool = arBool.First();
            //                        filterContainers.Add(f2.Term(ff2 => ff2.Attributes.First().ItemBool, (object)valBool));
            //                        break;

            //                    case ItemPrimitiveTypeEnum.Number:
            //                        foreach (var requestBucketComposite in items)
            //                        {
            //                            filterContainers.Add(f2.Range(rnum => rnum
            //                                .OnField(rnumField => rnumField.Attributes.First().ItemNumber)
            //                                .GreaterOrEquals(requestBucketComposite.NumberMinRangeValue.HasValue ? (double?)Convert.ToDouble(requestBucketComposite.NumberMinRangeValue) : null)
            //                                .LowerOrEquals(requestBucketComposite.NumberMaxRangeValue.HasValue ? (double?)Convert.ToDouble(requestBucketComposite.NumberMaxRangeValue) : null)));
            //                        }
            //                        break;

            //                    case ItemPrimitiveTypeEnum.Date:
            //                        foreach (var requestBucketComposite in items)
            //                        {
            //                            filterContainers.Add(f2.Range(rnum => rnum
            //                                .OnField(rnumField => rnumField.Attributes.First().ItemDate)
            //                                .GreaterOrEquals(requestBucketComposite.DateMinRangeValue)
            //                                .LowerOrEquals(requestBucketComposite.DateMaxRangeValue)));
            //                        }
            //                        break;

            //                    default:
            //                        filterContainers.Add(f2.Terms("attributes.item", items.Select(localSelect => localSelect.Key).ToArray()));
            //                        break;
            //                }
            //            }

            //            if (request.Distance > 0)
            //            {
            //                filterContainers.Add(f2.GeoDistance(
            //                    "coordinate",
            //                    d => d.Distance(request.Distance, GeoUnit.Kilometers).Location(request.SourceLat, request.SourceLon)
            //                  ));
            //            }

            //            if (request.FilterCategoryId > 0)
            //            {
            //                var lstCatIds = new List<int>() { request.FilterCategoryId };
            //                filterContainers.Add(f2.Term<IList<int>>(f2t => f2t.CategoryIds, lstCatIds));
            //            }

            //            return f2.And(filterContainers.ToArray());

            //        }));

            //        var returnRes = res;

            //        returnRes = returnRes && mandatoryTypeDemand && (mandatoryContraint || mandatoryContraint2);

            //        if (searchQueryContainer != null)
            //            returnRes = returnRes && searchQueryContainer;

            //        if (searchUserContainer != null)
            //            returnRes = returnRes && searchUserContainer;

            //        return returnRes;
            //    })
            //    .Aggregations(a1 => a1
            //        .Nested("attributes", n1 => n1
            //            .Path(p1 => p1.Attributes)
            //            .Aggregations(x2 => x2
            //                .Terms("attribute_agg", t => t
            //                        .Size(Int32.MaxValue)
            //                        .Field("attributes.type")
            //                        .Aggregations(x3 => x3
            //                            .Terms("attribute_type_primitive_type_id",
            //                                st0 =>
            //                                    st0.Field("attributes.itemPrimitiveType")
            //                                        .Size(1))
            //                            .Filter("filter_attribute_item_agg_string", f1 =>
            //                                f1
            //                                .Filter(fc1 => fc1.Or(fc1.Terms(fta1 => fta1.Attributes.First().ItemPrimitiveType, new[] { ItemPrimitiveTypeEnum.String }),
            //                                                        fc1.Not(fcn1 => fc1.Exists((ftae1 => ftae1.Attributes.First().ItemPrimitiveType)))))
            //                                .Aggregations(fa1 =>
            //                                    fa1.Terms("attribute_item_agg_string", t2 => t2.Size(Int32.MaxValue)
            //                                        .Field("attributes.item")
            //                                        ))
            //                            )
            //                            .Filter("filter_attribute_item_agg_date", f2 =>
            //                                f2
            //                                .Filter(fc2 => fc2.Terms(fta2 => fta2.Attributes.First().ItemPrimitiveType, new[] { ItemPrimitiveTypeEnum.Date }))
            //                                .Aggregations(fa1 =>
            //                                    fa1.Terms("attribute_item_agg_date", t2 => t2.Size(Int32.MaxValue)
            //                                        .Field("attributes.itemDate")
            //                                        ))
            //                            )
            //                            .Filter("filter_attribute_item_agg_number", f3 =>
            //                                f3
            //                                .Filter(fc3 => fc3.Terms(fta3 => fta3.Attributes.First().ItemPrimitiveType, new[] { ItemPrimitiveTypeEnum.Number }))
            //                                .Aggregations(fa1 =>
            //                                    fa1.Terms("attribute_item_agg_number", t2 => t2.Size(Int32.MaxValue)
            //                                        .Field("attributes.itemNumber")
            //                                        )
            //                                        .Min("attribute_item_agg_number_min", min => min.Field(minField => minField.Attributes.First().ItemNumber))
            //                                        .Max("attribute_item_agg_number_max", max => max.Field(minField => minField.Attributes.First().ItemNumber)))
            //                            )
            //                            .Filter("filter_attribute_item_agg_bool", f4 =>
            //                                f4
            //                                .Filter(fc4 => fc4.Terms(fta4 => fta4.Attributes.First().ItemPrimitiveType, new[] { ItemPrimitiveTypeEnum.Boolean }))
            //                                .Aggregations(fa1 =>
            //                                    fa1.Terms("attribute_item_agg_bool", t2 => t2.Size(Int32.MaxValue)
            //                                        .Field("attributes.itemBool")
            //                                        ))
            //                            )
            //                            ))))));

            var returnResponse = new RequestIndexedProductResponse();
            returnResponse.QuerySearch = request.QuerySearch;
            //returnResponse.Total = response.Total;
            //returnResponse.ContextTotal = response.Documents.Count();

            //var lstDoc = response.Documents.ToList();
            //lstDoc.ForEach(x => EnrichIndexProduct(x));

            //returnResponse.ContextProducts = lstDoc;

            //returnResponse.TimeRequestElapsed = response.ElapsedMilliseconds;
            //var bucketTree = BuildAttributeTree(response.Aggs.Nested("attributes"), request.Filters);
            //returnResponse.BucketTree = bucketTree.Skip(request.FromBucketTree).Take(request.SizeBucketTree).ToList();
            //returnResponse.TotalBucketTree = bucketTree.Count();
            return returnResponse;
        }

        private void EnrichIndexProduct(IndexedProduct x)
        {
            if (x.Images.Count == 0)
            {
                x.Images = new List<IndexedImage>(){
                    { new IndexedImage() { ImageUrl = ServiceImageHelper.DEFAULT_IMAGE_PRODUCT_LIST_TYPE_1, ImageType = "ImageProductList" } },
                    { new IndexedImage() { ImageUrl = ServiceImageHelper.DEFAULT_IMAGE_PRODUCT_VIEW_TYPE_2, ImageType = "ImageProductView" }},
                    { new IndexedImage() { ImageUrl = ServiceImageHelper.DEFAULT_IMAGE_PRODUCT_LINK_TYPE_3, ImageType = "ImageProductViewLink" }},
                    { new IndexedImage() { ImageUrl = ServiceImageHelper.DEFAULT_IMAGE_PRODUCT_MINI_TYPE_4, ImageType = "ImageProductMini" }},
                    { new IndexedImage() { ImageUrl = ServiceImageHelper.DEFAULT_IMAGE_PRODUCT_HOME_TYPE_5, ImageType = "ImageProductHome" }}
                };
            }
        }

        private IList<RequestBucketComposite> BuildAttributeTree(SingleBucket nestedBucket, IList<RequestBucketComposite> selected)
        {
            var typesBucket = nestedBucket.Terms("attribute_agg");
            var result = new List<RequestBucketComposite>();

            if (selected == null)
            {
                return result;
            }

            foreach (var type in typesBucket.Items)
            {
                var selectedType = selected.Where(x => string.Compare(x.Key, type.Key, true) == 0);

                var typeEntity = new RequestBucketComposite();
                typeEntity.Key = type.Key;
                typeEntity.Name = type.Key;
                typeEntity.DocumentCount = type.DocCount;
                typeEntity.BucketTransformationType = Enums.BucketTransformationEnum.TermType;
                typeEntity.Childrens = new List<RequestBucketComposite>();

                if (selectedType.Any())
                {
                    typeEntity.Selected = true;
                }

                var primitiveTypeIdBucket = type.Terms("attribute_type_primitive_type_id");

                if (primitiveTypeIdBucket.Items.Count > 0)
                {
                    var firstTopPrimitive = primitiveTypeIdBucket.Items.First().Key;
                    if (!string.IsNullOrWhiteSpace(firstTopPrimitive))
                    {
                        typeEntity.PrimitiveType = (ItemPrimitiveTypeEnum)Convert.ToInt32(firstTopPrimitive);
                    }
                    else
                    {
                        typeEntity.PrimitiveType = ItemPrimitiveTypeEnum.String;
                    }
                }
                else
                {
                    typeEntity.PrimitiveType = ItemPrimitiveTypeEnum.String;
                }

                switch (typeEntity.PrimitiveType)
                {
                    case ItemPrimitiveTypeEnum.String:
                        var typeFilterBucketString = type.Filter("filter_attribute_item_agg_string");
                        var itemsBucketString = typeFilterBucketString.Terms("attribute_item_agg_string");

                        foreach (var item in itemsBucketString.Items)
                        {
                            var itemEntity = new RequestBucketComposite();
                            itemEntity.DocumentCount = item.DocCount;
                            itemEntity.BucketTransformationType = Enums.BucketTransformationEnum.TermItem;
                            itemEntity.Key = item.Key;
                            itemEntity.Name = item.Key;
                            itemEntity.Childrens = null;

                            if (selectedType.Any())
                            {
                                var selectedItem = selectedType.First().Childrens.Where(x => string.Compare(x.Key, item.Key, true) == 0);
                                itemEntity.Selected = selectedItem.Any();
                            }

                            typeEntity.Childrens.Add(itemEntity);
                        }

                        result.Add(typeEntity);
                        break;

                    case ItemPrimitiveTypeEnum.Number:
                        var typeFilterBucketNum = type.Filter("filter_attribute_item_agg_number");
                        var itemsBucketNum = typeFilterBucketNum.Terms("attribute_item_agg_number");
                        var min = typeFilterBucketNum.Min("attribute_item_agg_number_min").Value;
                        var max = typeFilterBucketNum.Min("attribute_item_agg_number_max").Value;
                        var floor = Math.Floor(min.Value);
                        var ceil = Math.Ceiling(max.Value);

                        typeEntity.MinValue = min;
                        typeEntity.MaxValue = max;
                        typeEntity.MinFloorValue = floor;
                        typeEntity.MaxCeilValue = ceil;

                        foreach (var item in itemsBucketNum.Items)
                        {
                            var itemEntity = new RequestBucketComposite();
                            itemEntity.DocumentCount = item.DocCount;
                            itemEntity.BucketTransformationType = Enums.BucketTransformationEnum.TermItem;
                            itemEntity.Key = item.Key;
                            itemEntity.Name = item.Key;
                            itemEntity.Childrens = null;

                            itemEntity.MinValue = min;
                            itemEntity.MaxValue = max;
                            itemEntity.MinFloorValue = floor;
                            itemEntity.MaxCeilValue = ceil;

                            if (selectedType.Any())
                            {
                                var selectedItem = selectedType.First().Childrens.Where(x => string.Compare(x.Key, item.Key, true) == 0);
                                itemEntity.Selected = selectedItem.Any();
                            }

                            typeEntity.Childrens.Add(itemEntity);
                        }

                        result.Add(typeEntity);
                        break;

                    case ItemPrimitiveTypeEnum.Date:
                        var typeFilterBucketDate = type.Filter("filter_attribute_item_agg_date");
                        var itemsBucketDate = typeFilterBucketDate.Terms("attribute_item_agg_date");

                        foreach (var item in itemsBucketDate.Items)
                        {
                            var itemEntity = new RequestBucketComposite();
                            itemEntity.DocumentCount = item.DocCount;
                            itemEntity.BucketTransformationType = Enums.BucketTransformationEnum.TermItem;
                            itemEntity.Key = item.Key;
                            itemEntity.Name = item.Key;
                            itemEntity.Childrens = null;

                            if (selectedType.Any())
                            {
                                var selectedItem = selectedType.First().Childrens.Where(x => string.Compare(x.Key, item.Key, true) == 0);
                                itemEntity.Selected = selectedItem.Any();
                            }

                            typeEntity.Childrens.Add(itemEntity);
                        }

                        result.Add(typeEntity);
                        break;

                    case ItemPrimitiveTypeEnum.Boolean:
                        var typeFilterBucketBool = type.Filter("filter_attribute_item_agg_bool");
                        var itemsBucketBool = typeFilterBucketBool.Terms("attribute_item_agg_bool");

                        foreach (var item in itemsBucketBool.Items)
                        {
                            var itemEntity = new RequestBucketComposite();
                            itemEntity.DocumentCount = item.DocCount;
                            itemEntity.BucketTransformationType = Enums.BucketTransformationEnum.TermItem;
                            itemEntity.Key = item.Key;
                            itemEntity.Name = item.Key;
                            itemEntity.Childrens = null;

                            if (selectedType.Any())
                            {
                                var selectedItem = selectedType.First().Childrens.Where(x => string.Compare(x.Key, item.Key, true) == 0);
                                itemEntity.Selected = selectedItem.Any();
                            }

                            typeEntity.Childrens.Add(itemEntity);
                        }

                        result.Add(typeEntity);
                        break;

                    default:
                        var typeFilterBucketStringDefault = type.Filter("filter_attribute_item_agg_string");
                        var itemsBucketStringDefault = typeFilterBucketStringDefault.Terms("attribute_item_agg_string");

                        foreach (var item in itemsBucketStringDefault.Items)
                        {
                            var itemEntity = new RequestBucketComposite();
                            itemEntity.DocumentCount = item.DocCount;
                            itemEntity.BucketTransformationType = Enums.BucketTransformationEnum.TermItem;
                            itemEntity.Key = item.Key;
                            itemEntity.Name = item.Key;
                            itemEntity.Childrens = null;

                            if (selectedType.Any())
                            {
                                var selectedItem = selectedType.First().Childrens.Where(x => string.Compare(x.Key, item.Key, true) == 0);
                                itemEntity.Selected = selectedItem.Any();
                            }

                            typeEntity.Childrens.Add(itemEntity);
                        }

                        result.Add(typeEntity);
                        break;
                }
            }

            return result;
        }
    }
}
