using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sitecore.Configuration;
using Sitecore.DatabaseSync.Models;

namespace Sitecore.DatabaseSync.Providers
{
    public class BasicProvider : IDatabaseSyncChangesProvider
    {
        public IEnumerable<ItemChange> GetItems()
        {
            var serializationFiles = Directory.GetFiles(Settings.DataFolder, "*.item", SearchOption.AllDirectories);
            return serializationFiles.Select(path => new ItemChange
            {
                PhysicalPath = path
            });
        }
    }
}