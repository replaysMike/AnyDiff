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
        CompareProperties,
        /// <summary>
        /// Compare fields
        /// </summary>
        CompareFields,
        /// <summary>
        /// Compare arrays/collections/dictionaries
        /// </summary>
        CompareCollections,
        /// <summary>
        /// Compare all objects
        /// </summary>
        All = CompareProperties | CompareFields | CompareCollections,
        /// <summary>
        /// Specify if you want disable the ignore by attribute feature
        /// </summary>
        DisableIgnoreAttributes,
    }
}
