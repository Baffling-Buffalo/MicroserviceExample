using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// See if this enumeration has all items in passed enumeration. Returns true if passed enum is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            if (!b.Any()) return true;
            return !b.Except(a).Any();
        }

        /// <summary>
        /// See if this enumeration has all items in passed enumeration. Returns true if passed enum is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ContainsAllItems<T>(this List<T> a, List<T> b)
        {
            if (!b.Any()) return true;
            return !b.Except(a).Any();
        }
    }
}
