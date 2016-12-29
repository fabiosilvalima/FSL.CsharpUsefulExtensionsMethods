using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FSL.CsharpUsefulExtensionsMethods
{
    public static class FSLCollectionExtension
    {
        public static string GetValue(this NameValueCollection collection, string key, string def = null)
        {
            var tmp = "";
            if (collection.HasKey(key))
            {
                tmp = collection.Get(key);
                if (!tmp.IsNullOrEmpty())
                {
                    return tmp;
                }
            }

            return def;
        }
        
        public static bool HasKey(this NameValueCollection queryString, string key)
        {
            if (queryString != null && !key.IsNullOrEmpty() && queryString.AllKeys.Contains(key))
            {
                var qsTemplate = queryString.Get(key);

                return true;
            }

            return false;
        }

        public static bool IsGratherThanZero<T>(this IEnumerable<T> collection)
        {
            return collection.IsGratherThan<T>(0);
        }

        public static bool IsGratherThan<T>(this IEnumerable<T> collection, int count)
        {
            if (collection == null)
            {
                return false;
            }

            return collection.Count() > count;
        }
        
        public static NameValueCollection ParseQueryString(this string queryString)
        {
            if (!string.IsNullOrEmpty(queryString))
            {
                return HttpUtility.ParseQueryString(queryString);
            }

            return new NameValueCollection();
        }
        
        public static string ToQueryString(this NameValueCollection collection)
        {
            if (collection != null && collection.Count > 0)
            {
                return string.Join("&", collection.AllKeys.Select(a => string.Format("{0}={1}", HttpUtility.UrlEncode(a), HttpUtility.UrlEncode(collection[a]))));
            }

            return "";
        }

        public static TModel SingleOrNew<TModel>(this List<TModel> collection)
        {
            var obj = collection.SingleOrDefault();
            if (obj == null)
            {
                return Activator.CreateInstance<TModel>();
            }

            return obj;
        }
        
        public static TModel SingleOrNew<TModel>(this List<TModel> collection, Func<TModel, bool> predicate)
        {
            var obj = collection.SingleOrDefault(predicate);
            if (obj == null)
            {
                return Activator.CreateInstance<TModel>();
            }

            return obj;
        }
        
        public static TModel SingleOrNew<TModel>(this IEnumerable<TModel> collection, Func<TModel, bool> predicate)
        {
            var obj = collection.SingleOrDefault(predicate);
            if (obj == null)
            {
                return Activator.CreateInstance<TModel>();
            }

            return obj;
        }
        
        public static TModel FirstOrNew<TModel>(this List<TModel> collection)
        {
            var obj = collection.FirstOrDefault();
            if (obj == null)
            {
                return Activator.CreateInstance<TModel>();
            }

            return obj;
        }
        
        public static TModel FirstOrNew<TModel>(this IEnumerable<TModel> collection)
        {
            var obj = collection.FirstOrDefault();
            if (obj == null)
            {
                return Activator.CreateInstance<TModel>();
            }

            return obj;
        }
        
        public static TModel FirstOrNew<TModel>(this List<TModel> collection, Func<TModel, bool> predicate)
        {
            var obj = collection.FirstOrDefault(predicate);
            if (obj == null)
            {
                return Activator.CreateInstance<TModel>();
            }

            return obj;
        }

        public static TModel FirstOrNew<TModel>(this IEnumerable<TModel> collection, Func<TModel, bool> predicate)
        {
            var obj = collection.FirstOrDefault(predicate);
            if (obj == null)
            {
                return Activator.CreateInstance<TModel>();
            }

            return obj;
        }
        
        public static List<TModel> AddOnEmpty<TModel>(this List<TModel> collection)
        {
            if (collection.Count().Equals(0))
            {
                collection.Add(Activator.CreateInstance<TModel>());
            }

            return collection;
        }
        
        public static DataSet ToDataSet<TModel>(this IList<TModel> collection)
        {
            var dts = new DataSet();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(TModel));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (TModel item in collection)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            dts.Tables.Add(table);

            return dts;
        }
        
        public static void AddIfNotContains<T>(this IList<T> collection, T value)
        {
            if (value != null && !collection.Contains(value))
            {
                collection.Add(value);
            }
        }
        
        public static void AddRangeIfNotContains<T>(this IList<T> collection, IEnumerable<T> values)
        {
            if (values != null)
            {
                foreach (var value in values)
                {
                    collection.AddIfNotContains(value);
                }
            }
        }
        
        public static void AddIfTrueAndNotContains<T>(this IList<T> collection, T value, bool flag)
        {
            if (value != null && flag)
            {
                collection.AddIfNotContains(value);
            }
        }

        public static int IndexOf<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            var index = 0;
            if (collection == null) return index;

            var item = collection.SingleOrDefault(predicate);
            if (item != null) index = collection.IndexOf(item);

            return index;
        }

        private static string GetProperty<T>(Expression<T> expression)
        {
            var body = expression.Body.ToString();
            body = body.Substring(body.IndexOf('.') + 1).Remove("(").Remove(")");

            return body;
        }
        
        public static IList<T> AddIndexItem<T>(this IList<T> collection, Expression<Func<T, string>> expressionName, string valueName, int index)
        {
            return AddIndexItem(collection, expressionName, valueName, null, null, index);
        }
        
        public static IList<T> AddIndexItem<T>(this IList<T> collection, Expression<Func<T, string>> expressionName,
            string valueName, Expression<Func<T, int?>> expressionId, int? valueId, int index)
        {
            T instance = Activator.CreateInstance<T>();
            var type = instance.GetType();
            if (expressionName != null)
            {
                type.GetProperty(GetProperty(expressionName)).SetValue(instance, valueName);
            }
            if (expressionId != null)
            {
                type.GetProperty(GetProperty(expressionId)).SetValue(instance, valueId);
            }
            collection.Insert(index, instance);

            return collection;
        }
        
        public static IList<T> AddFirstItem<T>(this IList<T> collection, Expression<Func<T, string>> expressionName,
            string valueName, Expression<Func<T, int?>> expressionId, int? valueId)
        {
            return AddIndexItem(collection, expressionName, valueName, expressionId, valueId, 0);
        }
        
        public static IList<T> AddLastItem<T>(this IList<T> collection, Expression<Func<T, string>> expressionName, string valueName)
        {
            return AddLastItem(collection, expressionName, valueName, null, null);
        }
        
        public static IList<T> AddLastItem<T>(this IList<T> collection, Expression<Func<T, string>> expressionName, string valueName,
            Expression<Func<T, int?>> expressionId, int? valueId)
        {
            return AddIndexItem(collection, expressionName, valueName, expressionId, valueId, collection.Count);
        }
    }
}
