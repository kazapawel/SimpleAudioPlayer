using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerMVVMandNAudio
{
    public class FilePathEventArgs : EventArgs
    {
        public string Path { get; set; }

        public FilePathEventArgs(string path)
        {
            Path = path;
        }
    }
}
