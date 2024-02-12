using System;
using System.Collections.Generic;

namespace Ming.Util
{
    public class ReusableObject<T> where T : new()
    {
        List<T> _objects = new List<T>();
        Action<T> _initializeMethod;
        int idx_;

        public ReusableObject(Action<T> initializeMethod)
        {
            _initializeMethod = initializeMethod;
        }

        public T GetObject()
        {
            if (idx_ >= _objects.Count)
                CreateNew();

            return _objects[idx_++];
        }

        public void ReturnObject(T obj)
        {
            _objects[--idx_] = obj;
        }

        void CreateNew()
        {
            var newObject = new T();
            _initializeMethod(newObject);
            _objects.Add(newObject);
        }
    }
}
