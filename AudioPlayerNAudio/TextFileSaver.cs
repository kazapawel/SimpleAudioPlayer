using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Buffers;

namespace AudioPlayerNAudio
{
    public class TextFileSaver
    {
        public static void ExportToFile(string path, IEnumerable<string> collection) => File.WriteAllLines(path, collection);

        public static IEnumerable<string> ImportFromFile(string path)
        {
            return File.Exists(path) ? File.ReadAllLines(path) : new string[0];
        } 

    }
}
