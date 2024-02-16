using System;
using UnityEngine;

namespace Ming
{
    public static class MingGameObjects
    {
        public static Transform FindByName(Transform trans, string name)
            => trans.Find(name) ?? throw new ArgumentException($"Child with name '{name}' not found on transform '{trans.name}'");
    }
}
