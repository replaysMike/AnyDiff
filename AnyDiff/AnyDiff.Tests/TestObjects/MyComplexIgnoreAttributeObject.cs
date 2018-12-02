using System.Runtime.Serialization;

namespace AnyDiff.Tests.TestObjects
{
    public class MyComplexIgnoreAttributeObject
    {
        public int Id { get; }
        [IgnoreDataMember]
        public string Name { get; }
        public bool IsEnabled { get; }

        public MyComplexIgnoreAttributeObject(int id, string name, bool isEnabled)
        {
            Id = id;
            Name = name;
        }
    }
}
