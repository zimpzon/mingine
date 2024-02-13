using UnityEngine;

namespace Ming.Engine
{
    /// <summary>
    /// This script assumes it runs before any other updates in execution order.
    /// </summary>
    public class MingMain : MingBehaviour
    {
        public MingUpdater MingUpdaterControl;

        void Update()
        {
            // TODO: fixed delta time loop here
            MingTime.Update(Time.time, Time.deltaTime, Time.unscaledTime);
            (MingUpdaterControl as IMingUpdaterControl).UpdateAll();
        }

        void LateUpdate()
        {
            (MingUpdaterControl as IMingUpdaterControl).LateUpdateAll();
        }
    }
}
