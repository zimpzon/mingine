using UnityEngine;

namespace Ming
{
    /// <summary>
    /// This script assumes it runs before any other updates in the script execution order.
    /// </summary>
    public class MingMain : MingBehaviour, IMingMain
    {
        public static IMingMain Instance = MingMainNull.Instance;

        public IMingUpdater MingUpdater => _mingUpdater;

        private readonly MingUpdater _mingUpdater = new MingUpdater();

        void Awake()
        {
            TryClaimSingleton();
        }

        private void TryClaimSingleton()
        {
            bool alreadyExists = Instance != MingMainNull.Instance;
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
            MingTime.Update(Time.time, Time.deltaTime, Time.unscaledTime, Time.unscaledDeltaTime);
            _mingUpdater.MingUpdateAll();
        }

        void LateUpdate()
        {
            _mingUpdater.MingLateUpdateAll();
        }
    }
}
