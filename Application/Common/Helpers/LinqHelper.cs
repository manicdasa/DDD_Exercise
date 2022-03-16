using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GhostWriter.Application.Common.Helpers
{ 
    public static class LinqHelper
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool PropertyExists<T>(this IQueryable<T> source, string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
        }

        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };

        public static IEnumerable<T> OrderByProperty<T>(
           this IQueryable<T> source, string propertyName)
        {
            var sorted = source.AsEnumerable().OrderBy(x => GetPropertyValue(x, propertyName));
            return sorted;
        }

        public static IEnumerable<T> OrderByPropertyDescending<T>(
            this IQueryable<T> source, string propertyName)
        {
            var sorted = source.AsEnumerable().OrderByDescending(x => GetPropertyValue(x, propertyName));
            return sorted;
        }

        public static IEnumerable<T> OrderByPropertyName<T>(IQueryable<T> query, string orderByColumn, bool orderByAsc)
        {
            if (query == null)
                return null;

            IEnumerable<T> queryOrdered;
            if (orderByAsc)
                queryOrdered = LinqHelper.OrderByProperty<T>(query, orderByColumn);
            else
                queryOrdered = LinqHelper.OrderByPropertyDescending<T>(query, orderByColumn);

            return queryOrdered;
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            try
            {
                //var pom = FirstCharToUpper(propertyName).Split('.').Select(s => obj.GetType().GetProperty(s));
                foreach (var prop in FirstCharToUpper(propertyName).Split('.').Select(s => obj.GetType().GetProperty(s)))
                {
                    obj = prop.GetValue(obj, null);
                }
                return obj;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        

    }
}
