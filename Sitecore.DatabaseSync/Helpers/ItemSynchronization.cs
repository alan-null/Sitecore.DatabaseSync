using System.IO;
using Sitecore.Data.Serialization.ObjectModel;

namespace Sitecore.DatabaseSync.Helpers
{
    public class ItemSynchronization
    {
        public static SyncItem GetSyncItem(string path)
        {
            using (TextReader reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                return SyncItem.ReadItem(new Tokenizer(reader));
            }
        }
    }
}
