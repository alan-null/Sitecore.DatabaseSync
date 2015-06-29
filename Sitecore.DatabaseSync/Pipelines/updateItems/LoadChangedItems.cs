using System.Linq;
using Sitecore.Data.Serialization;
using Sitecore.DatabaseSync.Models;

namespace Sitecore.DatabaseSync.Pipelines.UpdateItems
{
    class LoadChangedItems : IDataSyncPipelineProcessor
    {
        public void Process(DataSyncPipelineArgs args)
        {
            LoadOptions loadOptions = new LoadOptions
            {
                DisableEvents = true,
                ForceUpdate = true,
            };
            foreach (ChangeRoot root in args.ChangeRoots.Where(r => r.Type == RootType.Item))
            {
                string serializationPath = PathUtils.GetFilePath(new ItemReference(root.Item).ToString());
                using (new SecurityModel.SecurityDisabler())
                {
                    Manager.LoadItem(serializationPath, loadOptions);
                }
            }
        }
    }
}
