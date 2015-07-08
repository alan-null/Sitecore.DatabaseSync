using System.Collections.Generic;
using Sitecore.DatabaseSync.Models;
using Sitecore.Pipelines;

namespace Sitecore.DatabaseSync.Pipelines.UpdateItems
{
    public class DataSyncPipelineArgs : PipelineArgs
    {
        public IDatabaseSyncChangesProvider Provider { get; set; }
        public IEnumerable<ChangeRoot> ChangeRoots { get; set; }
    }
}
