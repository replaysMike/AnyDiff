using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnyDiff
{
    public static class Extensions
    {
        private const int DefaultMaxDepth = -1;

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right)
        {
            return Diff(left, right, DefaultMaxDepth);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="ignoreProperties">A list of properties to ignore</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(this T left, T right, params Expression<Func<T, object>>[] ignoreProperties)
        {
            return Diff(left, right, DefaultMaxDepth, ignoreProperties);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right, int maxDepth)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(this T left, T right, int maxDepth, params Expression<Func<T, object>>[] ignoreProperties)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, ignoreProperties);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="allowCompareDifferentObjects">True to allow comparison of objects of a different type</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right, int maxDepth, bool allowCompareDifferentObjects)
        {
            var diffProvider = new DiffProvider();
            var ignorePropertiesList = new List<string>();
            return diffProvider.ComputeDiff(left, right, maxDepth, allowCompareDifferentObjects);
        }
    }
}
