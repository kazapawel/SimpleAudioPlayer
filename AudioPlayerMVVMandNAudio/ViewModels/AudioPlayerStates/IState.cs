using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerMVVMandNAudio
{
    public interface IState
    {
        void EnterState();
        void Play();
        void Stop();
        void Pause();
    }
}
