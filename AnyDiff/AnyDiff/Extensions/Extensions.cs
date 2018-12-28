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
        /// <param name="ignorePropertiesOrPaths">A list of property names or full path names to ignore</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right, ComparisonOptions options, params string[] ignorePropertiesOrPaths)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, options, ignorePropertiesOrPaths);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="ignorePropertiesOrPaths">A list of property names or full path names to ignore</param>
        /// <returns></returns>
        public static ICollection<Difference> Diff(this object left, object right, params string[] ignorePropertiesOrPaths)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, ComparisonOptions.All, ignorePropertiesOrPaths);
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
            return Diff(left, right, DiffProvider.DefaultMaxDepth, ComparisonOptions.All, ignoreProperties);
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
        public static ICollection<Difference> Diff<T>(this T left, T right, ComparisonOptions options, params Expression<Func<T, object>>[] ignoreProperties)
        {
            return Diff(left, right, DiffProvider.DefaultMaxDepth, options, ignoreProperties);
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
        public static ICollection<Difference> Diff(this object left, object right, int maxDepth, ComparisonOptions options, params string[] ignorePropertiesorPaths)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, options, ignorePropertiesorPaths);
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
        public static ICollection<Difference> Diff<T>(this T left, T right, int maxDepth, ComparisonOptions options, params Expression<Func<T, object>>[] ignoreProperties)
        {
            var diffProvider = new DiffProvider();
            return diffProvider.ComputeDiff(left, right, maxDepth, options, ignoreProperties);
        }
    }
}
