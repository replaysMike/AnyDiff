using System;

namespace AnyDiff.Tests.TestObjects
{
    public class EquatableObject : IEquatable<EquatableObject>
    {
        public int Id { get; set; }
        // name will not be used in equals comparison
        public string Name { get; set; }

        public EquatableObject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public bool Equals(EquatableObject other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return other.Id.Equals(Id);
        }
    }
}
