﻿using Ming.Util;
using System;
using System.Collections.Generic;

namespace Ming.Engine
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

        public void UpdateAll()
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

        public void LateUpdateAll()
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
