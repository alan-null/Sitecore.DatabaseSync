using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.DatabaseSync.Helpers;
using Sitecore.DatabaseSync.Models;

namespace Sitecore.DatabaseSync.Providers
{
    public class BasicProvider : IDatabaseSyncChangesProvider
    {
        public List<ItemChange> GetItems()
        {
            var serializationFiles = Directory.GetFiles(Settings.DataFolder, "*.item", SearchOption.AllDirectories);
            var syncItems = serializationFiles.Select(ItemSynchronization.GetSyncItem);
            return syncItems.Select(id => new ItemChange
            {
                Database = id.DatabaseName,
                SitecorePath = id.ItemPath
            }).ToList();
        }
    }
}