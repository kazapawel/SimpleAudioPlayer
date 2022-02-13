using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerMVVMandNAudio
{
    public interface IAudioPlayerState
    {
        void EnterState();
        void PlayTrack();
        void StopTrack();
    }
}
