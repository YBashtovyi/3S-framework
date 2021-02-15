// Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtm.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace App.DocumentTemplates.Extensions
{

    public static class CollectionExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> e, Func<T, IEnumerable<T>> f)
        {
            return e != null ? e.SelectMany(c => f(c).Flatten(f)).Concat(e) : Enumerable.Empty<T>();
        }

        //public static IQueryable<T> FilterOwner<T>(this IQueryable<T> e, long owner) where T : class, IOwnedEntity
        //{
        //    return e.Where(_ => _.OwnerId == owner);
        //}

        public static IEnumerable<T> GetParentHierarchy<T>(this IQueryable<T> dbSet, Func<T, bool> f, string rootName = null) where T : class, ISelfReferenced<T>
        {
            return dbSet.GetParentHierarchyInternal(f, rootName);
        }

        public static Task<IEnumerable<T>> GetParentHierarchyAsync<T>(this IQueryable<T> dbSet, Func<T, bool> f, string rootName = null) where T : class, ISelfReferenced<T>
        {
            return Task.Factory.StartNew<IEnumerable<T>>(() => GetParentHierarchyInternal(dbSet, f, rootName));
        }

        private static IEnumerable<T> GetParentHierarchyInternal<T>(this IQueryable<T> dbSet, Func<T, bool> f, string rootName = null) where T : class, ISelfReferenced<T>
        {
            var result = new List<T>();
            T parent = dbSet.Include(p => p.Parent).FirstOrDefault(x => f(x));
            if (parent != null)
            {
                result.Add(parent);
            }
            while (parent.Parent != null && (rootName == null || parent.Parent.Caption != rootName))
            {
                parent = parent.Parent;
                result.Add(parent);
            }
            result.Reverse();
            //return result.Select(_ => _).AsQueryable();
            return result;
        }
    }
}
