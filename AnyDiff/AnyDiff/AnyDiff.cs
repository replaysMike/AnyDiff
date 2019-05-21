using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AnyDiff
{
    /// <summary>
    /// Compare two objects for value differences
    /// </summary>
    public static class AnyDiff
    {

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, ComparisonOptions.All);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="options">Specify the comparison options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right, ComparisonOptions options)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, options);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="options">Specify the comparison options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions options)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, options, null);
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
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions options, string[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, options, propertyList);
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
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions options, params Expression<Func<T, object>>[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, options, propertyList);
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
        public static ICollection<Difference> Diff(object left, object right, int maxDepth, ComparisonOptions options, params string[] propertyList)
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
        public static ICollection<Difference> Diff<T>(T left, T right, int maxDepth, ComparisonOptions options, params Expression<Func<T, object>>[] propertyList)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, options, propertyList);
        }
    }
}
