namespace AnyDiff.Tests.TestObjects
{
    public class EqualsObject
    {
        public int Id { get; set; }
        // name will not be used in equals comparison
        public string Name { get; set; }

        public EqualsObject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(EqualsObject))
                return false;
            var objTyped = obj as EqualsObject;
            return objTyped.Id.Equals(Id);
        }
    }
}
