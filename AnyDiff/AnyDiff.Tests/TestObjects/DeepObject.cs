namespace AnyDiff.Tests.TestObjects
{
    public class DeepObject
    {
        public int Id { get; set; } = 111;
        public DeepChildObject DeepChildObject { get; set; } = new DeepChildObject();
    }

    public class DeepChildObject
    {
        public int Id { get; set; } = 222;
        public DeepChild2Object DeepChild2Object { get; set; } = new DeepChild2Object();
    }

    public class DeepChild2Object
    {
        public int Id { get; set; } = 333;
        public DeepChild3Object DeepChild3Object { get; set; } = new DeepChild3Object();
    }

    public class DeepChild3Object
    {
        public int Id { get; set; } = 444;
        public string Name { get; set; } = "Default";
    }
}
