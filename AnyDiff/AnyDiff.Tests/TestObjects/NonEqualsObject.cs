namespace AnyDiff.Tests.TestObjects
{
    public class NonEqualsObject
    {
        public int Id { get; set; }
        // name will not be used in equals comparison
        public string Name { get; set; }

        public NonEqualsObject(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
