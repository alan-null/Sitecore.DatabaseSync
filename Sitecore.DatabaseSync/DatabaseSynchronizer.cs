using Sitecore.Configuration;
using Sitecore.DatabaseSync.Models;
using Sitecore.DatabaseSync.Pipelines.UpdateItems;
using Sitecore.Pipelines;

namespace Sitecore.DatabaseSync
{
    public class DatabaseSynchronizer
    {
        public static void SynchronizeDatabase()
        {
            DataSyncPipelineArgs args = new DataSyncPipelineArgs
            {
                Provider = Factory.CreateObject("databaseSync/changesProvider", true) as IDatabaseSyncChangesProvider
            };
            CorePipeline.Run("SynchronizeDatabase", args);
        }

        public static void SynchronizeDatabase(IDatabaseSyncChangesProvider provider)
        {
            DataSyncPipelineArgs args = new DataSyncPipelineArgs
            {
                Provider = provider
            };
            CorePipeline.Run("SynchronizeDatabase", args);
        }
    }
}
