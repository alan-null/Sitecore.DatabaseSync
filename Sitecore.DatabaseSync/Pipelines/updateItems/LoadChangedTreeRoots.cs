using System.Linq;
using Sitecore.Data.Serialization;
using Sitecore.DatabaseSync.Models;

namespace Sitecore.DatabaseSync.Pipelines.UpdateItems
{
    class LoadChangedTreeRoots : IDataSyncPipelineProcessor
    {
        public void Process(DataSyncPipelineArgs args)
        {
            LoadOptions loadOptions = new LoadOptions
            {
                DisableEvents = true,
                ForceUpdate = false
            };
            foreach (ChangeRoot root in args.ChangeRoots.Where(r => r.Type == RootType.Tree))
            {
                string serializationPath = PathUtils.GetDirectoryPath(new ItemReference(root.Item).ToString());
                using (new SecurityModel.SecurityDisabler())
                {
                    Manager.LoadTree(serializationPath, loadOptions);
                }
            }
        }
    }
}
