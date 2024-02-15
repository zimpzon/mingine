using UnityEngine;

namespace Ming.Engine
{
    /// <summary>
    /// This script assumes it runs before any other updates in execution order.
    /// </summary>
    public class MingMain : MingBehaviour, IMingMain
    {
        public static IMingMain Instance;

        public IMingUpdater MingUpdater => _mingUpdater;

        private readonly MingUpdater _mingUpdater = new MingUpdater();

        void Awake()
        {
            TryClaimSingleton();
        }

        private void TryClaimSingleton()
        {
            bool alreadyExists = Instance != null;
            if (alreadyExists)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        void Update()
        {
            // TODO: fixed delta time loop here
            MingTime.Update(Time.time, Time.deltaTime, Time.unscaledTime);
            _mingUpdater.UpdateAll();
        }

        void LateUpdate()
        {
            _mingUpdater.LateUpdateAll();
        }
    }
}
