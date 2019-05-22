namespace AnyDiff.Tests.TestObjects
{
    public class EqualOperatorObject
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
