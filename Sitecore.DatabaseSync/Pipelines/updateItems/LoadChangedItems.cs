using System;
using System.Linq;
using Sitecore.Data.Serialization;
using Sitecore.DatabaseSync.Models;
using Sitecore.Diagnostics;

namespace Sitecore.DatabaseSync.Pipelines.UpdateItems
{
    class LoadChangedItems : IDataSyncPipelineProcessor
    {
        public void Process(DataSyncPipelineArgs args)
        {
            LoadOptions loadOptions = new LoadOptions
            {
                DisableEvents = true,
                ForceUpdate = true
            };
            foreach (ChangeRoot root in args.ChangeRoots.Where(r => r.Type == RootType.Item))
            {
                using (new SecurityModel.SecurityDisabler())
                {
                    try
                    {
                        Manager.LoadItem(root.PhysicalPath, loadOptions);
                    }
                    catch (Exception e)
                    {
                        Log.Error(String.Format("Cannot update following item {0}", root.PhysicalPath), e, this);
                    }
                }
            }
        }
    }
}
