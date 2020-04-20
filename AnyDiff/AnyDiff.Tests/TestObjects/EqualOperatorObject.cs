namespace AnyDiff.Tests.TestObjects
{
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    public class EqualOperatorObject
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    {
        public int Id { get; set; }
        // name will not be used in equals comparison
        public string Name { get; set; }

        public EqualOperatorObject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static bool operator == (EqualOperatorObject a, EqualOperatorObject b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (ReferenceEquals(a, null))
                return false;
            if (ReferenceEquals(b, null))
                return false;
            return a.Id.Equals(b.Id);
        }

        public static bool operator !=(EqualOperatorObject a, EqualOperatorObject b)
        {
            return !a.Equals(b);
        }
    }
}
