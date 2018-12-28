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
        /// <param name="ignorePropertiesOrPaths">A list of property names or full path names to ignore</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions options, string[] ignorePropertiesOrPaths)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, options, ignorePropertiesOrPaths);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="ignoreProperties">A list of properties to ignore</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, ComparisonOptions options, params Expression<Func<T, object>>[] ignoreProperties)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, DiffProvider.DefaultMaxDepth, options, ignoreProperties);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="ignorePropertiesOrPaths">A list of property names or full path names to ignore</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(object left, object right, int maxDepth, ComparisonOptions options, params string[] ignorePropertiesOrPaths)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, options, ignorePropertiesOrPaths);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="options">Specify the comparison options</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff<T>(T left, T right, int maxDepth, ComparisonOptions options, params Expression<Func<T, object>>[] ignoreProperties)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, options, ignoreProperties);
        }
    }
}
