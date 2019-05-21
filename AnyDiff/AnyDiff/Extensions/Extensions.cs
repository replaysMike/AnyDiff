using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnyDiff.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, ComparisonOptions.All);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="options">Specify the comparison options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right, ComparisonOptions options)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, options);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right, ComparisonOptions options, params string[] propertyList)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, options, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right, params string[] propertyList)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, ComparisonOptions.All, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(this T left, T right, params Expression<Func<T, object>>[] propertyList)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, ComparisonOptions.All, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(this T left, T right, ComparisonOptions options, params Expression<Func<T, object>>[] propertyList)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, options, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right, int maxDepth, ComparisonOptions options, params string[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, options, propertyList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="propertyList">A list of property names or full path names to include/exclude</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(this T left, T right, int maxDepth, ComparisonOptions options, params Expression<Func<T, object>>[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, options, propertyList);
        }
    }
}
