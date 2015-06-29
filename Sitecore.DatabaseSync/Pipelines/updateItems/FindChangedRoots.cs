using System;
using System.Collections.Generic;
using System.IO;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Data.Serialization;
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
                Item item = Factory.GetDatabase(change.Database).GetItem(change.SitecorePath);
                if (item == null)
                {
                    string tempPath = change.SitecorePath;
                    do
                    {
                        tempPath = GetParentPath(tempPath);
                        item = Factory.GetDatabase(change.Database).GetItem(tempPath);

                    } while (item == null);

                    ChangeRoot root = new ChangeRoot(item)
                    {
                        Type = RootType.Tree
                    };
                    roots.Add(root);
                }
                else
                {
                    if (SerializationDataDoesNotExists(item))
                    {
                        item.Recycle();
                    }
                    ChangeRoot root = new ChangeRoot(item)
                    {
                        Type = RootType.Item
                    };
                    roots.Add(root);
                }
                args.ChangeRoots = roots;
            }
        }

        private string GetParentPath(string path)
        {
            path = path.TrimEnd('/');
            var lastIndexOf = path.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase);
            return path.Substring(0, lastIndexOf).TrimEnd('/');
        }

        private bool SerializationDataDoesNotExists(Item item)
        {
            string serializationPath = PathUtils.GetFilePath(new ItemReference(item).ToString());
            return !File.Exists(serializationPath);
        }
    }
}
