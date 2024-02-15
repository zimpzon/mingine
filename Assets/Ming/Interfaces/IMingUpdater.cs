using Ming.Engine;

namespace Ming
{
    public interface IMingUpdater
    {
        void RegisterForUpdate(IMingObject mingObject, params MingUpdatePass[] passes);
        void UnregisterForUpdate(IMingObject mingObject, params MingUpdatePass[] passes);
    }
}
