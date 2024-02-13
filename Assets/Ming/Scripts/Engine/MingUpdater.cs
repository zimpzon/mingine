using Ming.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ming.Engine
{
    public interface IMingUpdaterControl
    {
        void UpdateAll();
        void LateUpdateAll();
    }

    [CreateAssetMenu(fileName = "MingUpdater", menuName = "Ming/Engine/MingUpdater", order = 1)]
    public class MingUpdater : ScriptableObject, IMingUpdaterControl
    {
        private Dictionary<MingUpdatePass, List<IMingUpdate>> _passes = new();

        public MingUpdater()
        {
            var values = Enum.GetValues(typeof(MingUpdatePass));
            foreach(var value in values)
            {
                const int Capacity = 200;   
                _passes[(MingUpdatePass)value] = new List<IMingUpdate>(Capacity);
            }
        }

        public void RegisterForUpdate(IMingUpdate component, params MingUpdatePass[] passes)
        {
            foreach (var pass in passes)
            {
                var list = _passes[pass];
                if (Application.isEditor)
                {
                    //if (list.Contains(component))
                    //    Debug.LogErrorFormat("Component (hash {0}) is already added for priority {1}.", component.GetHashCode(), pass);
                }

                list.Add(component);
            }
        }

        public void UnregisterForUpdate(IMingUpdate component, params MingUpdatePass[] passes)
        {
            foreach (var pass in passes)
            {
                var list = _passes[pass];
                if (Application.isEditor)
                {
                    //if (!list.Contains(component))
                    //    Debug.LogErrorFormat("Component (hash {0}) was not found in priority {1}.", component.GetHashCode(), pass);
                }

                MingLists.ReplaceRemove(list, component);
            }
        }

        void IMingUpdaterControl.UpdateAll()
        {
            foreach(var pair in _passes)
            {
                var pass = pair.Key;
                if (pass < MingUpdatePass.Late)
                {
                    var list = pair.Value;
                    for (int i = 0; i < list.Count; ++i)
                    {
                        list[i].MingUpdate(pass);
                    }
                }
            }
        }

        void IMingUpdaterControl.LateUpdateAll()
        {
            foreach (var pair in _passes)
            {
                var pass = pair.Key;
                if (pass >= MingUpdatePass.Late)
                {
                    var list = pair.Value;
                    for (int i = 0; i < list.Count; ++i)
                    {
                        list[i].MingUpdate(pass);
                    }
                }
            }
        }
    }
}
