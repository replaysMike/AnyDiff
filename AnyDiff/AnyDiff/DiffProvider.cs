using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using TypeSupport;
using TypeSupport.Extensions;

namespace AnyDiff
{
    /// <summary>
    /// Compare two objects for value differences
    /// </summary>
    public class DiffProvider
    {
        /// <summary>
        /// The default max depth to use
        /// </summary>
        internal const int DefaultMaxDepth = 32;

        /// <summary>
        /// The list of attributes to use when ignoring fields/properties
        /// </summary>
        private readonly ICollection<Type> _ignoreAttributes = new List<Type> {
            typeof(IgnoreDataMemberAttribute),
            typeof(NonSerializedAttribute),
            typeof(JsonIgnoreAttribute),
        };

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff(object left, object right)
        {
            return ComputeDiff(left, right, DefaultMaxDepth, ComparisonOptions.All);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="options">Specify the comparison options</param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff(object left, object right, ComparisonOptions options)
        {
            return ComputeDiff(left, right, DefaultMaxDepth, options);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="ignorePropertiesOrPaths">A list of property names or full path names to ignore</param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff(object left, object right, ComparisonOptions options, params string[] ignorePropertiesOrPaths)
        {
            return ComputeDiff(left, right, DefaultMaxDepth, options, ignorePropertiesOrPaths);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff<T>(T left, T right)
        {
            return ComputeDiff(left, right, DefaultMaxDepth, ComparisonOptions.All);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff<T>(T left, T right, ComparisonOptions options, params Expression<Func<T, object>>[] ignoreProperties)
        {
            return ComputeDiff(left, right, DefaultMaxDepth, options, ignoreProperties);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="maxDepth"></param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="ignorePropertiesOrPaths">A list of property names or full path names to ignore</param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff(object left, object right, int maxDepth, ComparisonOptions options, params string[] ignorePropertiesOrPaths)
        {
            return RecurseProperties(left, right, null, new List<Difference>(), 0, maxDepth, new HashSet<int>(), string.Empty, options, ignorePropertiesOrPaths);
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
        public ICollection<Difference> ComputeDiff<T>(T left, T right, int maxDepth, ComparisonOptions options, params Expression<Func<T, object>>[] ignoreProperties)
        {
            var ignorePropertiesList = new List<string>();
            if (ignoreProperties != null)
            {
                foreach (var expression in ignoreProperties)
                {
                    var name = "";
                    switch (expression.Body)
                    {
                        case MemberExpression m:
                            name = m.Member.Name;
                            break;
                        case UnaryExpression u when u.Operand is MemberExpression m:
                            name = m.Member.Name;
                            break;
                        default:
                            throw new NotImplementedException(expression.GetType().ToString());
                    }
                    ignorePropertiesList.Add(name);
                }
            }
            return RecurseProperties(left, right, null, new List<Difference>(), 0, maxDepth, new HashSet<int>(), string.Empty, options, ignorePropertiesList);
        }

        /// <summary>
        /// Recurse the object's tree
        /// </summary>
        /// <param name="left">The left object to compare</param>
        /// <param name="right">The right object to compare</param>
        /// <param name="parent">The parent object</param>
        /// <param name="differences">A list of differences currently found in the tree</param>
        /// <param name="currentDepth">The current depth of the tree recursion</param>
        /// <param name="maxDepth">The maximum number of tree children to recurse</param>
        /// <param name="objectTree">A hash table containing the tree that has already been traversed, to prevent recursion loops</param>
        /// <param name="allowCompareDifferentObjects">True to allow comparing of objects with different types</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="ignorePropertiesOrPaths">A list of property names or full path names to ignore</param>
        /// <returns></returns>
        private ICollection<Difference> RecurseProperties(object left, object right, object parent, ICollection<Difference> differences, int currentDepth, int maxDepth, HashSet<int> objectTree, string path, ComparisonOptions options, ICollection<string> ignorePropertiesOrPaths)
        {
            if (IgnoreObjectName(null, path, options, ignorePropertiesOrPaths, null))
                return differences;

            if (!options.BitwiseHasFlag(ComparisonOptions.AllowCompareDifferentObjects)
                && left != null && right != null
                && left?.GetType() != right?.GetType())
                throw new ArgumentException("Objects Left and Right must be of the same type.");

            if (left == null && right == null)
                return differences;

            if (maxDepth > 0 && currentDepth >= maxDepth)
                return differences;

            if (ignorePropertiesOrPaths == null)
                ignorePropertiesOrPaths = new List<string>();

            var typeSupport = new ExtendedType(left != null ? left.GetType() : right.GetType());
            if (typeSupport.Attributes.Any(x => _ignoreAttributes.Contains(x)))
                return differences;
            if (typeSupport.IsDelegate)
                return differences;

            // increment the current recursion depth
            currentDepth++;

            // construct a hashtable of objects we have already inspected (simple recursion loop preventer)
            // we use this hashcode method as it does not use any custom hashcode handlers the object might implement
            if (left != null)
            {
                var hashCode = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(left);
                if (objectTree.Contains(hashCode))
                    return differences;
                objectTree.Add(hashCode);
            }

            // get list of properties
            var properties = new List<ExtendedProperty>();
            if (options.BitwiseHasFlag(ComparisonOptions.CompareProperties))
                properties.AddRange(left.GetProperties(PropertyOptions.All));

            // get all fields, except for backed auto-property fields
            var fields = new List<ExtendedField>();
            if (options.BitwiseHasFlag(ComparisonOptions.CompareFields))
            {
                fields.AddRange(left.GetFields(FieldOptions.All));
                fields = fields.Where(x => !x.IsBackingField).ToList();
            }

            var rootPath = path;
            var localPath = string.Empty;
            foreach (var property in properties)
            {
                localPath = $"{rootPath}.{property.Name}";
                if (IgnoreObjectName(property.Name, localPath, options, ignorePropertiesOrPaths, property.CustomAttributes))
                    continue;
                object leftValue = null;
                try
                {
                    if (left != null)
                        leftValue = left.GetPropertyValue(property);
                }
                catch (Exception)
                {
                    // catch any exceptions accessing the property
                }
                object rightValue = null;
                try
                {
                    if (right != null)
                        rightValue = right.GetPropertyValue(property);
                }
                catch (Exception)
                {
                    // catch any exceptions accessing the property
                }
                differences = GetDifferences(property.Name, property.Type, GetTypeConverter(property), leftValue, rightValue, parent, differences, currentDepth, maxDepth, objectTree, localPath, options, ignorePropertiesOrPaths);
            }
            foreach (var field in fields)
            {
                localPath = $"{rootPath}.{field.Name}";
                if (IgnoreObjectName(field.Name, localPath, options, ignorePropertiesOrPaths, field.CustomAttributes))
                    continue;
                object leftValue = null;
                if (left != null)
                    leftValue = left.GetFieldValue(field);
                object rightValue = null;
                if (right != null)
                    rightValue = right.GetFieldValue(field);
                differences = GetDifferences(field.Name, field.Type, GetTypeConverter(field), leftValue, rightValue, parent, differences, currentDepth, maxDepth, objectTree, localPath, options, ignorePropertiesOrPaths);
            }

            return differences;
        }

        /// <summary>
        /// Get the differences between two objects
        /// </summary>
        /// <param name="propertyName">The name of the property being compared</param>
        /// <param name="propertyType">The type of property being compared. The left property is assumed unless allowCompareDifferentObjects=true</param>
        /// <param name="typeConverter">An optional TypeConverter to treat the type as a different type</param>
        /// <param name="left">The left object to compare</param>
        /// <param name="right">The right object to compare</param>
        /// <param name="parent">The parent object</param>
        /// <param name="differences">A list of differences currently found in the tree</param>
        /// <param name="currentDepth">The current depth of the tree recursion</param>
        /// <param name="maxDepth">The maximum number of tree children to recurse</param>
        /// <param name="objectTree">A hash table containing the tree that has already been traversed, to prevent recursion loops</param>
        /// <param name="options">Specify the comparison options</param>
        /// <param name="ignorePropertiesOrPaths">A list of property names or full path names to ignore</param>
        /// <returns></returns>
        private ICollection<Difference> GetDifferences(string propertyName, Type propertyType, TypeConverter typeConverter, object left, object right, object parent, ICollection<Difference> differences, int currentDepth, int maxDepth, HashSet<int> objectTree, string path, ComparisonOptions options, ICollection<string> ignorePropertiesOrPaths)
        {
            if (IgnoreObjectName(propertyName, path, options, ignorePropertiesOrPaths, null))
                return differences;

            object leftValue = null;
            object rightValue = null;
            leftValue = left;

            if (options.BitwiseHasFlag(ComparisonOptions.AllowCompareDifferentObjects) && rightValue != null)
                rightValue = GetValueForProperty(right, propertyName);
            else
                rightValue = right;

            if (rightValue == null && leftValue != null || leftValue == null && rightValue != null)
            {
                differences.Add(new Difference((leftValue ?? rightValue).GetType(), propertyName, path, leftValue, rightValue, typeConverter));
                return differences;
            }

            if (leftValue == null && rightValue == null)
                return differences;

            var isCollection = propertyType != typeof(string) && propertyType.GetInterface(nameof(IEnumerable)) != null;
            if (isCollection && options.BitwiseHasFlag(ComparisonOptions.CompareCollections))
            {
                var genericArguments = propertyType.GetGenericArguments();
                // iterate the collection
                var aValueCollection = (leftValue as IEnumerable);
                var bValueCollection = (rightValue as IEnumerable);
                var bValueCollectionCount = GetCountFromEnumerable(bValueCollection);
                var bValueEnumerator = bValueCollection?.GetEnumerator();
                var arrayIndex = 0;
                if (aValueCollection != null)
                {
                    foreach (var collectionItem in aValueCollection)
                    {
                        var hasValue = bValueEnumerator?.MoveNext() ?? false;
                        leftValue = collectionItem;
                        if (hasValue)
                        {
                            rightValue = bValueEnumerator?.Current;
                            // check array element for difference
                            if (!leftValue.GetType().IsValueType && leftValue.GetType() != typeof(string))
                            {
                                differences = RecurseProperties(leftValue, rightValue, parent, differences, currentDepth, maxDepth, objectTree, path, options, ignorePropertiesOrPaths);
                            }
                            else if (leftValue.GetType().IsGenericType && leftValue.GetType().GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                            {
                                // compare keys and values of a KVP
                                var leftKvpKey = GetValueForProperty(leftValue, "Key");
                                var leftKvpValue = GetValueForProperty(leftValue, "Value");
                                var rightKvpKey = GetValueForProperty(rightValue, "Key");
                                var rightKvpValue = GetValueForProperty(rightValue, "Value");

                                Type leftKeyType = leftKvpKey?.GetType() ?? genericArguments.First();
                                Type leftValueType = leftKvpValue?.GetType() ?? genericArguments.Skip(1).First();

                                // compare the key
                                if (leftKvpKey != null && !leftKeyType.IsValueType && leftKeyType != typeof(string))
                                    differences = RecurseProperties(leftKvpKey, rightKvpKey, leftValue, differences, currentDepth, maxDepth, objectTree, path, options, ignorePropertiesOrPaths);
                                else
                                {
                                    if (!IsMatch(leftKvpKey, rightKvpKey))
                                        differences.Add(new Difference(leftKeyType, propertyName, path, arrayIndex, leftKvpKey, rightKvpKey, typeConverter));
                                }

                                // compare the value
                                if (leftKvpValue != null && !leftValueType.IsValueType && leftValueType != typeof(string))
                                    differences = RecurseProperties(leftKvpValue, rightKvpValue, leftValue, differences, currentDepth, maxDepth, objectTree, path, options, ignorePropertiesOrPaths);
                                else
                                {
                                    if (!IsMatch(leftValue, rightValue))
                                        differences.Add(new Difference(leftValueType, propertyName, path, arrayIndex, leftKvpValue, rightKvpValue, typeConverter));
                                }
                            }
                            else
                            {
                                if (!IsMatch(leftValue, rightValue))
                                    differences.Add(new Difference(leftValue.GetType(), propertyName, path, arrayIndex, leftValue, rightValue, typeConverter));
                            }
                        }
                        else
                        {
                            // left has a value in collection, right does not. That's a difference
                            rightValue = null;
                            differences.Add(new Difference(leftValue.GetType(), propertyName, path, arrayIndex, leftValue, rightValue, typeConverter));
                        }
                        arrayIndex++;
                    }
                    if (bValueCollectionCount > arrayIndex)
                    {
                        // right side has extra elements
                        var rightSideExtraElements = bValueCollectionCount - arrayIndex;
                        if (bValueEnumerator != null)
                        {
                            for (var i = 0; i < rightSideExtraElements; i++)
                            {
                                bValueEnumerator.MoveNext();
                                differences.Add(new Difference(aValueCollection.GetType(), propertyName, path, arrayIndex, null, bValueEnumerator.Current, typeConverter));
                                arrayIndex++;
                            }
                        }
                    }
                }
            }
            else if (!propertyType.IsValueType && propertyType != typeof(string))
            {
                differences = RecurseProperties(leftValue, rightValue, leftValue, differences, currentDepth, maxDepth, objectTree, path, options, ignorePropertiesOrPaths);
            }
            else
            {
                if (!IsMatch(leftValue, rightValue))
                    differences.Add(new Difference(propertyType, propertyName, path, leftValue, rightValue, typeConverter));
            }

            return differences;
        }

        private static long GetCountFromEnumerable(IEnumerable enumerable)
        {
            if (enumerable == null)
                return 0L;
            var count = 0L;
            var enumerableType = enumerable.GetType().GetExtendedType();
            if (enumerableType.IsCollection)
                return ((ICollection)enumerable).Count;
            if (enumerableType.IsArray)
                return ((Array)enumerable).LongLength;

            var enumerator = enumerable.GetEnumerator();
            // count the enumerable
            foreach (var row in enumerable)
                count++;
            enumerator.Reset();
            return count;
        }

        private static TypeConverter GetTypeConverter(PropertyInfo property)
        {
            return GetTypeConverter(property.GetCustomAttributes(typeof(TypeConverterAttribute), false).FirstOrDefault() as TypeConverterAttribute);
        }

        private static TypeConverter GetTypeConverter(FieldInfo property)
        {
            return GetTypeConverter(property.GetCustomAttributes(typeof(TypeConverterAttribute), false).FirstOrDefault() as TypeConverterAttribute);
        }

        private static TypeConverter GetTypeConverter(TypeConverterAttribute attribute)
        {
            if (attribute != null)
            {
                var typeConverter = Activator.CreateInstance(Type.GetType(attribute.ConverterTypeName)) as TypeConverter;
                return typeConverter;
            }
            return null;
        }

        /// <summary>
        /// Returns true if object name should be ignored
        /// </summary>
        /// <param name="name">Property or field name</param>
        /// <param name="path">Full path to object</param>
        /// <param name="options">Comparison options</param>
        /// <param name="ignorePropertiesOrPaths">List of names or paths to ignore</param>
        /// <returns></returns>
        private bool IgnoreObjectName(string name, string path, ComparisonOptions options, ICollection<string> ignorePropertiesOrPaths, IEnumerable<CustomAttributeData> attributes)
        {
            var ignoreByNameOrPath = ignorePropertiesOrPaths?.Contains(name) == true || ignorePropertiesOrPaths?.Contains(path) == true;
            if (ignoreByNameOrPath)
                return true;
#if FEATURE_CUSTOM_ATTRIBUTES
            if (attributes?.Any(x => _ignoreAttributes.Contains(x.AttributeType)) == true && !options.BitwiseHasFlag(ComparisonOptions.DisableIgnoreAttributes))
#else
            if (attributes?.Any(x => _ignoreAttributes.Contains(x.Constructor.DeclaringType)) == true  && !options.BitwiseHasFlag(ComparisonOptions.DisableIgnoreAttributes))
#endif
                return true;
            return false;
        }

        private static object GetValueForProperty(object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            var property = obj.GetType().GetProperties().FirstOrDefault(x => x.Name.Equals(propertyName));
#if FEATURE_SETVALUE
            return property?.GetValue(obj);
#else
            return property?.GetValue(obj, null);
#endif
        }

        private static bool IsMatch(object leftValue, object rightValue)
        {
            var isMatch = false;
            if (leftValue == null && rightValue == null)
            {
                isMatch = true;
            }
            else if (leftValue == null || rightValue == null)
            {
                isMatch = false;
            }
            else if (leftValue.Equals(rightValue))
            {
                isMatch = true;
            }
            return isMatch;
        }
    }
}
