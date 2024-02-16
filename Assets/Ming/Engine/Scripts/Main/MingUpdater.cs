using System;
using System.Collections.Generic;

namespace Ming
{
    public class MingUpdater : IMingUpdater
    {
        private Dictionary<MingUpdatePass, List<IMingObject>> _passes = new();

        public MingUpdater()
        {
            var values = Enum.GetValues(typeof(MingUpdatePass));
            foreach(var value in values)
            {
                const int Capacity = 200;   
                _passes[(MingUpdatePass)value] = new List<IMingObject>(Capacity);
            }
        }

        public void RegisterForUpdate(IMingObject mingObject, params MingUpdatePass[] passes)
        {
            foreach (var pass in passes)
            {
                var list = _passes[pass];
                list.Add(mingObject);
            }
        }

        public void UnregisterForUpdate(IMingObject mingObject, params MingUpdatePass[] passes)
        {
            foreach (var pass in passes)
            {
                var list = _passes[pass];
                MingLists.ReplaceRemove(list, mingObject);
            }
        }

        public void MingUpdateAll()
        {
            foreach(var pair in _passes)
            {
                var pass = pair.Key;
                if (pass <= MingUpdatePass.JustBeforeUnityUpdate)
                {
                    var list = pair.Value;
                    for (int i = 0; i < list.Count; ++i)
                    {
                        list[i].MingUpdate(pass);
                    }
                }
            }
        }

        public void MingLateUpdateAll()
        {
            foreach (var pair in _passes)
            {
                var pass = pair.Key;
                if (pass >= MingUpdatePass.MingLate)
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
