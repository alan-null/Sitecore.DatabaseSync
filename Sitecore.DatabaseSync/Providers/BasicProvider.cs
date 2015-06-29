using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sitecore.DatabaseSync.Models;

namespace Sitecore.DatabaseSync.Providers
{
    class BasicProvider : IDatabaseSyncChangesProvider
    {
        public List<ItemChange> GetItems()
        {
            var serializationFiles = Directory.GetFiles(Configuration.Settings.DataFolder, "*.item", SearchOption.AllDirectories);
            var rawFields = serializationFiles.Select(ReadItem);
            return rawFields.Select(id => new ItemChange
            {
                Database = id["database"].TrimStart(' '),
                SitecorePath = id["path"].TrimStart(' ')
            }).ToList();
        }

        private Dictionary<string, string> ReadItem(string path)
        {
            List<string> lines = GetRawContent(path);
            List<string> range = GetSection(@"----item----", lines);
            return range.Select(x => x.Split(':')).ToList().ToDictionary(s => s[0], s => s[1]);
        }

        private static List<string> GetSection(string section, List<string> lines)
        {
            var startIndex = lines.IndexOf(section);
            int finishIndex = lines.IndexOf(String.Empty, startIndex);
            return lines.GetRange(startIndex + 1, finishIndex - 1);
        }

        private static List<string> GetRawContent(string path)
        {
            return File.ReadAllLines(path).ToList();
        }
    }
}