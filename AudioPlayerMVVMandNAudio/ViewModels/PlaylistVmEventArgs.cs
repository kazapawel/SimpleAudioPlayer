using System;

namespace AudioPlayerMVVMandNAudio
{
    public class PlaylistVmEventArgs :EventArgs
    {
        public bool IsEnded { get; set; }

        public PlaylistVmEventArgs(bool condition)
        {
            IsEnded = condition;
        }
    }
}
