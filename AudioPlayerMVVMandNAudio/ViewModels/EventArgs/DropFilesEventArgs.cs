using System;

namespace AudioPlayerMVVMandNAudio
{
    public class DropFilesEventArgs :EventArgs
    {
        public string[] Files { get; set; }

        public DropFilesEventArgs(string[] files)
        {
            Files = files;
        }
    }
}
