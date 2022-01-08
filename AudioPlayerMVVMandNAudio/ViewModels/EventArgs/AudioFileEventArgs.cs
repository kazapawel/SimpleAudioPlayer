using System;

namespace AudioPlayerMVVMandNAudio
{
    public class AudioFileVMEventArgs : EventArgs
    {
        public AudioFileVM AudioFileVM { get; set; }

        public AudioFileVMEventArgs(AudioFileVM file)
        {
            AudioFileVM = file;
        }
    }
}
