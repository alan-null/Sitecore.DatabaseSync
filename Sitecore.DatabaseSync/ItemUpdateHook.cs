using Sitecore.Events.Hooks;

namespace Sitecore.DatabaseSync
{
    class ItemUpdateHook : IHook
    {
        public void Initialize()
        {
            DatabaseSynchronizer.SynchronizeDatabase();
        }
    }
}
