using System.Collections.Generic;
using System.IO;
using Sitecore.Data.Serialization.ObjectModel;
using Sitecore.DatabaseSync.Models;

namespace Sitecore.DatabaseSync.Pipelines.UpdateItems
{
    class FindChangedRoots : IDataSyncPipelineProcessor
    {
        public void Process(DataSyncPipelineArgs args)
        {
            List<ChangeRoot> roots = new List<ChangeRoot>();

            foreach (ItemChange change in args.Provider.GetItems())
            {
                SyncItem syncItem = GetSyncItem(change.PhysicalPath);
                if (syncItem != null)
                {
                    ChangeRoot root = new ChangeRoot(change)
                    {
                        Type = RootType.Item
                    };
                    roots.Add(root);
                }
            }
            args.ChangeRoots = roots;
        }

        public static SyncItem GetSyncItem(string path)
        {
            if (File.Exists(path))
            {
                using (TextReader reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    return SyncItem.ReadItem(new Tokenizer(reader));
                }
            }
            return null;
        }
    }
}
