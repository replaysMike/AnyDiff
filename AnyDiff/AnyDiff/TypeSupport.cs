using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AnyDiff
{
    /// <summary>
    /// Helper class for getting information about a <see cref="Type"/>
    /// </summary>
    public class TypeSupport
    {
        public Type Type { get; }
        public bool HasEmptyConstructor { get; private set; }
        public bool IsAbstract { get; private set; }
        public Type UnderlyingType { get; private set; }
        public bool IsImmutable { get; private set; }
        public bool IsEnumerable { get; private set; }
        public bool IsCollection { get; private set; }
        public bool IsArray { get; private set; }
        public bool IsDictionary { get; private set; }
        public bool IsGeneric { get; private set; }
        public bool IsDelegate { get; private set; }
        public ICollection<Type> Attributes { get; private set; }

        public TypeSupport(Type type)
        {
            Type = type;
            InspectType();
        }

        private void InspectType()
        {
            var emptyConstructorDefined = Type.GetConstructor(Type.EmptyTypes);
            Attributes = new List<Type>();
            HasEmptyConstructor = Type.IsValueType || emptyConstructorDefined != null;
            IsAbstract = Type.IsAbstract;
            UnderlyingType = Type.UnderlyingSystemType;
            if (
                Type == typeof(string)
            )
                IsImmutable = true;

            // collections support
            IsArray = Type.IsArray;
            IsGeneric = Type.IsGenericType;

            if (typeof(IEnumerable).IsAssignableFrom(Type))
                IsEnumerable = true;
            if (Type.IsGenericType && typeof(Collection<>).IsAssignableFrom(Type.GetGenericTypeDefinition()))
                IsCollection = true;
            if (Type.IsGenericType && Type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                IsDictionary = true;
            if (typeof(Delegate).IsAssignableFrom(Type))
                IsDelegate = true;

            // attributes
            if (Type.CustomAttributes.Any())
                Attributes = Type.CustomAttributes.Select(x => x.AttributeType).ToList();
        }
    }

    /// <summary>
    /// Helper class for getting information about a <see cref="Type"/>
    /// </summary>
    public class TypeSupport<T> : TypeSupport
    {
        public TypeSupport() : base(typeof(T))
        {
        }
    }
}
