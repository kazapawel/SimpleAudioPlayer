using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerMVVMandNAudio
{
    public class StopState : IState
    {
        private AudioPlayerVM vM;

        public StopState(AudioPlayerVM vm)
        {
            vM = vm;
        }

        public void EnterState()
        {
            //Sets buffer state
            if (vM.BufferTrack != null)
                vM.BufferTrack.IsAudioFilePlaying = false;

            vM.AudioEngineStop();
            vM.OnPropertyChanged(nameof(vM.IsPlaying));
            vM.StopTimer();
        }
        public void Play()
        {
            vM.State = new PlayState(vM);
            vM.State.EnterState();
        }
        public void Stop()
        {
            //Do nothing
        }
        public void Pause()
        {
            //Do nothing
        }
    }
}
