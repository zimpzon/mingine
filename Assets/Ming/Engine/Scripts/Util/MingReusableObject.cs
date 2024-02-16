using System;
using System.Collections.Generic;

namespace Ming
{
    public class MingReusableObject<T> where T : new()
    {
        List<T> _objects = new List<T>();
        Action<T> _initializeMethod;
        int _idx;

        public MingReusableObject(Action<T> initializeMethod)
        {
            _initializeMethod = initializeMethod;
        }

        public T GetObject()
        {
            if (_idx >= _objects.Count)
                CreateNew();

            return _objects[_idx++];
        }

        public void ReturnObject(T obj)
        {
            _objects[--_idx] = obj;
        }

        void CreateNew()
        {
            var newObject = new T();
            _initializeMethod(newObject);
            _objects.Add(newObject);
        }
    }
}
