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

namespace AnyDiff
{
    /// <summary>
    /// Compare two objects for value differences
    /// Supports two of the same type, or different type objects.
    /// </summary>
    public class DiffProvider
    {
        private const int DefaultMaxDepth = -1;
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
            return ComputeDiff(left, right, DefaultMaxDepth);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="ignoreProperties"></param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff<T>(T left, T right, params Expression<Func<T, object>>[] ignoreProperties)
        {
            return ComputeDiff(left, right, DefaultMaxDepth, ignoreProperties);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="maxDepth"></param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff(object left, object right, int maxDepth)
        {
            return RecurseProperties(left, right, null, new List<Difference>(), 0, maxDepth, new HashSet<int>(), false, string.Empty);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff<T>(T left, T right, int maxDepth, params Expression<Func<T, object>>[] ignoreProperties)
        {
            var ignorePropertiesList = new List<string>();
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
            return RecurseProperties(left, right, null, new List<Difference>(), 0, maxDepth, new HashSet<int>(), false, string.Empty, ignorePropertiesList);
        }

        /// <summary>
        /// Compare two objects for value differences
        /// </summary>
        /// <param name="left">Object A</param>
        /// <param name="right">Object B</param>
        /// <param name="maxDepth">Maximum recursion depth</param>
        /// <param name="allowCompareDifferentObjects">True to allow comparison of objects of a different type</param>
        /// <returns></returns>
        public ICollection<Difference> ComputeDiff(object left, object right, int maxDepth, bool allowCompareDifferentObjects)
        {
            return RecurseProperties(left, right, null, new List<Difference>(), 0, maxDepth, new HashSet<int>(), allowCompareDifferentObjects, string.Empty);
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
        /// <param name="ignoreProperties">List of property names to ignore</param>
        /// <returns></returns>
        private ICollection<Difference> RecurseProperties(object left, object right, object parent, ICollection<Difference> differences, int currentDepth, int maxDepth, HashSet<int> objectTree, bool allowCompareDifferentObjects, string path, ICollection<string> ignoreProperties = null)
        {
            if (!allowCompareDifferentObjects
                && left != null && right != null
                && left?.GetType() != right?.GetType())
                throw new ArgumentException("Objects Left and Right must be of the same type.");

            if (left == null && right == null)
                return differences;

            if (maxDepth > 0 && currentDepth >= maxDepth)
                return differences;

            if (ignoreProperties == null)
                ignoreProperties = new List<string>();

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

            // everything is ok to recurse
            var properties = GetProperties(left);
            var fields = GetFields(left);

            var rootPath = path;
            foreach (var property in properties)
            {
                path = $"{rootPath}.{property.Name}";
                if (property.CustomAttributes.Any(x => _ignoreAttributes.Contains(x.AttributeType)))
                    continue;
                var propertyTypeSupport = new ExtendedType(property.PropertyType);
                object leftValue = null;
                try
                {
                    if (left != null)
                        leftValue = property.GetValue(left);
                }
                catch (Exception)
                {
                    // catch any exceptions accessing the property
                }
                object rightValue = null;
                try
                {
                    if (right != null)
                        rightValue = property.GetValue(right);
                }
                catch (Exception)
                {
                    // catch any exceptions accessing the property
                }
                differences = GetDifferences(property.Name, property.PropertyType, GetTypeConverter(property), leftValue, rightValue, parent, differences, currentDepth, maxDepth, objectTree, allowCompareDifferentObjects, path, ignoreProperties);
            }
            foreach (var field in fields)
            {
                path = $"{rootPath}.{field.Name}";
                if (field.CustomAttributes.Any(x => _ignoreAttributes.Contains(x.AttributeType)))
                    continue;
                var fieldTypeSupport = new ExtendedType(field.FieldType);
                object leftValue = null;
                if (left != null)
                    leftValue = field.GetValue(left);
                object rightValue = null;
                if (right != null)
                    rightValue = field.GetValue(right);
                differences = GetDifferences(field.Name, field.FieldType, GetTypeConverter(field), leftValue, rightValue, parent, differences, currentDepth, maxDepth, objectTree, allowCompareDifferentObjects, path, ignoreProperties);
            }

            return differences;
        }

        private TypeConverter GetTypeConverter(PropertyInfo property)
        {
            return GetTypeConverter(property.GetCustomAttributes(typeof(TypeConverterAttribute), false).FirstOrDefault() as TypeConverterAttribute);
        }

        private TypeConverter GetTypeConverter(FieldInfo property)
        {
            return GetTypeConverter(property.GetCustomAttributes(typeof(TypeConverterAttribute), false).FirstOrDefault() as TypeConverterAttribute);
        }

        private TypeConverter GetTypeConverter(TypeConverterAttribute attribute)
        {
            if (attribute != null)
            {
                var typeConverter = Activator.CreateInstance(Type.GetType(attribute.ConverterTypeName)) as TypeConverter;
                return typeConverter;
            }
            return null;
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
        /// <param name="allowCompareDifferentObjects">True to allow comparing of objects with different types</param>
        /// <param name="ignoreProperties">List of property names to ignore</param>
        /// <returns></returns>
        private ICollection<Difference> GetDifferences(string propertyName, Type propertyType, TypeConverter typeConverter, object left, object right, object parent, ICollection<Difference> differences, int currentDepth, int maxDepth, HashSet<int> objectTree, bool allowCompareDifferentObjects, string path, ICollection<string> ignoreProperties = null)
        {
            if (!ignoreProperties.Contains(propertyName))
            {
                object leftValue = null;
                object rightValue = null;
                leftValue = left;

                if (allowCompareDifferentObjects && rightValue != null)
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
                if (isCollection)
                {
                    // iterate the collection
                    var aValueCollection = (leftValue as IEnumerable);
                    var bValueCollection = (rightValue as IEnumerable);
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
                                    differences = RecurseProperties(leftValue, rightValue, parent, differences, currentDepth, maxDepth, objectTree, allowCompareDifferentObjects, path, ignoreProperties);
                                }
                                else if (leftValue.GetType().IsGenericType && leftValue.GetType().GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                                {
                                    // compare keys and values of a KVP
                                    var leftKvpKey = GetValueForProperty(leftValue, "Key");
                                    var leftKvpValue = GetValueForProperty(leftValue, "Value");
                                    var rightKvpKey = GetValueForProperty(rightValue, "Key");
                                    var rightKvpValue = GetValueForProperty(rightValue, "Value");
                                    differences = RecurseProperties(leftKvpKey, rightKvpKey, leftValue, differences, currentDepth, maxDepth, objectTree, allowCompareDifferentObjects, path, ignoreProperties);
                                    differences = RecurseProperties(leftKvpValue, rightKvpValue, leftValue, differences, currentDepth, maxDepth, objectTree, allowCompareDifferentObjects, path, ignoreProperties);
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
                    }
                }
                else if (!propertyType.IsValueType && propertyType != typeof(string))
                {
                    differences = RecurseProperties(leftValue, rightValue, leftValue, differences, currentDepth, maxDepth, objectTree, allowCompareDifferentObjects, path, ignoreProperties);
                }
                else
                {
                    if (!IsMatch(leftValue, rightValue))
                        differences.Add(new Difference(propertyType, propertyName, path, leftValue, rightValue, typeConverter));
                }
            }
            return differences;
        }

        private object GetValueForProperty(object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            var property = obj.GetType().GetProperties().FirstOrDefault(x => x.Name.Equals(propertyName));
            return property?.GetValue(obj);
        }

        private object GetValueForField(object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            var field = obj.GetType().GetFields().FirstOrDefault(x => x.Name.Equals(propertyName));
            return field?.GetValue(obj);
        }

        private bool IsMatch(object leftValue, object rightValue)
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

        private ICollection<PropertyInfo> GetProperties(object obj)
        {
            if (obj != null)
            {
                var t = obj.GetType();
                return t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            }
            return new PropertyInfo[0];
        }

        private ICollection<FieldInfo> GetFields(object obj)
        {
            if (obj != null)
            {
                var t = obj.GetType();
                var allFields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var allFieldsExcludingAutoPropertyFields = allFields.Where(x => !x.Name.Contains("k__BackingField")).ToList();
                return allFieldsExcludingAutoPropertyFields;
            }
            return new FieldInfo[0];
        }
    }
}
