namespace AnyDiff.Tests.TestObjects.Abstract
{
    public abstract class AbstractObject
    {
        public virtual string Type
        {
            get => GetType().Name.ToLower();
            set {}
        }
    }

    public class AbstractImplementation1 : AbstractObject
    {
        public string Property1 { get; }
        public int Property2 { get; }
    }

    public class AbstractImplementation2 : AbstractObject
    {
        public string Property2 { get; }
        public int Property3 { get; }
    }

    public class TypeWithAbstractProperty
    {
        public string Id { get; set;  }
        public AbstractObject AbstractProp { get; set; }
    }
}
