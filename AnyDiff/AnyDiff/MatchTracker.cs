using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AnyDiff
{
    internal class MatchTracker : IDisposable
    {
        private Hashtable _leftObjects = new Hashtable();
        private Hashtable _rightObjects = new Hashtable();

        public void AddLeft(object value, int arrayIndex)
        {
            if (!_leftObjects.ContainsKey(arrayIndex))
                _leftObjects.Add(arrayIndex, new ArrayObject { Object = value, ArrayIndex = arrayIndex });
        }
        public void AddRight(object value, int arrayIndex)
        {
            if (!_rightObjects.ContainsKey(arrayIndex))
                _rightObjects.Add(arrayIndex, new ArrayObject { Object = value, ArrayIndex = arrayIndex });
        }

        public void MatchLeft(object value, int arrayIndex)
        {
            var obj = (ArrayObject)_leftObjects[arrayIndex];
            obj.Matches++;
        }
        public void MatchRight(object value, int arrayIndex)
        {
            var obj = (ArrayObject)_rightObjects[arrayIndex];
            obj.Matches++;
        }

        public ICollection<ArrayObject> GetLeftUnmatched() {
            var unmatched = new List<ArrayObject>();
            foreach(var arrayIndex in _leftObjects.Keys)
            {
                var obj = (ArrayObject)_leftObjects[arrayIndex];
                if (obj.Matches == 0)
                    unmatched.Add(obj);
            }
            return unmatched;
        }

        public ICollection<ArrayObject> GetRightUnmatched()
        {
            var unmatched = new List<ArrayObject>();
            foreach (var arrayIndex in _rightObjects.Keys)
            {
                var obj = (ArrayObject)_rightObjects[arrayIndex];
                if (obj.Matches == 0)
                    unmatched.Add(obj);
            }
            return unmatched;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _leftObjects.Clear();
                _rightObjects.Clear();
                _leftObjects = null;
                _rightObjects = null;
            }
        }
    }

    internal class ArrayObject
    {
        internal int ArrayIndex { get; set; }
        internal object Object { get; set; }
        internal int Matches { get; set; }
    }
}
