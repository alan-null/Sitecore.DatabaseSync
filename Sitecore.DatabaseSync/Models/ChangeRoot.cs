using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;

namespace Sitecore.DatabaseSync.Models
{
    public class ChangeRoot
    {
        public Item Item { get; set; }
        public RootType Type { get; set; }

        public ChangeRoot(Item item)
        {
            Item = item;
        }

        public bool IsChildOfTree(IEnumerable<ChangeRoot> treeRoots)
        {
            return treeRoots.Any(x => x.Item.Paths.IsAncestorOf(Item));
        }
    }
}