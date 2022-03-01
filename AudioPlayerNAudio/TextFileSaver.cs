using System.Collections.Generic;
using System.IO;

namespace AudioPlayerNAudio
{
    public class TextFileSaver
    {
        public static void ExportToFile(string path, IEnumerable<string> collection) => File.WriteAllLines(path, collection);

        public static IEnumerable<string> ImportFromFile(string path) => File.Exists(path) ? File.ReadAllLines(path) : new string[0];
    }
}
