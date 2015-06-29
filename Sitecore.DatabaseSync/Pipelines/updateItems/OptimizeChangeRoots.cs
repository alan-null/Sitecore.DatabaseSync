using System.Collections.Generic;
using System.Linq;
using Sitecore.DatabaseSync.Models;

namespace Sitecore.DatabaseSync.Pipelines.UpdateItems
{
    class OptimizeChangeRoots : IDataSyncPipelineProcessor
    {
        public void Process(DataSyncPipelineArgs args)
        {
            IEnumerable<ChangeRoot> treeRoots = args.ChangeRoots.Where(x => x.Type == RootType.Tree);

            // Remove tree being subset of other tree
            args.ChangeRoots.RemoveAll(root => root.Type == RootType.Tree && root.IsChildOfTree(treeRoots));

            // Remove duplicates
            IEnumerable<ChangeRoot> changeRoots = args.ChangeRoots.GroupBy(r => new { r.Item.Paths.Path, r.Type }).Select(grp => grp.First());

            args.ChangeRoots = changeRoots.ToList();
        }
    }
}
