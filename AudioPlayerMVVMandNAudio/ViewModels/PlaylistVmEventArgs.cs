using System;

namespace AudioPlayerMVVMandNAudio
{
    public class PlaylistVmEventArgs
    {
        public bool IsEnded { get; set; }

        public PlaylistVmEventArgs(bool condition)
        {
            IsEnded = condition;
        }
    }
}
