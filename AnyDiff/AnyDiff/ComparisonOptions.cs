using System;

namespace AnyDiff
{
    /// <summary>
    /// The difference comparison options
    /// </summary>
    [Flags]
    public enum ComparisonOptions
    {
        /// <summary>
        /// Compare properties
        /// </summary>
        CompareProperties = 1,
        /// <summary>
        /// Compare fields
        /// </summary>
        CompareFields = 2,
        /// <summary>
        /// Compare arrays/collections/dictionaries
        /// </summary>
        CompareCollections = 4,
        /// <summary>
        /// Specify if you want disable the ignore by attribute feature
        /// </summary>
        DisableIgnoreAttributes = 8,
        /// <summary>
        /// Specify if you want to allow comparing of two differntly typed objects
        /// </summary>
        AllowCompareDifferentObjects = 16,
        /// <summary>
        /// Compare all objects
        /// </summary>
        All = CompareProperties | CompareFields | CompareCollections,
    }
}
