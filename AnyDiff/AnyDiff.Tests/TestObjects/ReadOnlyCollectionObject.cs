using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AnyDiff.Tests.TestObjects
{
    public class ReadOnlyCollectionObject
    {
        public ReadOnlyCollection<EqualsObject> Collection { get; }
        public ReadOnlyCollectionObject(IList<EqualsObject> collection)
        {
            Collection = new ReadOnlyCollection<EqualsObject>(collection);
        }
    }
}
