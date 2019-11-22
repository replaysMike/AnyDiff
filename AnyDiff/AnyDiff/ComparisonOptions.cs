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
        /// Specify if you want to allow comparing of lists/arrays that don't have the same order
        /// </summary>
        AllowCollectionsToBeOutOfOrder = 32,
        /// <summary>
        /// Specify if you want to allow Equals overrides for object comparison
        /// </summary>
        AllowEqualsOverride = 64,
        /// <summary>
        /// Specify the property list passed is an include list
        /// </summary>
        IncludeList = 128,
        /// <summary>
        /// Specify the property list passed is an exclude list (default)
        /// </summary>
        ExcludeList = 256,
        /// <summary>
        /// Specify the IncludeList should not inherit children unless specified
        /// </summary>
        IncludeListNoInheritance = 512,
        /// <summary>
        /// Specify that an empty list and a null list would be an equal comparison
        /// </summary>
        TreatEmptyListAndNullTheSame = 1024,
        /// <summary>
        /// Compare all objects
        /// </summary>
        All = CompareProperties | CompareFields | CompareCollections,
    }
}
