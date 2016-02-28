using Mendeo.Mercurius.Dto;
using Mendeo.Mercurius.Service.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mendeo.Mercurius.ExcelBusiness
{
    public class ProductModule : IProductModule
    {
        private readonly IProductService _productService;

        public ProductModule(IProductService productService)
        {
            _productService = productService;
        }

        //public void UpdateFromProductFlatten(DataTable dt, DataRow row)
        //{
        //    var changedColumns = GetChangedColumns(dt, row);
        //    var objChanged = changedColumns.GroupBy(x =>
        //    {
        //        var splitHierarachy = x.ColumnName.Split(new[] {"_"}, StringSplitOptions.None);
        //        return splitHierarachy[splitHierarachy.Length - 2];
        //    });

        //    var updateConfig = new Dictionary<string, Action<DataRow, IList<DataColumn>>>();
        //    updateConfig.Add(typeof(AdminProductViewProductDto).Name, ((x, y) =>
        //    {
        //        var objDest = ActivationObjectFromFlattenDataTable<AdminProductViewProductDto>(x);                
        //        _proxyTransactionProductService.UpdateProduct(objDest);
        //    }));
        //    updateConfig.Add(typeof(AdminProductViewProductCultureDto).Name, ((x, y) =>
        //    {
        //        var productDto = ActivationObjectFromFlattenDataTable<AdminProductViewProductDto>(x);
        //        var cultureDto = ActivationObjectFromFlattenDataTable<AdminProductViewProductCultureDto>(x);
        //        _proxyTransactionProductService.UpdateProductCultureMap(productDto.Id, cultureDto);
        //    }));

        //    foreach (var strObject in objChanged.Select(x => x.Key).ToList())
        //    {
        //        Action<DataRow, IList<DataColumn>> action;

        //        if (updateConfig.TryGetValue(strObject, out action))
        //        {
        //            action(row,
        //                objChanged.Where(x => string.Compare(x.Key, strObject, true, CultureInfo.InvariantCulture) == 0).First()
        //                .Select(x => x)
        //                .ToList());
        //        }

        //    }
        //}

        public T ActivationObjectFromFlattenDataTable<T>(DataRow dr)
            where T : class 
        {
            object obj = Activator.CreateInstance(typeof(T));
            var props = GetPropertiesToProcess(obj.GetType());

            var colList = dr.Table.Columns.Cast<DataColumn>().GroupBy(z1 =>
            {
                var splitHierarachy = z1.ColumnName.Split(new[] { "_" }, StringSplitOptions.None);
                return splitHierarachy[splitHierarachy.Length - 2];
            }).First(z3 => string.Compare(z3.Key, typeof(T).Name, StringComparison.InvariantCulture) == 0).Select(z3 => z3);

            foreach (var propertyInfo in props)
            {
                if (null != propertyInfo && propertyInfo.CanWrite)
                {
                    var nameCol = colList.Where(z3 =>
                    {
                        var arNames = z3.ColumnName.Split(new[] { "_" }, StringSplitOptions.None);
                        return
                            string.Compare(arNames[arNames.Length - 1], propertyInfo.Name,
                                StringComparison.InvariantCulture) == 0;
                    }).FirstOrDefault();

                    if (nameCol == null || dr[nameCol] == null || dr[nameCol] is DBNull)
                    {
                        continue;
                    }

                    propertyInfo.SetValue(obj, dr[nameCol], null);
                }
            }

            var objDest = obj as T;
            return objDest;
        }

        public void CreateFromProductFlatten()
        {

        }

        public void DeleteFromProductFlatten()
        {
            
        }

        private IList<DataColumn> GetChangedColumns(DataTable changedTable, DataRow row)
        {
            var listResult = new List<DataColumn>();
            foreach (DataColumn dc in changedTable.Columns)
            {
                if (!row[dc, DataRowVersion.Original].Equals(
                     row[dc, DataRowVersion.Current])) /* skipped Proposed as indicated by a commenter */
                {
                    listResult.Add(dc);
                }
            }
            return listResult;
        }

        public DataTable GetAllProductFlatten()
        {
            var allProducts = _productService.GetAllProducts();

            var lstProductsFlatten = new List<IDictionary<string, object>>();

            foreach (var adminProductViewProductDto in allProducts)
            {
                var dicoProduct = new Dictionary<string, object>();
                lstProductsFlatten.Add(dicoProduct);
                IteratePropertiesRecursively(string.Empty, adminProductViewProductDto, dicoProduct).ToList();
            }

            var dataTable = lstProductsFlatten.ToADOTableFromDico();

            return dataTable;
        }

        public string ProcessInnerLevel(string prefix, Type type, PropertyInfo p, object value, IDictionary<string, object> result, bool isCollection)
        {
            var destType = type.IsGenericType ? type.GetGenericArguments()[0] : type;
            // then enumerate the generic subtype

            MethodInfo method = typeof(ProductModule).GetMethod("IteratePropertiesRecursively");
            MethodInfo generic = method.MakeGenericMethod(destType);
            IEnumerable<string> resDynamic = null;
            if (isCollection)
            {
                var coll = value as IEnumerable;
                var typeColl = value.GetType();
                var count = 0;
                foreach(var emt in coll)
                {
                    resDynamic = generic.Invoke(this, new[] { string.Concat(prefix, destType.Name, ":", count, "_"), emt, result }) as IEnumerable<string>;
                    count++;
                }

                if (resDynamic == null)
                {
                    return string.Empty;
                }
            }
            else
            {
                resDynamic = generic.Invoke(this, new[] { string.Concat(prefix, destType.Name, "_"), value, result }) as IEnumerable<string>;
            }

            var lstResDynamic = resDynamic.ToList();
            foreach (string propertyName in lstResDynamic)
            {
                return propertyName;
            }

            return string.Empty;
        }

        public PropertyInfo[] GetPropertiesToProcess(Type t)
        {
            var ps = t.GetProperties().Where(
                prop => true /* Attribute.IsDefined(prop, typeof(BusinessAttribute)) */);

            return ps.ToArray();
        }

        public IEnumerable<string> IteratePropertiesRecursively<T>(string prefix, T value, IDictionary<string, object> result)
        {
            var t = typeof (T);

            if (!string.IsNullOrEmpty(prefix) && !prefix.EndsWith("_")) prefix += "_";
            prefix += t.Name + "_";

            // enumerate the properties of the type
            foreach (PropertyInfo p in GetPropertiesToProcess(t))
            {
                Type pt = p.PropertyType;

                // if property is a generic list
                if (pt.Name == "ICollection`1" || pt.Name == "IList`1" || pt.Name == "IEnumerable`1")
                {
                    yield return ProcessInnerLevel(prefix, pt, p, p.GetValue(value), result, true);
                }
                else if (pt.IsSubclassOf(typeof(BaseDto)))
                {
                    yield return ProcessInnerLevel(prefix, pt, p, p.GetValue(value), result, false);
                }
                else
                {
                    var ojbValue = value != null ? p.GetValue(value) : null;

                    if (ojbValue != null 
                        &&
                        !(ojbValue is string &&
                            string.IsNullOrWhiteSpace(Convert.ToString(ojbValue))))
                    {
                        if (!result.ContainsKey(prefix + p.Name))
                            result.Add(prefix + p.Name, ojbValue);   
                    }
                    
                    yield return prefix + p.Name;
                }
            }
        }

        public void UpdateFromProductFlatten(DataTable dt, DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
