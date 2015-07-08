namespace Sitecore.DatabaseSync.Models
{
    public class ChangeRoot
    {
        public RootType Type { get; set; }
        public string PhysicalPath { get; set; }

        public ChangeRoot(ItemChange itemChange)
        {
            PhysicalPath = itemChange.PhysicalPath;
        }

        public ChangeRoot(string path)
        {
            PhysicalPath = path;
        }
    }
}